using System;
using System.ComponentModel.DataAnnotations;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TuyenSinh_api.Application.Features.TEntity.Queries.GetUsers
{
    public partial class UserVm
    {
        [Display(Name = "Code")]
        //[JsonProperty(PropertyName = "Code")]
        public int Id { get; set; }

        [Display(Name = "Họ và tên")]
        //[JsonProperty(PropertyName = "Họ và tên")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        //[JsonProperty(PropertyName = "Mật khẩu")]
        [JsonIgnore]
        public string Password { get; set; }


    }
}
