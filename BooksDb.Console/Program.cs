using Basic.BooksDb;
using Basic.BooksDb.Repositories;
using BooksDb.Models;
using BooksDb.Repositories;
using BooksDb.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;

namespace BooksDb.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Migrate();
            DeleteReview(Guid.Parse("86fa42a5-d1ff-49df-960f-b43cdb5949cb"));

        }

        private static  void Migrate()
        {
            var dbConn = File.ReadAllText("DbConnection.txt");
            var dbOpts = new DbContextOptionsBuilder().UseSqlServer(dbConn);
            using (var ctx = new BooksDbContext(dbOpts.Options))
            {
                ctx.Database.Migrate();
                var aservice = new Services.AuditService("Seed Data");
                SeedData sd = new SeedData(ctx, aservice);
                sd.Seed(BasicBookData.Books());
            }
        }

        private static void DeleteReview(Guid delReviewId)
        {
            var dbConn = File.ReadAllText("DbConnection.txt");
            var dbOpts = new DbContextOptionsBuilder().UseSqlServer(dbConn);
            using (var ctx = new BooksDbContext(dbOpts.Options))
            {
                var auditService = new AuditServiceWithDateService("Paul Lawrence", ()=>DateTime.Parse("30/09/1978"));
                var bookRepo = new BooksRepository(ctx, auditService);
                 bookRepo.DropReview(delReviewId);
            }
        }
    }
}
