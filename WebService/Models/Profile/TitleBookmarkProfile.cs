using AutoMapper;
using DataService.Objects;
using WebService.ObjectDto;

namespace WebService.Models
{
    public class TitleBookmarkProfile : Profile
    {
        public TitleBookmarkProfile()
        {
            CreateMap<Title_Bookmark, TitleBookmarkDTO>();
        }
    }
}