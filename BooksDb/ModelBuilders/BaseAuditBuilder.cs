using BooksDb.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BooksDb.ModelBuilders
{
    // Don't want to ever instatiate.
    abstract class BaseAuditBuilder<T> : IEntityTypeConfiguration<T> where T : AuditBaseDb
    {
        // Want that sweet virtual look up cost.
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.CreatedBy).IsRequired();
            builder.Property(p => p.ModifiedBy).IsRequired();
            builder.Property(p => p.Created).HasDefaultValueSql("getutcdate()");
            builder.Property(p => p.ModifiedBy).HasDefaultValueSql("getutcdate()");
        }

    }
}
