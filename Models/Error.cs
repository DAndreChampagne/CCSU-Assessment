using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Assessment.Models {
    
    // TODO: finish XML comments
    /// <summary>
    /// 
    /// </summary>
    public class Error {
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }

        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public int HResult { get; set; }
    }
}