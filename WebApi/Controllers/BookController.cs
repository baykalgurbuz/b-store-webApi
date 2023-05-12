using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
         
        };


        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetBookById(int id)
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();
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
            var book = BookList.SingleOrDefault(x=>x.Title==newBook.Title);
            if(book is not null)
                return BadRequest();
            
            BookList.Add(newBook);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id ,[FromBody] Book updateBook)
        {
            var book=BookList.SingleOrDefault(x=>x.Id==id);

            if(book is null)
                BadRequest();
            
            book.GenreId=updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount=updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate=updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title=updateBook.Title != default ? updateBook.Title : book.Title;
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x=>x.Id==id);
            if(book is not null)
                return BadRequest();
            
            BookList.Remove(book);
            return Ok();
        }
    }
}