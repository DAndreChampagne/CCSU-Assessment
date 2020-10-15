
using System;
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


        public string Name { get; set; }

        [Display(Name = "Score of 4")]
        public string Desciption4 { get; set; }

        [Display(Name = "Score of 3")]
        public string Desciption3 { get; set; }

        [Display(Name = "Score of 2")]
        public string Desciption2 { get; set; }

        [Display(Name = "Score of 1")]
        public string Desciption1 { get; set; }

    }

}