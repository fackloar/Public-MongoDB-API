namespace MongoDB.DataLayer.Models
{
    public class BooksDatabaseSettings : IBooksDatabaseSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
