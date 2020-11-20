
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Models {

    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>    
    public class RubricCriteria {
        public int Id { get; set; }
        
        public virtual Rubric Rubric { get; set; }
        public int RubricId { get; set; }

        public string CriteriaText { get; set; }

        public virtual ICollection<RubricCriteriaElement> RubricCriteriaElements { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }

}