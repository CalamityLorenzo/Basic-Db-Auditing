using BooksDb.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Services
{
    public class AuditServiceWithDateService : IAuditService
    {
        private readonly string userName;
        private readonly Func<DateTime> dateTimeService;

        public AuditServiceWithDateService(string userName, Func<DateTime> dateTimeService)
        {
            this.userName = userName;
            this.dateTimeService = dateTimeService;
        }
        public void MigrateAudit(AuditBaseDb updated, AuditBaseDb original)
        {
            updated.Created = original.Created;
            updated.CreatedBy = original.CreatedBy;
        }

        public void NewAuditInfo(AuditBaseDb entity)
        {
            var date = this.dateTimeService();
            NewAuditInfo(entity, date);
            UpdateAuditInfo(entity, date);
        }

        public void UpdateAuditInfo(AuditBaseDb entity)
        {
            var date = this.dateTimeService();
            UpdateAuditInfo(entity, date);
        }

        private void UpdateAuditInfo(AuditBaseDb entity, DateTime utcDatePlease)
        {
            entity.Modified = utcDatePlease;
            entity.ModifiedBy = userName;
        }

        private void NewAuditInfo(AuditBaseDb entity, DateTime utcDatePlease)
        {
            entity.CreatedBy = userName;
            entity.Created = utcDatePlease;
        }

    }
}
