
using System.Collections.Generic;

namespace Models {
    
    /// <summary>
    /// 
    /// </summary>
    public class Artifact {
        public int Id { get; set; }

        public virtual School School { get; set; }
        public virtual int SchoolId { get; set; }

        public virtual Rubric Rubric { get; set; }
        public virtual int RubricId { get; set; }


        public string Name { get; set; }

        public string Term { get; set; }
        public string StudentId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string LearningObjective { get; set; }
        public string Level { get; set; }
        public string CRN { get; set; }


        // TODO: decide if we're storing the file in the DB, or if we're using a file system
        // I'm thinking that storing the file in the DB is a better idea, since it offers additional security
        public string FilePath { get; set; }
        public byte[] File { get; set; }


        public virtual ICollection<Rubric> Rubrics { get; set; }
        
    }

}