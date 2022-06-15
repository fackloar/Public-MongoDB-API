using AutoMapper;
using MongoDB.BusinessLayer.DTOs;
using MongoDB.DataLayer.Models;

namespace MongoDB.BusinessLayer.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Book, BookDTO>();
        }
    }
}
