
using System;

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
        public string Desciption4 { get; set; }
        public string Desciption3 { get; set; }
        public string Desciption2 { get; set; }
        public string Desciption1 { get; set; }

    }

}