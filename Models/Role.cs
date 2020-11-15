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
    public class Role : IdentityRole<string> {
        public string Description { get; set; }

        public Role(): base() {}
        public Role(string name, string description = null): base(name) {
            Description = description;
        }

    }

}