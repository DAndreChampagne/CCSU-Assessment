using System;
//using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models {
    
    /// <summary>
    /// 
    /// </summary>
    public class User /*: IdentityUser*/ {
        public int Id { get; set; }

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

}