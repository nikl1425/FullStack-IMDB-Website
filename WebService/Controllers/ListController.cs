using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataService.Objects;
using DataService.Services;
using Microsoft.AspNetCore.Mvc;
using WebService.ObjectDto;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ListController : ControllerBase
    {
        private IUserDataService _dataService;
        private ITitleDataService _titleDataService;
        private IPersonDataService _personDataService;
        private readonly IMapper _mapper;

        public ListController(IUserDataService dataService, ITitleDataService titleDataService, IPersonDataService personDataService, IMapper mapper)
        {
            _dataService = dataService;
            _titleDataService = titleDataService;
            _personDataService = personDataService;
            _mapper = mapper;
        }

        [HttpGet("plist/{listid}")]
        public IActionResult GetPersonBookMarkList(int listid)
        {
            var pbookmarklist = _dataService.GetPersonBookmarkList(listid);
            var pgetbookmarks = _dataService.GetPersonBookmarks(listid);
                
            IList<PersonBookmarkListDto> plistDto = pbookmarklist.Select(x => new PersonBookmarkListDto
            {
                Id = "p"+x.Id,
                UserId = x.UserId,
                ListName = x.ListName,
                Username = _dataService.GetUser(x.UserId).Username,
                Url = "http://localhost:5001/api/user/"+x.UserId //URL to user
            }).ToList();
            
            IList<PersonBookmarkDto> pbookmarkDtos = pgetbookmarks.Select(x => new PersonBookmarkDto
            {
                Id = x.Id,
                List_Id = x.List_Id,
                Person_Id = x.Person_Id,
                PersonName = _personDataService.GetPersonSingle(x.Person_Id).Name,
                Url = "http://localhost:5001/api/name/"+x.Person_Id
            }).ToList();

            return Ok(new {plistDto, pbookmarkDtos});
        }
        
        [HttpGet("tlist/{listid}")]
        public IActionResult GetTitleBookMarkList(int listid)
        {
            //var titleBookmarkList = _dataService.GetTitleBookmarkLists(id);
            var tgetbookmarks = _dataService.GetTitleBookmarks(listid);
            var titleBookmarkList = _dataService.GetTitleBookmarkList(listid);
            
            IList<TitleBookmarkListDTO> titleList = titleBookmarkList.Select(x => new TitleBookmarkListDTO
            {
                Type = "tlist",
                Id = "t"+x.Id,
                UserId = x.UserId,
                Username = _dataService.GetUser(x.UserId).Username,
                ListName = x.ListName,
                Url = "http://localhost:5001/api/user/"+x.UserId //URL to user
            }).ToList();

            var posterTemp = _titleDataService.GetOmdbData("tt11000576").Poster;

            IList<TitleBookmarkDTO> tbookmarkDtos = tgetbookmarks.Select(x => new TitleBookmarkDTO
            {
                Id = x.Id,
                TitleId = x.TitleId,
                ListId = x.ListId,
                Url = "http://localhost:5001/api/title/" + x.TitleId,
                titleName = _titleDataService.GetTitle(x.TitleId).OriginalTitle,
                prodYear = _titleDataService.GetTitle(x.TitleId).StartYear,
                Poster = _titleDataService.GetOmdbData(x.TitleId).Poster ?? posterTemp,
                plot = _titleDataService.GetOmdbData(x.TitleId).Plot
             
            }).ToList();

            return Ok(new {titleList, tbookmarkDtos});
        }
    }
}