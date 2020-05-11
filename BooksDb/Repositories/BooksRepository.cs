using BooksDb.Entities;
using BooksDb.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BooksDb.Repositories;

namespace Basic.BooksDb.Repositories
{
    public class BooksRepository : BaseAuditRepository
    {
        private readonly BooksDbContext _ctx;

        public BooksRepository(BooksDbContext ctx, string userName) : base(userName)
        {
            this._ctx = ctx;
        }

        /// <summary>
        /// WE are addding a graph, so reviews can also be added as well..
        /// </summary>
        /// <param name="newBook"></param>
        public Book AddBook(Book newBook)
        {
            var newBookDb = newBook.ToDb();
            this.NewAuditInfo(newBookDb);
            this.NewAuditInfo(newBookDb.Reviews);

            var saved = this._ctx.Add(newBookDb);
            _ctx.SaveChanges();
            return saved.Entity.ToClient();
        }

        public Book GetBook(Guid id)
        {
            return _ctx.Books.AsNoTracking().First(p => p.Id == id).ToClient();
        }

        public Review AddBookReview(Guid Id, Review review)
        {
            var newReview = review.ToDb();
            newReview.BookId = Id;
            this.NewAuditInfo(newReview);
            var reviewTracking = this._ctx.Reviews.Add(newReview);
            _ctx.SaveChanges();
            return reviewTracking.Entity.ToClient();

        }

        /// <summary>
        /// Updating a book, means updating the reviews too.
        /// So if the review is MISSING in the client version
        /// It is DELETED in the Server version.
        /// </summary>
        /// <param name="book"></param>
        public void UpdateBook(Book book)
        {
            var dbBook = book.ToDb();
            // update all the reviews to have at least the correct bookId
            // not doing this means NEW reviews are skipped.
            // Lets also sneak in the audit Updates
            var allReviews = _ctx.Reviews.Where(r => r.BookId == dbBook.Id).ToList();
            dbBook.Reviews = dbBook.Reviews.Select(r =>
            {
                r.BookId = book.Id;
                if (r.Id != Guid.Empty)
                {
                    this.MigrateAudit(r, allReviews.First(a => a.Id == r.Id));
                    this.UpdateAuditInfo(r);
                }
                else NewAuditInfo(r);
                return r;
            }).ToList();

            UpdateAuditInfo(dbBook);
            // Fetch all the original reviews and find out if any are missing.
            // These missing ones are removed.
            var removedReviews = allReviews.Where(a => !dbBook.Reviews.Any(db => db.Id != a.Id)).ToList();
            _ctx.Reviews.RemoveRange(removedReviews);

            _ctx.Books.Update(dbBook);
            _ctx.SaveChanges();
        }

        public void DropBook(Guid bookId)
        {
            var book = _ctx.Books.First(p => p.Id == bookId);
            _ctx.Books.Remove(book);
            _ctx.SaveChanges();
        }

        public void DropBook(Book book)
        {
            this.DropBook(book.Id);
        }

        /// <summary>
        /// This updates the book...
        /// </summary>
        /// <param name="ReviewId"></param>
        public void DropReview(Guid ReviewId)
        {

            var review = _ctx.Reviews.Include(p => p.Book).AsNoTracking().First(p => p.Id == ReviewId);

            this.UpdateAuditInfo(review.Book);

            _ctx.Reviews.Remove(review);
            _ctx.SaveChanges();
        }

        public void DropReview(Review review)
        {
            this.DropReview(review.Id);
        }

    }
}
