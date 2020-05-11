using Basic.BooksDb;
using BooksDb.Entities;
using BooksDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksDb.Repositories
{
    public class SeedData
    {
        private readonly BooksDbContext ctx;
        
        public SeedData(BooksDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Seed(IEnumerable<Book> boooooks)
        {
            // Don't add if the books are already theree...
            if (ctx.Books.Count() > 0) return;
            var dbBooks = boooooks.ToDb();
            ctx.AddRange(dbBooks);
            ctx.SaveChanges();
        }
    }
}
