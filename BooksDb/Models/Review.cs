using BooksDb.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Models
{
    public class Review
    {
        public Review(Guid id, Guid bookId, string name, string review, short score)
        {
            Id = id;
            BookId = bookId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ReviewBody = review ?? throw new ArgumentNullException(nameof(review));
            Score = score;
        }

        public Review(string name, string review, short score) : this(Guid.Empty, Guid.Empty, name, review, score) { }

        public Guid Id { get; }
        public Guid BookId { get; }
        public string Name { get; }
        public string ReviewBody { get; }
        public short Score { get; }

        public override string ToString()
        {
            return $"{Id} {Name} {Score}";
        }
    }
}
