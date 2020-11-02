
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

        public virtual Rubric Rubric { get; set; }
        public virtual int RubricId { get; set; }

        public virtual Artifact Artifact { get; set; }
        public virtual int ArtifactId { get; set; }
        
        // TODO: normalize this
        
        [Display(Name = "Score 1")]
        public int? Score01 { get; set; }

        [Display(Name = "Score 2")]
        public int? Score02 { get; set; }

        [Display(Name = "Score 3")]
        public int? Score03 { get; set; }
        [Display(Name = "Score 4")]
        public int? Score04 { get; set; }

        [Display(Name = "Score 5")]
        public int? Score05 { get; set; }

        [Display(Name = "Score 6")]
        public int? Score06 { get; set; }

        [Display(Name = "Score 7")]
        public int? Score07 { get; set; }

        [Display(Name = "Score 8")]
        public int? Score08 { get; set; }

        [Display(Name = "Score 9")]
        public int? Score09 { get; set; }

        [Display(Name = "Score 10")]
        public int? Score10 { get; set; }

    }

}