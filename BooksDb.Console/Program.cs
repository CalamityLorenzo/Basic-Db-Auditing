using Basic.BooksDb;
using BooksDb.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace BooksDb.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Migrate();
        }

        private static  void Migrate()
        {
            var dbConn = File.ReadAllText("DbConnection.txt");

            var dbOpts = new DbContextOptionsBuilder().UseSqlServer(dbConn);
            using (var ctx = new BooksDbContext(dbOpts.Options))
            {
                ctx.Database.Migrate();
                SeedData sd = new SeedData(ctx);
                sd.Seed(BasicBookData.Books());
            }
        }
    }
}
