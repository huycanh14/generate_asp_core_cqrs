using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Domain.Common
{
    public class FilterBase<T>
    {
        private readonly PropertyInfo[] _props = typeof(T).GetProperties();
        public int Page { get; set; } = 1;
        public int Rows { get; set; } = 10;
        public string KeySort { get; set; }
        public List<string> MemberNames { get; set; }
        public List<string> Includes { get; set; } = new List<string> { };
        public SortEnum sort { get; set; } = SortEnum.ASC;
        public Dictionary<string, string> _Properties { get; set; } = new Dictionary<string, string> { };
        public List<Expression<Func<T, bool>>> GetFilterWhere()
        {
            var filterExpressions = new List<Expression<Func<T, bool>>>();

            foreach (var item in _Properties)
            {
                var check = _props.Where(x => x.Name.ToString().ToUpper() == item.Key.ToUpper()).FirstOrDefault();
                if (check != null && !string.IsNullOrEmpty(item.Value))
                {
                    var parameter = Expression.Parameter(typeof(T), "p");
                    ConstantExpression right = Expression.Constant(item.Value);
                    Expression left = Expression.PropertyOrField(parameter, check.Name);
                    if (check.PropertyType == typeof(string))
                    {

                        MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var predicate = Expression.Lambda<Func<T, bool>>(
                            Expression.Call(left, method, right),
                            parameter);
                        filterExpressions.Add(predicate);

                    }
                    else
                    {
                        try
                        {
                            var valueToSet = Convert.ChangeType(item.Value, check.PropertyType);
                            right = Expression.Constant(valueToSet);
                            var predicate = Expression.Lambda<Func<T, bool>>(
                                Expression.Equal(left, right),
                                parameter);
                            filterExpressions.Add(predicate);

                        }
                        catch (Exception) { continue; }
                    }

                }
            }
            return filterExpressions;

        }
        public string UnqualifiedFieldName
        {
            get
            {
                var check = _props.Where(x => KeySort != null && x.Name.ToString().ToUpper() == KeySort.ToUpper()).FirstOrDefault();
                if (check == null)
                {
                    KeySort = null;
                }
                else
                {
                    KeySort = check.Name;
                }
                return KeySort;
            }
        }
        public bool All { get; set; } = false;
    }
}
