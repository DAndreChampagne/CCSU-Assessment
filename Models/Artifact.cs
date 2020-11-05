using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class Artifact {

        public int Id { get; set; }

        public virtual Rubric Rubric { get; set; }

        [Display(Name = "Learning Objective")]
        public virtual string RubricId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Term { get; set; }
        
        [StringLength(10)]
        [Display(Name = "Student ID")]
        public string StudentId { get; set; }

        [StringLength(10)]
        [Display(Name = "Faculty ID")]
        public string FacultyId { get; set; }

        [StringLength(2)]
        public string Level { get; set; }

        [StringLength(10)]
        public string CRN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>        
        public byte[] File { get; set; }



        public virtual ICollection<Rubric> Rubrics { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }

}