using System;
using System.Collections.Generic;

namespace TuyenSinh_api.Domain.Entities
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
