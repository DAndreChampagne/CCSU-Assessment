
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class Score {

        public int Id { get; set; }

        public virtual RubricCriteria RubricCriteria { get; set; }
        [Display(Name = "Rubric Criteria ID")]
        public virtual string RubricCriteriaId { get; set; }

        [Display(Name = "Faculty ID")]
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }

        public virtual Artifact Artifact { get; set; }
        public virtual int ArtifactId { get; set; }
        

        [Display(Name = "Score")]
        public int? ScoreValue { get; set; }


    }

}