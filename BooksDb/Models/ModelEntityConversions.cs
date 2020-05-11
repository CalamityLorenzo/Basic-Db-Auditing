using BooksDb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksDb.Models
{
    public static class ModelEntityConversions
    {
        public static BookDb ToDb(this Book @this)
        {
            return new BookDb
            {
                Id = @this.Id,
                Name = @this.Name,
                Author = @this.Author,
                Blurb = @this.Blurb,
                DatePublished = @this.DatePublished,
                Pages = @this.Pages,
                Reviews = @this.Reviews.ToDb().ToList()
            };
        }

        public static IEnumerable<BookDb> ToDb(this IEnumerable<Book> @this)
        {
            foreach (var book in @this)
            {
                yield return book.ToDb();
            }
        }

        public static IEnumerable<ReviewDb> ToDb(this IEnumerable<Review> @this)
        {
            foreach (var item in @this)
            {
                yield return item.ToDb();
            }
        }

        public static ReviewDb ToDb(this Review @this)
        {
            return new ReviewDb
            {
                BookId = @this.BookId,
                Id = @this.Id,
                Name = @this.Name,
                Review = @this.ReviewBody,
                Score = @this.Score
            };
        }

        public static Book ToClient(this BookDb @this)
        {
            return new Book(
                id:@this.Id,
                name:@this.Name,
                author:@this.Author,
                blurb:@this.Blurb,
                pages:@this.Pages,
                datePublished:@this.DatePublished,
                reviews:@this.Reviews.ToClient().ToList()
                );
        }

        public static IEnumerable<Review> ToClient(this IEnumerable<ReviewDb> @this)
        {
            foreach (var item in @this)
                yield return item.ToClient();
        }

        public  static Review ToClient(this ReviewDb @this)
        {
            return new Review(
                id: @this.Id,
                name:@this.Name,
                review:@this.Review,
                bookId:@this.BookId,
                score:@this.Score);
        }
    }
}
