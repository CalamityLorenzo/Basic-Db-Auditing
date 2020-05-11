using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Entities
{
    public abstract class AuditBaseDb
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}
