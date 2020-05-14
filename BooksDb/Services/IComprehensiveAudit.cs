using BooksDb.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Services
{
    interface IStratifiedAuditService<BaseDbEntity, T> where BaseDbEntity : AuditBaseDb, IEntityId<T>
    {
        public void SetAuditInfo(BaseDbEntity entity);
        public void SetAuditInfo(AuditBaseDb updatedEntity, AuditBaseDb originalEntity);
        public void SetAuditInfo(IEnumerable<BaseDbEntity> updatedEnities, IEnumerable<BaseDbEntity> originalEntities);
        public void SetAuditInfo(IEnumerable<BaseDbEntity> entities);
    }

}
