using BooksDb.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksDb.Services
{
    class AuditServiceForType<BaseDbEntity, T> : IStratifiedAuditService<BaseDbEntity, T>
        where BaseDbEntity : AuditBaseDb, IEntityId<T>
    {
        private readonly string userName;

        public AuditServiceForType(string userName, Func<BaseDbEntity, AuditBaseDb> originalItem, Func<BaseDbEntity, bool> IsNewItem)
        {
            this.userName = userName;
            OriginalItem = originalItem;
            this.IsNewItem = IsNewItem;
        }

        private Func<BaseDbEntity, AuditBaseDb> OriginalItem { get; }
        private Func<BaseDbEntity, bool> IsNewItem { get; }

        public void SetAuditInfo(BaseDbEntity entity)
        {
            if (IsNewItem(entity))
            {
                NewAuditInfo(entity);
            }
            else
            {
                MigrateAudit(entity, OriginalItem(entity));
            }
        }

        public void SetAuditInfo(AuditBaseDb updatedEntity, AuditBaseDb originalEntity)
        {
            MigrateAudit(updatedEntity, originalEntity);
            UpdateAuditInfo(updatedEntity);
        }

        public void SetAuditInfo(IEnumerable<BaseDbEntity> updatedEnities, IEnumerable<BaseDbEntity> originalEntities)
        {
            foreach(var entity in updatedEnities)
            {
                var original = originalEntities.FirstOrDefault(i => i.Id.Equals(entity.Id));
                if (original != null)
                {
                    SetAuditInfo(entity, original);
                }
                else
                    SetAuditInfo(entity);
            }
        }

        public void SetAuditInfo(IEnumerable<BaseDbEntity> entities)
        {
            this.NewAuditInfo(entities);
        }

        private void MigrateAudit(AuditBaseDb updated, AuditBaseDb original)
        {
            updated.Created = original.Created;
            updated.CreatedBy = original.CreatedBy;
        }

        private void NewAuditInfo(AuditBaseDb entity)
        {
            var cDate = DateTime.UtcNow;
            NewAuditInfo(entity, cDate);
            UpdateAuditInfo(entity, cDate);
        }

        private void NewAuditInfo(IEnumerable<AuditBaseDb> entities)
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
            entity.CreatedBy = this.userName;
            entity.Created = utcDatePlease;
        }

        private void UpdateAuditInfo(AuditBaseDb entity)
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
