using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Webapi.Common;
using Webapi.DbOperation;

namespace Webapi.BookOperations.AddBook
{
    public class AddBookCommand
    {
        public AddBookModel Model { get; set; }
        private readonly BookStoreDbContext _context;
        public AddBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Kitap Zaten Mevcut");
            
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.PageCount = Model.PageCount;
            book.GenreId = Model.GenreId;

            _context.Books.Add(book);
            _context.SaveChanges();
           

        }

    }
    public class AddBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }


}