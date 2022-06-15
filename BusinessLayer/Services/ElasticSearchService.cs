using Microsoft.Extensions.Options;
using MongoDB.BusinessLayer.Interfaces;
using MongoDB.DataLayer.Models;
using Nest;

namespace MongoDB.BusinessLayer.Services
{
    public class ElasticSearchService : IElasticSearchService
    {
        private ElasticClient _elasticClient;

        public ElasticSearchService(IOptions<ElasticSearchSettings> options)
        {
            ConnectionSettings connectionSettings = new ConnectionSettings(new Uri(options.Value.Uri))
                .DefaultIndex(options.Value.DefaultIndex);

            _elasticClient = new ElasticClient(connectionSettings);
        }
        public async Task<ISearchResponse<Book>> SearchByTitle(string title)
        {
            return await _elasticClient
                .SearchAsync<Book>(s => s
                    .Index("books")
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.Title)
                                    .Query(title))));
        }

        public async Task<ISearchResponse<Book>> SearchByDescription(string description)
        {
            return await _elasticClient
                .SearchAsync<Book>(s => s
                    .Index("books")
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.Description)
                                    .Query(description))));
        }

        public async Task<ISearchResponse<Book>> SearchByAuthor(string author)
        {
            return await _elasticClient
                .SearchAsync<Book>(s => s
                    .Index("books")
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.Author)
                                    .Query(author))));
        }

        public async Task<ISearchResponse<Book>> SearchByRecommendation()
        {
            return await _elasticClient
                .SearchAsync<Book>(s => s
                    .Index("books")
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.IsRecommended)
                                    .Query("true"))));
        }
    }
}
