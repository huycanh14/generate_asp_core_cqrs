using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using TuyenSinh_api.Application.Features.Common.Commands.Update;

namespace TuyenSinh_api.Application.Features.User.Commands.UpdateUser
{
    public class UserUpdateDto : CommonUpdateDto
    {
        [Display(Name = "Code")]
        public int Id { get; set; }

        [Display(Name = "Họ và tên")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [JsonIgnore]
        public string Password { get; set; }

        [Display(Name = "Quyền")]
        [JsonIgnore]
        public int RoldId { get; set; }
    }
}
