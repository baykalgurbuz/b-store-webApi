using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.GetDetailQuery;
using WebApi.BookOperations.UpdateBook;
using WebApi.DbOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
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
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);

            // var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            // return book;
        }

        //  [HttpGet]
        // public Book Get([FromQuery]string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            // var book = _context.Books.SingleOrDefault(x=>x.Title==newBook.Title);
            // if(book is not null)
            //     return BadRequest();

            // _context.Books.Add(newBook);
            // _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();

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
             try
             {
                DeleteBookCommand command  =new DeleteBookCommand(_context);
                command.BookId=id;
                command.Handle();
             }
             catch (Exception ex)
             {
                return BadRequest(ex.Message);
             }
             return Ok();
        }
    }
}