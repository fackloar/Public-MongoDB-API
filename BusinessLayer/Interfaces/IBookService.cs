using MongoDB.BusinessLayer.DTOs;
using MongoDB.DataLayer.Models;
using Safe_Development.BusinessLayer.Validation;

namespace MongoDB.BusinessLayer.Interfaces
{
    public interface IBookService
    {
        Task<IList<BookDTO>> GetList();
        Task<IOperationResult<BookDTO>> GetById(string id);
        Task<IOperationResult<BookDTO>> Create(BookDTO bookDTO);
        Task Update (string id, BookDTO bookDTO);
        Task Delete(string id);

    }
}
