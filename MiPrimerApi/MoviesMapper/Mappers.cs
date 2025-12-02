using AutoMapper;
using MiPrimerApi.DAL.Models;
using MiPrimerApi.DAL.Models.Dtos;

namespace Api.W.Movies.MoviesMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryCreateUpdateDto>().ReverseMap();
        }
    }
}
