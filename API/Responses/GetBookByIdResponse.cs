using MongoDB.BusinessLayer.DTOs;

namespace MongoDB.API.Responses
{
    public class GetBookByIdResponse
    {
        public BookDTO Book { get; set; }
    }
}
