using MongoDB.DataLayer.Models;
using Nest;

namespace MongoDB.BusinessLayer.Interfaces
{
    public interface IElasticSearchService
    {
        Task<ISearchResponse<Book>> SearchByTitle(string title);
        Task<ISearchResponse<Book>> SearchByDescription(string description);
        Task<ISearchResponse<Book>> SearchByAuthor(string author);
        Task<ISearchResponse<Book>> SearchByRecommendation();
    }
}
