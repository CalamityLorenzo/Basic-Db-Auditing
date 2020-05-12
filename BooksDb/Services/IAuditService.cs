using BooksDb.Entities;
using System.Collections.Generic;

namespace BooksDb.Services
{
    public interface IAuditService
    {
        public void NewAuditInfo(AuditBaseDb entity);
        public void NewAuditInfo(IEnumerable<AuditBaseDb> entity);
        public void MigrateAudit(AuditBaseDb updated, AuditBaseDb original);
        public void UpdateAuditInfo(AuditBaseDb entity);
    }
}