using MongoDB.DataLayer.Models;
using MongoDB.Driver;

namespace MongoDB.DataLayer.Interfaces
{
    public interface IBookRepository
    {
        Task CreateBook(Book book);
        Task <List<Book>> GetAllBooks();
        Task<Book> GetBookById(string id);
        Task<List<Book>> GetBooksByDescription(string description);
        Task<ReplaceOneResult> Update(string id, Book book);
        Task Delete(string id);

    }
}
