using Basic.BooksDb;
using BooksDb.Entities;
using BooksDb.Models;
using BooksDb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksDb.Repositories
{
    public class SeedData
    {
        private readonly BooksDbContext ctx;
        private readonly AuditService auditService;

        public SeedData(BooksDbContext ctx, AuditService auditService)
        {
            this.ctx = ctx;
            this.auditService = auditService;
        }

        public void Seed(IEnumerable<Book> boooooks)
        {
            // Don't add if the books are already theree...
            if (ctx.Books.Count() > 0) return;

            // Ensure it's a concrete instance.
            var dbBooks = boooooks.ToDb().ToList();
            // Book and reviews
            foreach (var dbBook in dbBooks)
            {
                auditService.NewAuditInfo(dbBook);
                auditService.NewAuditInfo(dbBook.Reviews);
            }
            ctx.AddRange(dbBooks);
            ctx.SaveChanges();
        }
    }
}
