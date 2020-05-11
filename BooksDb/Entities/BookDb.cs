﻿using BooksDb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksDb.Entities
{
    public class BookDb
    {
        public BookDb()
        {
            Reviews = new List<ReviewDb>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Blurb { get; set; }
        public DateTime DatePublished { get; set; }

        public ICollection<ReviewDb> Reviews { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name} {Author} {Pages} {DatePublished.ToShortDateString()}";
        }
    }
}
