
using System.Collections.Generic;

namespace Models {

    /// <summary>
    /// 
    /// </summary>    
    public class Rubric {
        public int Id { get; set; }
        
        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public byte[] File { get; set; }

        public virtual List<RubricData> RubricData { get; set; }
    }

}