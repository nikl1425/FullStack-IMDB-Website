using AutoMapper;
using DataService.Objects;
using WebService.ObjectDto;

namespace WebService.Models
{
    public class TitleBookmarkListProfile : Profile
    {
        public TitleBookmarkListProfile()
        {
            CreateMap<Title_Bookmark_List, TitleBookmarkListDTO>();
        }
    }
}