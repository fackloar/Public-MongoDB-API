using Microsoft.AspNetCore.Mvc;
using MongoDB.API.Responses;
using MongoDB.BusinessLayer.DTOs;
using MongoDB.BusinessLayer.Interfaces;
using MongoDB.DataLayer.Models;
using Nest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticSearchController : ControllerBase
    {
        private readonly IElasticSearchService _elasticSearch;
        public ElasticSearchController(IElasticSearchService elasticSearch)
        {
            _elasticSearch = elasticSearch;
        }

        [HttpGet("search/title/{title}")]
        public async Task<IActionResult> GetByTitle(string title)
        {
            ISearchResponse<Book> response = await _elasticSearch.SearchByTitle(title);

            return Ok(response.Documents);
        }

        [HttpGet("search/author/{author}")]
        public async Task<IActionResult> GetByAuthor(string author)
        {
            ISearchResponse<Book> response = await _elasticSearch.SearchByAuthor(author);

            return Ok(response.Documents);
        }
        [HttpGet("search/description/{description}")]

        public async Task<IActionResult> GetByDescription(string description)
        {
            ISearchResponse<Book> response = await _elasticSearch.SearchByDescription(description);

            return Ok(response.Documents);
        }
        [HttpGet("search/recommendation")]

        public async Task<IActionResult> GetByReccomendation()
        {
            ISearchResponse<Book> response = await _elasticSearch.SearchByRecommendation();

            return Ok(response.Documents);
        }
    }
}