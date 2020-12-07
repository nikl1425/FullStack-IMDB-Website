using AutoMapper;
using DataService.Objects;
using WebService.ObjectDto;

namespace WebService.Models
{
    public class Omdb_dataProfile : Profile
    {
        public Omdb_dataProfile()
        {
            CreateMap<OmdbData, Omdb_dataDto>();
        }
    }
}