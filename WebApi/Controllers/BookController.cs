using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
         
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
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

       

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

 
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