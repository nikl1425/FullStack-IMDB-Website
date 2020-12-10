using AutoMapper;
using DataService.Objects;
using WebService.ObjectDto;

namespace WebService.Models
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movies, MoviesDto>();
        }
    }
}