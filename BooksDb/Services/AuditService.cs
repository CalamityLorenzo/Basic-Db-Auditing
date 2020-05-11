using BooksDb.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Services
{
    /// <summary>
    /// Handles the management of Audit information
    /// for a db entity.
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly string userName;

        public AuditService(string userName)
        {
            this.userName = userName;
        }

        /// <summary>
        ///  Moves the original Created data over to the updated entity.
        /// </summary>
        /// <param name="updated"></param>
        /// <param name="original"></param>
        public void MigrateAudit(AuditBaseDb updated, AuditBaseDb original)
        {
            updated.Created = original.Created;
            updated.CreatedBy = original.CreatedBy;
        }

        public void NewAuditInfo(AuditBaseDb entity)
        {
            var cDate = DateTime.UtcNow;
            NewAuditInfo(entity, cDate);
            UpdateAuditInfo(entity, cDate);
        }

        public void NewAuditInfo(IEnumerable<AuditBaseDb> entities)
        {
            var cDate = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                NewAuditInfo(entity, cDate);
                UpdateAuditInfo(entity, cDate);
            }
        }

        private void NewAuditInfo(AuditBaseDb entity, DateTime utcDatePlease)
        {
            entity.CreatedBy = userName;
            entity.Created = utcDatePlease;
        }

        public void UpdateAuditInfo(AuditBaseDb entity)
        {
            var cDate = DateTime.UtcNow;
            UpdateAuditInfo(entity, cDate);
        }

        private void UpdateAuditInfo(AuditBaseDb entity, DateTime utcDatePlease)
        {
            entity.Modified = utcDatePlease;
            entity.ModifiedBy = userName;
        }
    }
}
