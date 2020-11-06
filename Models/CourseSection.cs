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
    public class CourseSection {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CRN { get; set; }

        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        

        [StringLength(100)]
        public string Name { get; set; }



        
    }

}