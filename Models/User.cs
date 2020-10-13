using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Assessment.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class User : IdentityUser {
        // public int Id { get; set; }

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [NotMapped]
        public string Roles { get; set; }

    }

}