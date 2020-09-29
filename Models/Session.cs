using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment.Models {
    
    /// <summary>
    /// 
    /// </summary>
    public enum Semester {
        Fall = 10,
        Spring = 40,
    }


    /// <summary>
    /// 
    /// </summary>
    public class Session {
        public int Id { get; set; }

        [ScaffoldColumn(true)]
        [NotMapped]
        public string Code { get { return String.Concat(Year, (int)Semester); } }

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        [Range(2000, 2999)]
        public int Year { get; set; }

        public Semester Semester { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
    }

}