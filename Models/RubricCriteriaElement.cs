using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Models {

    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>    
    public class RubricCriteriaElement {
        public int Id { get; set; }
        
        public virtual RubricCriteria RubricCriteria { get; set; }
        [Display(Name = "Criteria ID")]
        public int RubricCriteriaId { get; set; }

        [Display(Name = "Criteria Text")]
        public string CriteriaText { get; set; }

        [Range(0, Int32.MaxValue)]
        [Display(Name = "Score Value")]
        public int ScoreValue { get; set; }
    }

}