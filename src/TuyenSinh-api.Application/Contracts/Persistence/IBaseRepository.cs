using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;

namespace TuyenSinh_api.Application.Contracts.Persistence
{
    public interface IBaseRepository<T> where T : class
    {
        /***
         * Lớp cơ bản của Repository gồm các phương thức hay dùng đến.
         * Sử dụng GENERIC REPOSITORY: cho phép chúng ta định nghĩa một kiểu dữ liệu hoặc lớp mà không cần quan tâm đến kiểu dữ liệu chính xác của nó là gì => cho phép chúng ta định nghĩa một data structure dùng chung
         */
        Task<IEnumerable<T>> GetAllAsync();
        //Task<IEnumerable<T>> GetAllFilterAsync(
        //    List<Field<T>> fields,
        //    IEnumerable<string> memberNames,
        //    T searchFilters,
        //    SortEnum sortDir = SortEnum.ASC,
        //    int pageSize = 10,
        //    int page = 1
        //);
        Task<ListResponse<T>> GetAllFilterAsync(
            List<Expression<Func<T, bool>>> fields,
            List<string> memberNames,
            string keySort,
            List<string> includes,
            SortEnum sortDir = SortEnum.ASC,
            int pageSize = 10,
            int page = 1,
            bool all = false
        );
        Task<T> GetByIdAsync(int id);
        Task<bool> ExitsByIdAsync(int id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
