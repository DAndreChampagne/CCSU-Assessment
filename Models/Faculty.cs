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
    public class Faculty {
        public int Id { get; set; }

        public virtual Rubric Rubric { get; set; }
        public string RubricId { get; set; }
        

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual ICollection<CourseSection> CourseSections { get; set; }

    }

}