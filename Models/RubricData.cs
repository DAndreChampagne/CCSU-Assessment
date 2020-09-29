
namespace Assessment.Models {

    /// <summary>
    /// 
    /// </summary>    
    public class RubricData {
        public int Id { get; set; }
        
        public virtual Rubric Rubric { get; set; }
        public int RubricId { get; set; }

    }

}