using System;
//using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models {
    
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
        public string RoleNames { 
            get {
                var result = String.Empty;
                foreach (var role in UserRoles) {
                    result += role.Role.Name + ", ";
                }

                if (result?.Length > 2) {
                    result = result.Substring(0, result.Length-2);
                }

                return result;
            }
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }

}