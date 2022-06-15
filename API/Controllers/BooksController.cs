using Microsoft.AspNetCore.Mvc;
using MongoDB.API.Responses;
using MongoDB.BusinessLayer.DTOs;
using MongoDB.BusinessLayer.Interfaces;
using MongoDB.DataLayer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET: api/<BooksController>
        [HttpGet]
        public async Task<ActionResult<GetBooksResponse>> GetBooks()
        {
            var response = new GetBooksResponse()
            {
                Books = new List<BookDTO>()
            };
            var books = await _bookService.GetList();
            foreach (var book in books)
            {
                response.Books.Add(book);
            }
            return response;
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookByIdResponse>> GetById(string id)
        {
            var response = await _bookService.GetById(id);
            if (response.Succeed)
            {
                return Ok(response.Result);
            }
            else
            {
                return UnprocessableEntity(response.Failures);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook([FromBody] BookDTO bookDTO)
        {
            var response = await _bookService.Create(bookDTO);
            if (response.Succeed)
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity(response.Failures);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(string id, [FromBody] BookDTO bookDTO)
        {
            await _bookService.Update(id, bookDTO);
            return Ok();
        }

        // DELETE api/<BooksController>/5
        [HttpDelete("{id}")]
        public ActionResult<Book> Delete(string id)
        {
            var foundBook = _bookService.GetById(id);

            if (foundBook == null)
            {
                return NotFound($"Book with Id = {id} not found");
            }

            _bookService.Delete(id);

            return Ok();
        }
    }
}
