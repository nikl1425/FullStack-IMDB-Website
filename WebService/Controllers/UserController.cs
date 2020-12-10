using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataService;
using DataService.Objects;
using DataService.Services;
using Microsoft.AspNetCore.Mvc;
using WebService.ObjectDto;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private IUserDataService _dataService;
        private readonly IMapper _mapper;
        public UserController(IUserDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        //GET USER PROFILE 
        [HttpGet ("user/{id}", Name = nameof(getUser))]
        public IActionResult getUser(int id)
        {
            var user = _dataService.GetUser(id);
            var usersLists = getPersonBookmarkLists(id);
            if (user == null)
            {
                return NotFound();
            }
            //return Ok(user);
            return Ok(new {user, usersLists});
        }
        
        //CREATE NEW USER
        [HttpPost("user/register")]
        public IActionResult createUser(UserDto userDto)
        {
            //string surname, string lastname, int age, string email
            var user = _dataService.CreateUser(userDto.Username, userDto.Password, userDto.Surname, userDto.LastName, userDto.Age, userDto.Email);
            return Created(" ", user);
        }
        
        //UPDATE PASSWORD
        [HttpPut("user/changepassword/{id}")]
        public IActionResult changeUserPassword(int id, UserDto userDto)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var updateUser = _dataService.ChangePassword(userDto.Username, userDto.Password, userDto.NewPassword);

            if (updateUser)
            {
                return Ok(updateUser);
            }
            return NotFound();
        }

        //UPDATE USER
        [HttpPut("user/{id}")]
        public IActionResult updateUser(int id, UserDto userDto)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            // If all is getting updated:
            var updateUser = _dataService.UpdateUser(id, userDto.Username, userDto.Password, userDto.Surname, userDto.LastName, userDto.Age, userDto.Email);
            return Ok(updateUser);
        }
        
        //DELETE USER
        [HttpDelete("user/{id}")]
        public IActionResult deleteUser(int id)
        {
            var delete = _dataService.DeleteUser(id);
            if (id <= 0)
            {
                return NotFound();
            }
            return Ok(delete);
        }
        
        //NEW PERSON BOOKMARK LIST
        [HttpPost("user/{userid}/plists/")] 
        public IActionResult newPersonBookmarkList(PersonBookmarkListDto pblDto)
        {
            var list = _dataService.NewPersonBookmarkList(pblDto.UserId, pblDto.ListName);
            return Created("New list: ", list);
        }
        
        //ADD PERSON BOOKMARK TO LIST
        [HttpPost("user/{userid}/plist/bookmark")] 
        //[HttpPost("name/{personid}/bookmark/")] 
        public IActionResult newPersonBookmark(PersonBookmarkDto pbDto)
        {
            var newBookmark = _dataService.NewPersonBookmark(pbDto.Person_Id, pbDto.List_Id);
            return Created("",newBookmark);
        }
        
        //DELETE USERS BOOKMARK LIST
        [HttpDelete("plist/{listid}")] 
        public IActionResult deletePersonBookmarkList(int listid)
        {
            var delete = _dataService.deletePersonBookmarkList(listid);
            return Ok(delete);
        }
        
        //DELETE PERSON BOOKMARK FROM LIST
        [HttpDelete("plist/{listid}/{bookmarkid}")]
        public IActionResult deletePersonBookmark(int bookmarkid)
        {
            var delete = _dataService.deletePersonBookmark(bookmarkid);
            return Ok(delete);
        }
        
        //NEW TITLE BOOKMARK LIST
        [HttpPost("user/{userid}/tlists/")] 
        public IActionResult newTitleBookmarkList(TitleBookmarkListDTO tblDto)
        {
            var list = _dataService.NewTitleBookmarkList(tblDto.UserId, tblDto.ListName);
            return Created("New list: ", list);
        }
        
        //ADD TITLE BOOKMARK TO LIST
        [HttpPost("user/{userid}/tlist/bookmark")]
        //[HttpPost("title/{titleid}/bookmark/")]
        public IActionResult newTitleBookmark(TitleBookmarkDTO tbDto)
        {
            var newBookmark = _dataService.NewTitleBookmark(tbDto.TitleId, tbDto.ListId);
            return Created("",newBookmark);
        }
        
        //DELETE USERS TITLE BOOKMARK LIST
        [HttpDelete("tlist/{listid}")] 
        public IActionResult deleteTitleBookmarkList(int listid)
        {
            var delete = _dataService.deleteTitleBookmarkList(listid);
            return Ok(delete);
        }
        
        //DELETE TITLE BOOKMARK FROM LIST
        [HttpDelete("tlist/{listid}/{bookmarkid}")]
        public IActionResult deleteTitleBookmark(int bookmarkid)
        {
            var delete = _dataService.deleteTitleBookmark(bookmarkid);
            return Ok(delete);
        }

        //GET A USERS BOOKMARK LISTS
        [HttpGet("user/{id}/lists")]
        public IActionResult getPersonBookmarkLists(int id)
        {
            var personBookmarkList = _dataService.GetUsersPersonBookmarkLists(id);
            var titleBookmarkList = _dataService.GetTitleBookmarkLists(id);
            
            IList<TitleBookmarkListDTO> titleList = titleBookmarkList.Select(x => new TitleBookmarkListDTO
            {
                Id = x.Id,
                UserId = x.UserId,
                ListName = x.ListName,
                Url = "http://localhost:5001/api/tlist/"+x.Id
            }).ToList();
            
            IList<PersonBookmarkListDto> personList = personBookmarkList.Select(x => new PersonBookmarkListDto
            {
                Id = x.Id,
                UserId = x.UserId,
                ListName = x.ListName,
                Url = "http://localhost:5001/api/plist/"+x.Id
            }).ToList();

            List<object> result = titleList.Cast<object>()
                .Concat(personList)
                .ToList();
            return Ok(result);
            //return Ok(new {personList, titleList});
            
        }
        
        //RATE A MOVIE
        [HttpPost("title/{titleid}/RateMovie/{userid}/{thisRating}/")]
        public IActionResult rateMovie(int userid, int thisRating, string titleid)
        {
            if (titleid == null)
            {
                return NotFound();
            }
            
            var rateThisMovie = _dataService.RateMovie(userid, thisRating, titleid);
            return Ok(rateThisMovie);
        }
        
        //GET USERS RATED MOVIES
        [HttpGet("user/{userid}/ratings/")]
        public IActionResult getRatings(int userid)
        {
            var ratingsList = _dataService.GetRatingFromUsers(userid);
            IList<RatingDTO> ratingList = ratingsList.Select(x => new RatingDTO
            {
                user_id = x.User_Id,
                rating = x.Rating_,
                title_id = x.Title_Id,
                url = "http://localhost:5001/api/title/"+x.Title_Id,
                updateUrl = "/api/title/"
                            +x.Title_Id+"/RateMovie/"
                            +x.User_Id+"/"
            }).ToList();
            return Ok(ratingList);
        }
        
        //DELETE USERS RATED MOVIE
        [HttpDelete("title/{titleid}/RateMovie/{userid}/Delete/")]
        public IActionResult deleteRating(int userid, string titleid)
        {
            if (userid.Equals(null) && titleid == null)
            {
                return NotFound();
            }

            var delRating = _dataService.DeleteRatingFromUser(userid, titleid);
            return Ok(delRating);

        }
        
    }
}