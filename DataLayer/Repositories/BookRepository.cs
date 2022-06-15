using MongoDB.DataLayer.Interfaces;
using MongoDB.DataLayer.Models;
using MongoDB.Driver;
using Nest;

namespace MongoDB.DataLayer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<Book> _books;

        public BookRepository(IBooksDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }
        public async Task CreateBook(Book book)
        {
            await _books.InsertOneAsync(book);
        }
        public async Task<List<Book>> GetAllBooks()
        {
            return await _books.Find(book => true).ToListAsync();
        }

        public async Task<Book> GetBookById(string id)
        {
            return await _books.Find(book => book.Id == id).SingleOrDefaultAsync();
        }

        public Task<List<Book>> GetBooksByDescription(string description)
        {
            throw new NotImplementedException();
        }
        public async Task<ReplaceOneResult> Update(string id, Book book)
        {
            return await _books.ReplaceOneAsync(book => book.Id == id, book);
        }
        public async Task Delete(string id)
        {
            await _books.DeleteOneAsync(book => book.Id == id);
        }


    }
}
