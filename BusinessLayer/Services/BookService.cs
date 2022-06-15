using AutoMapper;
using FluentValidation;
using MongoDB.BusinessLayer.DTOs;
using MongoDB.BusinessLayer.Interfaces;
using MongoDB.DataLayer.Interfaces;
using MongoDB.DataLayer.Models;
using MongoDB.Driver;
using Safe_Development.BusinessLayer.Validation;

namespace MongoDB.BusinessLayer.Services
{
    public class BookService : IBookService
    {
        private IBookRepository _repository;
        private IValidator<BookDTO> _validator;
        private IMapper _mapper;

        public BookService(IBookRepository repository, IValidator<BookDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        public async Task<IOperationResult<BookDTO>> Create(BookDTO bookDTO)
        {
            var check = await _validator.ValidateAsync(bookDTO);
            if (!check.IsValid)
            {
                var failures = check.Errors.Select(e => new OperationFailure
                {
                    PropertyName = e.PropertyName,
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage
                }).ToArray();
                return new OperationResult<BookDTO>(failures);
            }
            else
            {
                var bookToCreate = _mapper.Map<Book>(bookDTO);
                await _repository.CreateBook(bookToCreate);
                return new OperationResult<BookDTO>(bookDTO);
            }
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }

        public async Task<IOperationResult<BookDTO>> GetById(string id)
        {
            var bookGot = await _repository.GetBookById(id);
            var bookDTO = _mapper.Map<BookDTO>(bookGot);
            var check = await _validator.ValidateAsync(bookDTO);
            if (!check.IsValid)
            {
                var failures = check.Errors.Select(e => new OperationFailure
                {
                    PropertyName = e.PropertyName,
                    Code = e.ErrorCode,
                    Message = e.ErrorMessage
                }).ToArray();
                return new OperationResult<BookDTO>(failures);
            }
            else
            {
                return new OperationResult<BookDTO>(bookDTO);
            }
        }

        public async Task<IList<BookDTO>> GetList()
        {
            var books = await _repository.GetAllBooks();
            List<BookDTO> bookDTOs = _mapper.Map<List<BookDTO>>(books);
            return bookDTOs;
        }

        public async Task Update(string id, BookDTO bookDTO)
        {
            var bookToUpdate = await _repository.GetBookById(id);
            var updatedBook = _mapper.Map<Book>(bookDTO);
            bookToUpdate = updatedBook;
            await _repository.Update(id, updatedBook);
        }
    }
}
