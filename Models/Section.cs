using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    
    /// <summary>
    /// 
    /// </summary>
    public class Section {
        public int Id { get; set; }

        public virtual Session Session { get; set; }
        public virtual int SessionId { get; set; }

        public string Name { get; set; }
        public int CRN { get; set; }

    }

}