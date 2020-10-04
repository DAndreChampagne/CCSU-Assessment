
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class Role {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

}