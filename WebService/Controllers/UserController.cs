using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataService;
using DataService.Objects;
using DataService.Services;
using DataService.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebService.ObjectDto;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebService.Controllers
{
    
    
    
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserDataService _dataService;
        private ITitleDataService _titleDataService;
        private readonly IMapper _mapper;
        private IConfiguration _config;
        public UserController(IUserDataService dataService, ITitleDataService titleDataService, IMapper mapper, IConfiguration config)
        {
            _config = config;
            _dataService = dataService;
            _titleDataService = titleDataService;
            _mapper = mapper;
        }
        
        //LOGIN
        [HttpPost("user/login/")]
        public IActionResult Login(UserDto userDto)
        {
            var user = _dataService.Login(userDto.Username, userDto.Password);
            IActionResult response = Unauthorized();
            if (user)
            {
                var tokenStr = GenerateJSONWebToken(userDto);
                response = Ok(new {welcome = "Logged in as: " + userDto.Username, tokenStr});
            }
            else
            {
                return BadRequest("User not authorized");
            }
            return response;
        }
        
        [Authorize]
        [HttpPost("post")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            Console.WriteLine(claim.Count);
            var username = claim[0].Value;
            Console.WriteLine(username);
            return "Welcome to " + username;
        }
        
        [Authorize]
        [HttpGet("GetValue")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"Value1", "Value2", "Value3"};
        }

        private string GenerateJSONWebToken(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDto.Username),
                new Claim(JwtRegisteredClaimNames.Email, userDto.Password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            
            var token = new JwtSecurityToken(
                issuer: "Issuer",
                audience: "Issuer",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
        
        [Authorize]
        [HttpGet("user/approved/")]
        public IActionResult Validate()
        {
            return Ok();
        }

        //GET USER PROFILE 
        [Authorize]
        [HttpGet ("user/{id}", Name = nameof(getUser))]
        public IActionResult getUser(int id)
        {
            var user = _dataService.GetUser(id);
            var usersLists = getPersonBookmarkLists(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
            //return Ok(new {user, usersLists});
        }
        
        //CREATE NEW USER
        [Authorize]
        [HttpPost("user/register")]
        public IActionResult createUser(UserDto userDto)
        {
            //string surname, string lastname, int age, string email
            var user = _dataService.CreateUser(userDto.Username, userDto.Password, userDto.Surname, userDto.LastName, userDto.Age, userDto.Email);
            return Created(" ", user);
        }

        //UPDATE PASSWORD
        [Authorize]
        [HttpPost("user/{id}/changepassword")]
        public IActionResult changeUserPassword(UserDto userDto)
        {
            /*if (id <= 0)
            {
                return NotFound();
            }*/ 
            var updateUser = _dataService.ChangePassword(userDto.Username, userDto.Password, userDto.NewPassword);
            return Ok(updateUser);
        }

        //UPDATE USER
        [Authorize]
        [HttpPost("user/{id}/update")]
        public IActionResult updateUser(int id, UserDto userDto)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var updateUser = _dataService.UpdateUser(id, userDto.Username, userDto.Surname, userDto.LastName, userDto.Age, userDto.Email);
            return Ok(updateUser);
        }
        
        //DELETE USER
        [Authorize]
        [HttpDelete("user/{id}/delete")]
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
        [Authorize]
        [HttpPost("user/{userid}/plists/create")] 
        public IActionResult newPersonBookmarkList(PersonBookmarkListDto pblDto)
        {
            var list = _dataService.NewPersonBookmarkList(pblDto.UserId, pblDto.ListName);
            return Created("New list: ", list);
        }
        
        //ADD PERSON BOOKMARK TO LIST
        [Authorize]
        [HttpPost("user/{userid}/plist/bookmark")] 
        //[HttpPost("name/{personid}/bookmark/")] 
        public IActionResult newPersonBookmark(PersonBookmarkDto pbDto)
        {
            var newBookmark = _dataService.NewPersonBookmark(pbDto.Person_Id, pbDto.List_Id);
            return Created("",newBookmark);
        }
        
        //DELETE USERS BOOKMARK LIST
        [Authorize]
        [HttpDelete("plist/{listid}/delete")] 
        public IActionResult deletePersonBookmarkList(int listid)
        {
            var delete = _dataService.deletePersonBookmarkList(listid);
            return Ok(delete);
        }
        
        //DELETE PERSON BOOKMARK FROM LIST
        [Authorize]
        [HttpDelete("plist/{listid}/{bookmarkid}")]
        public IActionResult deletePersonBookmark(int bookmarkid)
        {
            var delete = _dataService.deletePersonBookmark(bookmarkid);
            return Ok(delete);
        }
        
        //NEW TITLE BOOKMARK LIST
        [Authorize]
        [HttpPost("user/{userid}/tlists/create")] 
        public IActionResult newTitleBookmarkList(TitleBookmarkListDTO tblDto)
        {
            var list = _dataService.NewTitleBookmarkList(tblDto.UserId, tblDto.ListName);
            return Created("New list: ", list);
        }
        
        //ADD TITLE BOOKMARK TO LIST
        [Authorize]
        [HttpPost("user/{userid}/tlist/bookmark")]
        //[HttpPost("title/{titleid}/bookmark/")]
        public IActionResult newTitleBookmark(TitleBookmarkDTO tbDto)
        {
            var newBookmark = _dataService.NewTitleBookmark(tbDto.TitleId, tbDto.ListId);
            return Created("",newBookmark);
        }
        
        //DELETE USERS TITLE BOOKMARK LIST
        [Authorize]
        [HttpDelete("tlist/{listid}/delete")] 
        public IActionResult deleteTitleBookmarkList(int listid)
        {
            var delete = _dataService.deleteTitleBookmarkList(listid);
            return Ok(delete);
        }
        
        //DELETE TITLE BOOKMARK FROM LIST
        [Authorize]
        [HttpDelete("tlist/{listid}/{bookmarkid}")]
        public IActionResult deleteTitleBookmark(int bookmarkid)
        {
            var delete = _dataService.deleteTitleBookmark(bookmarkid);
            return Ok(delete);
        }

        //GET A USERS BOOKMARK LISTS
        [Authorize]
        [HttpGet("user/{id}/lists")]
        public IActionResult getPersonBookmarkLists(int id)
        {
            var personBookmarkList = _dataService.GetUsersPersonBookmarkLists(id);
            var titleBookmarkList = _dataService.GetUsersTitleBookmarkLists(id);
            
            IList<TitleBookmarkListDTO> titleList = titleBookmarkList.Select(x => new TitleBookmarkListDTO
            {
                Type = "tlist",
                Id = "t"+x.Id,
                UserId = x.UserId,
                ListName = x.ListName,
                Url = "http://localhost:5001/api/tlist/"+x.Id
            }).ToList();
            
            IList<PersonBookmarkListDto> personList = personBookmarkList.Select(x => new PersonBookmarkListDto
            {
                Type = "plist",
                Id = "p"+x.Id,
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
        [Authorize]
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
        [Authorize]
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
                            +x.User_Id+"/",
                titleName = _titleDataService.GetTitle(x.Title_Id).OriginalTitle,
                prodYear = _titleDataService.GetTitle(x.Title_Id).StartYear,
                poster = _titleDataService.GetOmdbData(x.Title_Id).Poster ?? _titleDataService.GetOmdbData("tt11000576").Poster,
                plot = _titleDataService.GetOmdbData(x.Title_Id).Plot
            }).ToList();
            return Ok(ratingList);
        }
        
        //DELETE USERS RATED MOVIE
        [Authorize]
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