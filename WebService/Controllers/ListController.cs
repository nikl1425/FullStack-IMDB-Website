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
        private readonly IMapper _mapper;

        public ListController(IUserDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
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
                Url = "http://localhost:5001/api/user/"+x.UserId //URL to user
            }).ToList();
            
            IList<PersonBookmarkDto> pbookmarkDtos = pgetbookmarks.Select(x => new PersonBookmarkDto
            {
                Id = x.Id,
                List_Id = x.List_Id,
                Person_Id = x.Person_Id,
                Url = "http://localhost:5001/api/name/"+x.Person_Id
            }).ToList();

            return Ok(new {plistDto, pbookmarkDtos});
        }
        
        [HttpGet("tlist/{listid}")]
        public IActionResult GetTitleBookMarkList(int listid)
        {
            var tbookmarklist = _dataService.GetTitleBookmarkLists(listid);
            var tgetbookmarks = _dataService.GetTitleBookmarks(listid);
                
            IList<TitleBookmarkListDTO> tlistDto = tbookmarklist.Select(x => new TitleBookmarkListDTO
            {
                Id = "t"+x.Id,
                UserId = x.UserId,
                ListName = x.ListName,
                Url = "http://localhost:5001/api/user/"+x.UserId //URL to user
            }).ToList();
            
            IList<TitleBookmarkDTO> tbookmarkDtos = tgetbookmarks.Select(x => new TitleBookmarkDTO
            {
             Id = x.Id,
             TitleId = x.TitleId,
             ListId = x.ListId,
             Url = "http://localhost:5001/api/title/" + x.TitleId 
            }).ToList();

            return Ok(new {tlistDto, tbookmarkDtos});
        }
    }
}