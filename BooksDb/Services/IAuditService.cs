using BooksDb.Entities;

namespace BooksDb.Services
{
    public interface IAuditService
    {
        public void NewAuditInfo(AuditBaseDb entity);
        public void MigrateAudit(AuditBaseDb updated, AuditBaseDb original);
        public void UpdateAuditInfo(AuditBaseDb entity);
    }
}