using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models {

    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>    
    public class Rubric {
        
        [StringLength(2)]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public byte[] File { get; set; }


        public virtual ICollection<RubricCriteria> RubricCriteria { get; set; }

    }

}