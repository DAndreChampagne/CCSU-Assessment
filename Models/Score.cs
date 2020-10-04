
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

        public virtual User User { get; set; }
        public virtual int UserId { get; set; }

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        public virtual Rubric Rubric { get; set; }
        public virtual int RubricId { get; set; }

        public virtual Artifact Artifact { get; set; }
        public virtual int ArtifactId { get; set; }
        
        // TODO: normalize this
        public int? Score01 { get; set; }
        public int? Score02 { get; set; }
        public int? Score03 { get; set; }
        public int? Score04 { get; set; }
        public int? Score05 { get; set; }
        public int? Score06 { get; set; }
        public int? Score07 { get; set; }
        public int? Score08 { get; set; }
        public int? Score09 { get; set; }
        public int? Score10 { get; set; }

    }

}