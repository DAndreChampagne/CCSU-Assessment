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

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        public virtual Rubric Rubric { get; set; }
        public virtual int RubricId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Term { get; set; }
        
        [StringLength(10)]
        public string StudentId { get; set; }


        [StringLength(100)]
        [Display(Name = "Learning Objective")]
        public string LearningObjective { get; set; }

        [StringLength(2)]
        public string Level { get; set; }

        [StringLength(10)]
        public string CRN { get; set; }


        // TODO: decide if we're storing the file in the DB, or if we're using a file system
        // I'm thinking that storing the file in the DB is a better idea, since it offers additional security
        [StringLength(256)]
        [Display(Name = "File Path")]
        public string FilePath { get; set; }
        
        public byte[] File { get; set; }



        public virtual ICollection<Rubric> Rubrics { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }

}