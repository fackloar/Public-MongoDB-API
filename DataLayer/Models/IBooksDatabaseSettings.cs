namespace MongoDB.DataLayer.Models
{
    public interface IBooksDatabaseSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
