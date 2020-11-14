using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class Artifact {

        public int Id { get; set; }

        public virtual string RubricId { get; set; }
        public virtual Rubric Rubric { get; set; }
        

        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        public int CRN { get; set; }
        [ForeignKey("CRN")]
        public virtual CourseSection CourseSection { get; set; }


        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Term { get; set; }
        
        [StringLength(10)]
        [Display(Name = "Student ID")]
        public string StudentId { get; set; }

        [StringLength(2)]
        public string Level { get; set; }

        public byte[] File { get; set; }



        public virtual ICollection<Score> Scores { get; set; }
    }

}