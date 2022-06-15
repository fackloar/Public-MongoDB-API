using MongoDB.BusinessLayer.DTOs;

namespace MongoDB.API.Responses
{
    public class GetBooksResponse
    {
        public List<BookDTO> Books { get; set; }
    }
}
