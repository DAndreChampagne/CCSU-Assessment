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


        [StringLength(100)]
        public string Name { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; } = new List<string>();

        [NotMapped]
        [Display(Name = "Roles")]
        public string RoleName { get; set; }

    }

}