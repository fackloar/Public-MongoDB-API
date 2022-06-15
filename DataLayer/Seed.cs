using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.DataLayer.Models;
using MongoDB.Driver;
using Nest;

namespace MongoDB.DataLayer
{
    public class Seed
    {
        private IMongoCollection<Book> _booksCollection;
        private List<Book> _initialBooks;
        private IElasticClient _elasticClient;
        public Seed(IOptions<BooksDatabaseSettings> options, IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
            var mongoClient = new MongoClient(
                options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _booksCollection = mongoDatabase.GetCollection<Book>(
                options.Value.BooksCollectionName);
            // это единственный способ который я придумал как можно удобно обновить локальную датабазу - удаляем ее и заполняем заного, внеся все обновления внутри этого класса
            var filter = Builders<Book>.Filter.Exists("Id", true);
            _booksCollection.DeleteMany(filter); 
            var books = new List<Book>()
                {
                    new Book { Author = "George Orwell", Title = "1984", Id = "625627d6a3a0512cc2f1b919", IsRead = true, IsRecommended = true, Description = "Antiutopian past where your every step is watched by the Big Brother" },
                    new Book { Author = "Oldos Hucksley", Title = "Brave New World", Id = "625627d6a3a0512cc2f1b920", IsRead = true, IsRecommended = true, Description = "Antiutopian future where all people have is pleasure" },
                    new Book { Author = "Chuck Palahniuk", Title = "Fight Club", Id = "625627d6a3a0512cc2f1b921", IsRead = true, IsRecommended = true, Description = "A book about two people trying to change the world for better or worse"},
                    new Book { Author = "Leo Tolstoy", Title= "War and Peace", Id= "625627d6a3a0512cc2f1b922", IsRead = false, IsRecommended = false, Description = "A monumental work that perfectly portrays a whole epoch"},
                };
            _initialBooks = books;
        }

        public async Task Initialize()
        {
            await _booksCollection.InsertManyAsync(_initialBooks);
            foreach (var book in _initialBooks)
            {
                await _elasticClient.IndexAsync<Book>(book, x => x.Index("books"));
            }
        }
    }
}
