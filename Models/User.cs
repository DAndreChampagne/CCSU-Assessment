using System;
//using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    
    /// <summary>
    /// 
    /// </summary>
    public class User /*: IdentityUser*/ {
        public int Id { get; set; }

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [NotMapped]
        [Display(Name = "Roles")]
        public string RoleNames { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

}