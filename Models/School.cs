
using System.ComponentModel.DataAnnotations;

namespace Assessment.Models {
    
    /// <summary>
    /// 
    /// </summary>
    public class School {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}