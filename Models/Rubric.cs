
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models {

    /// <summary>
    /// 
    /// </summary>    
    public class Rubric {
        public int Id { get; set; }
        
        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        [StringLength(2)]
        public string Code { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Data { get; set; }

        public byte[] File { get; set; }

        public virtual List<RubricData> RubricData { get; set; }
    }

}