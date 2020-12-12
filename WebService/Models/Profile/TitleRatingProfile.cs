using AutoMapper;
using DataService.Objects;
using WebService.ObjectDto;

namespace WebService.Models
{
    public class TitleRatingProfile : Profile
    {
        public TitleRatingProfile()
        {
            CreateMap<Title_Rating, TitleRatingDto>();
        }
    }
}