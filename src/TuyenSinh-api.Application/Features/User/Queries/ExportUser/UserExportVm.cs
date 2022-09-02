using System;
using System.ComponentModel.DataAnnotations;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Application.Features.User.Queries.ExportUser
{
    public class UserExportVm : CommonExportVm
    {
        [Display(Name = "Code")]
        //[JsonProperty(PropertyName = "Code")]
        public int Id { get; set; }

        [Display(Name = "Họ và tên")]
        //[JsonProperty(PropertyName = "Họ và tên")]
        public string UserName { get; set; }
    }
}
