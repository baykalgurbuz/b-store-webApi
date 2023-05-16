using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController (BookStoreDbContext context)
        {
            _context=context;
        }
        // private static List<Book> BookList = new List<Book>(){
        //    new Book
        //              {
        //                  Id = 1,
        //                  Title = "Böyle Söyledi Zerdüst",
        //                  GenreId = 1,
        //                  PageCount = 350,
        //                  PublishDate = new DateTime(2001, 06, 12)
        //              },
        //              new Book
        //              {
        //                  Id = 2,
        //                  Title = "Savas Sanatı",
        //                  GenreId = 2,
        //                  PageCount = 150,
        //                  PublishDate = new DateTime(2000, 06, 12)
        //              },
        //              new Book
        //              {
        //                  Id = 3,
        //                  Title = "Yer Altindan Notlar",
        //                  GenreId = 2,
        //                  PageCount = 250,
        //                  PublishDate = new DateTime(2003, 06, 12)
        //              }
        // };


        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetBookById(int id)
        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        //  [HttpGet]
        // public Book Get([FromQuery]string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x=>x.Title==newBook.Title);
            if(book is not null)
                return BadRequest();
            
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id ,[FromBody] Book updateBook)
        {
            var book=_context.Books.SingleOrDefault(x=>x.Id==id);

            if(book is null)
                BadRequest();
            
            book.GenreId=updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount=updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate=updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title=updateBook.Title != default ? updateBook.Title : book.Title;
            
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x=>x.Id==id);
            if(book is not null)
                return BadRequest();
            
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}