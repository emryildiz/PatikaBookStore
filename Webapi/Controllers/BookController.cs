using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Webapi.BookOperations.AddBook;
using Webapi.BookOperations.GetBooks;
using Webapi.DbOperation;
using Webapi.BookOperations.UpdateBook;
using Webapi.BookOperations.GetBookById;

namespace Webapi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {

            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);

        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookById query = new GetBookById(_context);
            var result = query.Handle(id);

            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] AddBookModel newBook)
        {
            AddBookCommand cmd = new AddBookCommand(_context);
            try
            {
                cmd.Model = newBook;
                cmd.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }



            return Ok();


        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand cmd = new UpdateBookCommand(_context);
            try
            {
                cmd.Model = updatedBook;
                cmd.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            var book = _context.Books.SingleOrDefault(x => x.ID == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return Ok();
        }




    }
}