namespace MongoDB.BusinessLayer.DTOs
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsRead { get; set; }
        public bool IsRecommended { get; set; }
        public string Description { get; set; }
    }
}
