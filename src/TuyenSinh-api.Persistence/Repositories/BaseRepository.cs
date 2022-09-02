using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;
using TuyenSinh_api.Persistence.Entities;

namespace TuyenSinh_api.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        /***
         * Kế thừa từ IBaseRepository, sử dụng GENERIC
         */
        private readonly CleanArchitectureContext _context;
        private readonly DbSet<T> _dbSet; // Table trong db;
        private PropertyInfo[] _props
        {
            get
            {
                return typeof(T).GetProperties();
            }
        }
        public BaseRepository(CleanArchitectureContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private T TrimData(T entity)
        {
            var stringProperties = entity.GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(string) && p.CanWrite);

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(entity, null);
                if (currentValue != null)
                    stringProperty.SetValue(entity, currentValue.Trim(), null);
            }
            return entity;
        }
        public void Add(T entity)
        {
            entity = TrimData(entity);
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            var listEntity = entities.ToList();
            for (var i = 0; i < listEntity.Count(); i++)
            {
                var entity = listEntity[i];
                listEntity[i] = TrimData(entity);
            }
            _dbSet.AddRange(listEntity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync(); // Thay cách viết ví dụ _context.ThiSinh.ToListAsync()
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            //_dbSet.Attach(entity);
            entity = TrimData(entity);
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<T>> GetAllFilterV1Async(
            List<Field<T>> fields,
            IEnumerable<string> memberNames,
            T searchFilters,
            SortEnum sortDir = SortEnum.ASC,
            int pageSize = 10,
            int page = 1
        )
        {
            IQueryable<T> searchResults = _dbSet;

            // Add Query
            ConcurrentDictionary<string, ParameterExpression> _parameters = new ConcurrentDictionary<string, ParameterExpression>();
            ConcurrentDictionary<string, MemberAssignment> _bindings = new ConcurrentDictionary<string, MemberAssignment>();
            ConcurrentDictionary<string, Expression<Func<T, T>>> _selectors = new ConcurrentDictionary<string, Expression<Func<T, T>>>();

            var parameterName = typeof(T).FullName;

            var requestName = $"{parameterName}:{string.Join(",", memberNames.OrderBy(x => x))}";
            if (!_selectors.TryGetValue(requestName, out var selector))
            {
                if (!_parameters.TryGetValue(parameterName, out var parameter))
                {
                    parameter = Expression.Parameter(typeof(T), typeof(T).Name.ToLowerInvariant());

                    _ = _parameters.TryAdd(parameterName, parameter);
                }

                var bindings = memberNames
                    .Select(name =>
                    {
                        var memberName = $"{parameterName}:{name}";
                        if (!_bindings.TryGetValue(memberName, out var binding))
                        {
                            var member = Expression.PropertyOrField(parameter, name);
                            binding = Expression.Bind(member.Member, member);

                            _ = _bindings.TryAdd(memberName, binding);
                        }
                        return binding;
                    });

                var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
                selector = Expression.Lambda<Func<T, T>>(body, parameter);

                _selectors.TryAdd(requestName, selector);
            }

            var filterExpressions = new List<Expression<Func<T, bool>>>();

            //Add filters
            foreach (var field in fields)
            {
                //try to get the search value, ignoring null exceptions because it's much harder
                //to check for null objects at multiple levels. Instead the exception tells us there's
                //no search value
                string searchValue = null;
                try
                {
                    searchValue = field.GetValue(searchFilters)?.ToString();
                }
                catch (NullReferenceException) { }
                if (string.IsNullOrWhiteSpace(searchValue)) continue;

                //shared expression setup
                ParameterExpression param = field.FieldExpression.Parameters.First();
                Expression left = field.FieldExpression.Body;
                ConstantExpression right = Expression.Constant(searchValue);
                Expression body = null;

                //create expression for strings so we can use "contains" instead of "equals"           
                if (field.FieldType == typeof(string))
                {
                    //build the expression body
                    MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    body = Expression.Call(left, method, right);
                }
                else
                {   //handle expression for all other types      
                    body = Expression.Equal(left, right);
                }

                //finish expression
                Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(body, param);
                filterExpressions.Add(lambda);
            }
            searchResults = filterExpressions.Aggregate(searchResults, (current, expression) => current.Where(expression));

            //get sort field
            Field<T> sortField = fields.FirstOrDefault(f => f.SortField);
            searchResults = searchResults.Select(selector);
            if (sortDir == SortEnum.ASC)
                searchResults = searchResults.OrderBy(x => sortField.UnqualifiedFieldName);
            else
                searchResults = searchResults.OrderByDescending(x => sortField.UnqualifiedFieldName);
            //// Get the search results 
            int count = searchResults.Count();
            return await searchResults.Skip(page * pageSize - pageSize).Take(page).ToListAsync();
        }

        public async Task<ListResponse<T>> GetAllFilterAsync(
            List<Expression<Func<T, bool>>> fields,
            List<string> memberNames,
            string keySort,
            List<string> includes,
            SortEnum sortDir = SortEnum.ASC,
            int pageSize = 10,
            int page = 1,
            bool all = false
        )
        {
            IQueryable<T> searchResults = _dbSet;
            page = page < 1 ? 1 : page;

            // Add Query
            ConcurrentDictionary<string, ParameterExpression> _parameters = new ConcurrentDictionary<string, ParameterExpression>();
            ConcurrentDictionary<string, MemberAssignment> _bindings = new ConcurrentDictionary<string, MemberAssignment>();
            ConcurrentDictionary<string, Expression<Func<T, T>>> _selectors = new ConcurrentDictionary<string, Expression<Func<T, T>>>();

            // include
            foreach (var include in includes)
            {
                var check = _props.Where(x => x.ToString().ToUpper() == include.ToUpper()).FirstOrDefault();
                if (check != null)
                {
                    searchResults = searchResults.Include(include);
                }
            }


            //Add filters
            searchResults = fields.Aggregate(searchResults, (current, expression) => current.Where(expression));
            if (memberNames == null || memberNames.Count() == 0)
            {
                searchResults = searchResults.Select(x => x);
            }
            else
            {
                var parameterName = typeof(T).FullName;

                var requestName = $"{parameterName}:{string.Join(",", memberNames.OrderBy(x => x))}";
                if (!_selectors.TryGetValue(requestName, out var selector))
                {
                    if (!_parameters.TryGetValue(parameterName, out var parameter))
                    {
                        parameter = Expression.Parameter(typeof(T), typeof(T).Name.ToLowerInvariant());

                        _ = _parameters.TryAdd(parameterName, parameter);
                    }

                    var bindings = memberNames
                        .Select(name =>
                        {
                            var memberName = $"{parameterName}:{name}";
                            if (!_bindings.TryGetValue(memberName, out var binding))
                            {
                                var member = Expression.PropertyOrField(parameter, name);
                                binding = Expression.Bind(member.Member, member);

                                _ = _bindings.TryAdd(memberName, binding);
                            }
                            return binding;
                        });

                    var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
                    selector = Expression.Lambda<Func<T, T>>(body, parameter);

                    _selectors.TryAdd(requestName, selector);
                }
                //get sort field
                searchResults = searchResults.Select(selector);
            }

            if (keySort != null)
            {
                if (sortDir == SortEnum.ASC)
                    searchResults = searchResults.OrderBy(x => EF.Property<object>(x, keySort));
                else
                    searchResults = searchResults.OrderByDescending(x => EF.Property<object>(x, keySort));
            }


            //// Get the search results 
            int count = await searchResults.CountAsync();
            var data = all == false ? await searchResults.Skip(page * pageSize - pageSize).Take(pageSize).ToListAsync() : await searchResults.ToListAsync();
            return new ListResponse<T>(data: data, listHeader: null, count: count);
            //return await searchResults.ToListAsync();
        }

        public async Task<bool> ExitsByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }

            return entity != null;
        }
    }
}
