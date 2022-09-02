using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Entities;

namespace TuyenSinh_api.Domain.Filters
{

    public class UserFilter : FilterBase<User>
    {

        public string UserName { get; set; }

        //public override string UnqualifiedFieldName
        //{
        //    get
        //    {
        //        PropertyInfo[] props = typeof(User).GetProperties();
        //        var check = props.Where(x => KeySort != null && x.Name.ToString().ToUpper() == KeySort.ToUpper()).FirstOrDefault();
        //        if (check == null)
        //        {
        //            KeySort = "Id";
        //        }
        //        else
        //        {
        //            KeySort = check.Name;
        //        }
        //        return KeySort;
        //    }
        //}

        //public override List<Expression<Func<User, bool>>> GetFilterWhere()
        //{
        //    var filterExpressions = new List<Expression<Func<User, bool>>>();
        //    if (!string.IsNullOrEmpty(UserName))
        //    {
        //        filterExpressions.Add(x => x.UserName == UserName);
        //    }
        //    return filterExpressions;
        //}
    }
}
