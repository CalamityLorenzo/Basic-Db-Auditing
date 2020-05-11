using BooksDb.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Repositories
{
    public abstract class BaseAuditRepository
    {
        private readonly string userName;

        // user who has made the requests.
        public BaseAuditRepository(string userName)
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

        protected void NewAuditInfo(AuditBaseDb entity)
        {
            var cDate = DateTime.UtcNow;
            NewAuditInfo(entity, cDate);
            UpdateAuditInfo(entity, cDate);
        }

        protected void NewAuditInfo(IEnumerable<AuditBaseDb> entities)
        {
            var cDate = DateTime.UtcNow;
            foreach (var entity in entities )
            {
                NewAuditInfo(entity,cDate);
                UpdateAuditInfo(entity, cDate);
            }
        }

        private void NewAuditInfo(AuditBaseDb entity, DateTime utcDatePlease)
        {
            entity.CreatedBy = userName;
            entity.Created = utcDatePlease;
        }

        protected void UpdateAuditInfo(AuditBaseDb entity)
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
