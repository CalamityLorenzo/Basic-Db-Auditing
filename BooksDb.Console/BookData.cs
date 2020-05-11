using BooksDb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BooksDb.ConsoleApp
{
    public static class BasicBookData
    {
        public static IEnumerable<Book> Books() => new List<Book>
        {
            new Book("Book One", "Paul Lawrence", 500, "This is the back of the book", DateTime.Parse("01/01/1987")),
            new Book("Book Two", "Morocco Bookface", 183, "What we say about a new tome", DateTime.Parse("03/06/1983")
                        , new List<Review>
                        {
                            new Review("Clare Reviewer", "Written in spite", 8),
                            new Review("Aled Reviews", "Left me cold.", 0),
                            new Review("Terrible-Anon", "Nothing much to see but brilliance.", 18)
                        }),
            new Book("A book in rhyme", "Floral Coral", 100, "All up tin the beats", DateTime.Parse("14/05/2007")
                        , new List<Review>
                        {
                            new Review("Roasted Reviews", "Nice. Weather for ducks", 19),
                            new Review("Salid Khan", "Pablum for the indolent masses.Consume and rejoine", 1),
                        }),
            new Book("Those Were The Days", "Salid Khan", 499, "What words are used for", DateTime.Parse("23/08/1965")
                        , new List<Review>
                        {
                            new Review("Clare Reviewer", "Better than the last one", 12),
                            new Review("Terrible-Anon", "Worst than the first", 2),
                            new Review("Floral Coral", "A thing so mixed as to be the same.", 5),
                        })
        };
    }
}
