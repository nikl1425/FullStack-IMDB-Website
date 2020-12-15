using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using DataService.Objects;
using DataService.Services.Utils;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace DataService.Services
{
    public class UserDataService : IUserDataService
    {
        private Hashing hashing = new Hashing();
        private UserValidation _userValidation = new UserValidation();
        public UserDataService()
        {
            using var ctx = new ImdbContext();
        }

        //
        //        USER PROFILE
        //
        
        //LOGIN
        public bool Login(string username, string password, string email)
        {
            using var ctx = new ImdbContext();
            
            var getUser = ctx.users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
            return getUser != null && username.ToLower() == getUser.Username.ToLower() && email == getUser.Email && 
                   _userValidation.VerifyPassword(password, getUser.Password, getUser.Salt);
        }
        

        //GET USER PROFILE
        public User GetUser(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.users.Find(id);
        }
        
        //GET USER PROFILE
        public int GetUserIDByUsername(string username)
        {
            using var ctx = new ImdbContext();
            var query = ctx.users.Where(x => x.Username == username).FirstOrDefault().Id;
            return query;
        }

        //CHECK IF AN EMAIL IS AN EMAIL && DOESNT EXISTS IN THE DB
        private static bool IsValidEmail(string email)
        {
            using var ctx = new ImdbContext();
            var emailTrimed = email.Trim();

            if (string.IsNullOrEmpty(emailTrimed)) return false;
            
            var hasWhitespace = emailTrimed.Contains(" ");
            var indexOfAtSign = emailTrimed.LastIndexOf('@');
            
            if (indexOfAtSign <= 0 || hasWhitespace) return false;
            
            var afterAtSign = emailTrimed.Substring(indexOfAtSign + 1);
            var indexOfDotAfterAtSign = afterAtSign.LastIndexOf('.');
            
            var query = ctx.users.Where(x => x.Email == email).ToList();
            
            if (query.Count > 0) return false;

            return indexOfDotAfterAtSign > 0 && afterAtSign.Substring(indexOfDotAfterAtSign).Length > 1;
        }
        
        //CREATE NEW USER
        public bool CreateUser(string username, string password, string surname, string lastname, int age, string email)
        {
            Hashing.HashSalt hashSalt = hashing.PasswordHash(16, password);
            
            using var ctx = new ImdbContext();
            var maxId = ctx.users.Max(x => x.Id);
            var usernameQuery = ctx.users.Where(x => x.Username == username).ToList();
            if (usernameQuery.Count > 0) return false;
            var emailQuery = ctx.users.Where(x => x.Email == email).ToList();
            if (emailQuery.Count > 0) return false;
            
            // TODO: Add username & password check 
            if(!Regex.IsMatch(surname, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine(surname + "is not correct");
                return false;
            }
            if(!Regex.IsMatch(lastname, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine(lastname + "is not correct");
                return false;
            }
            if(!IsValidEmail(email))
            {
                Console.WriteLine(email + "is not correct");
                return false;
            }
            if(age == 0)
            {
                Console.WriteLine(age + " is 0");
                return false; 
            }
            if(password == null)
            {
                Console.WriteLine(password + "is null");
                return false;
            } 
            ctx.users.Add(new User
                {Id = maxId+1, Username = username, Password = hashSalt.Hash, Salt = hashSalt.Salt, Age = age, Surname = surname, Lastname = lastname, Email = email});
            ctx.SaveChanges();
            return true;
            //return ctx.users.Find(maxId + 1);
        }

        //CHANGE PASSWORD
        public bool ChangePassword(/*int id,*/ string username, string oldpassword, string newpassword)
        {
            using var ctx = new ImdbContext();
            var getUser = ctx.users.FirstOrDefault(x => x.Username == username);
            if (getUser.Equals(null)) return false;
            if (!_userValidation.VerifyPassword(oldpassword, getUser.Password, getUser.Salt)) return false;

            Hashing.HashSalt hashSalt = hashing.PasswordHash(16, newpassword);
            ctx.users.Update(getUser).Entity.Password = hashSalt.Hash;
            ctx.users.Update(getUser).Entity.Salt = hashSalt.Salt;
            ctx.SaveChanges();
            return true;

        }
        
        //UPDATE USER PROFILE
        public bool UpdateUser(int id, string username, string surname, string lastname, int age, string email)
        {
            using var ctx = new ImdbContext();
            var getUser = ctx.users.FirstOrDefault(x => x.Username == username);
            //if (getUser == null) return false;
            //PASSWORD
            //if (_userValidation.VerifyPassword(password, ctx.users.Find(id).Password, ctx.users.Find(id).Salt))
            //{
                //USERNAME
                if (username != null && Regex.IsMatch(username, @"^[a-zA-Z]+$"))
                {
                    ctx.users.Update(ctx.users.Find(id)).Entity.Username = username;
                }

                //SURNAME
                if (surname != null && Regex.IsMatch(surname, @"^[a-zA-Z]+$"))
                {
                    ctx.users.Update(ctx.users.Find(id)).Entity.Surname = surname;
                }

                //LASTNAME
                if (lastname != null && Regex.IsMatch(lastname, @"^[a-zA-Z]+$"))
                {
                    ctx.users.Update(ctx.users.Find(id)).Entity.Lastname = lastname;
                }

                //AGE
                if (age != 0)
                {
                    ctx.users.Update(ctx.users.Find(id)).Entity.Age = age;
                }

                //EMAIL
                if (email != null && IsValidEmail(email))
                {
                    ctx.users.Update(ctx.users.Find(id)).Entity.Email = email;
                }

                ctx.SaveChanges();
                return true;
            //}

            return false;
            
        }
        
        //DELETE USER PROFILE
        //TODO : Slet alle brugerens lister mm. 
        public bool DeleteUser(int id)
        {
            using var ctx = new ImdbContext();
            var dbUser = GetUser(id);
            if (dbUser == null)
            {
                return false;
            }
            
            ctx.users.Remove(dbUser);
            ctx.SaveChanges();
            return true;
        }

        //
        //        PERSON BOOKMARKS
        //
        
        //GET USERS PERSON BOOKMARK LIST
        public List<Person_Bookmark_list> GetUsersPersonBookmarkLists(int userid)
        {
            using var ctx = new ImdbContext();
            var x = ctx.person_bookmark_list
                .Where(b => b.UserId == userid)
                .ToList();
            return x;
        }
        
        //NEW PERSON BOOKMARK LIST
        public bool NewPersonBookmarkList(int userid, string listName)
        {
            using var ctx = new ImdbContext();
            var maxId = ctx.person_bookmark_list.Max(x => x.Id);
            var dbUser = GetUser(userid).Id;
            ctx.person_bookmark_list
                .Add(new Person_Bookmark_list
                {ListName = listName, UserId = dbUser});
            ctx.SaveChanges();
            return true;
            //return ctx.person_bookmark_list.Find(maxId+1);
        }
        
        //ADD PERSON BOOKMARK TO LIST
        public bool NewPersonBookmark(string personid, int listid)
        {
            using var ctx = new ImdbContext();
            ctx.person_bookmarks
                .Add(new Person_Bookmark()
                    {List_Id = listid, Person_Id = personid});
            ctx.SaveChanges();
            return true;
        }
        
        //GET USERS PERSON BOOKMARK LIST
        public List<Person_Bookmark_list> GetPersonBookmarkList(int id)
        {
            using var ctx = new ImdbContext();
            var x = ctx.person_bookmark_list.Where(x => x.Id == id);
            return x.ToList();
        }

        // GET A PERSON BOOKMARK (USED FOR DELETE)
        public Person_Bookmark GetPersonBookmark(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.person_bookmarks.Find(id);
        }

        //GET A LIST OF PERSON BOOKMARKS (USED FOR DELETE)
        public IList<Person_Bookmark> GetPersonBookmarks(int listid)
        {
            using var ctx = new ImdbContext();
            var x = ctx.person_bookmarks
                .Where(x => x.List_Id == listid)
                .ToList();
            return x;
        }
        
        //DELETE A USERS PERSON BOOKMARK LIST
        public bool deletePersonBookmarkList(int listid)
        {
            using var ctx = new ImdbContext();
            var dbList = GetPersonBookmarkList(listid).FirstOrDefault();
            if (dbList == null)
            {
                return false;
            }

            deletePersonBookmarks(listid);
            ctx.person_bookmark_list.Remove(dbList);
            ctx.SaveChanges();
            
            return true;
        }
        
        //DELETE PERSON BOOKMARKS FROM LIST (USED IN DELETE OF LISTS)
        public bool deletePersonBookmarks(int id)
        {
            using var ctx = new ImdbContext();
            var dbBookmark = GetPersonBookmarks(id);
            if (dbBookmark == null)
            {
                return false;
            }

            foreach (var x in ctx.person_bookmarks.Where(x=>x.List_Id == id))
            {
                ctx.person_bookmarks.Remove(x);
            }
            ctx.SaveChanges();
            return true;
        }
        
        //DELETE A PERSON BOOKMARK
        public bool deletePersonBookmark(int id)
        {
            using var ctx = new ImdbContext();
            var dbBookmark = GetPersonBookmark(id);
            if (dbBookmark == null)
            {
                return false;
            }

            ctx.person_bookmarks.Remove(dbBookmark);
            ctx.SaveChanges();
            return true;
        }
        
        //
        //    TITLE BOOKMARKS
        //
        
        //NEW TITLE BOOKMARK LIST
        public bool NewTitleBookmarkList(int userid, string listName)
        {
            using var ctx = new ImdbContext();
            var maxId = ctx.person_bookmark_list.Max(x => x.Id);
            var dbUser = GetUser(userid).Id;
            ctx.title_bookmark_list
                .Add(new Title_Bookmark_List()
                    {ListName = listName, UserId = dbUser});
            ctx.SaveChanges();
            return true;
            //return ctx.title_bookmark_list.Find(maxId+1);
        }

        //CREATE NEW TITLE BOOKMARK
        public bool NewTitleBookmark(string titleid, int listid)
        {
            using var ctx = new ImdbContext();
            var maxId = ctx.title_bookmarks.Max(x => x.Id);
            ctx.title_bookmarks
                .Add(new Title_Bookmark()
                    {ListId = listid, TitleId = titleid});
            ctx.SaveChanges();
            return true;
        }
        
        //GET TITLE BOOKMARK LISTS
        public List<Title_Bookmark_List> GetUsersTitleBookmarkLists(int userid)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_bookmark_list
                .Where(x => x.UserId == userid);
            return query.ToList();
        }
        
        //GET TITLE BOOKMARK LISTS
        public List<Title_Bookmark_List> GetTitleBookmarkList(int id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_bookmark_list
                .Where(x => x.Id == id);
            return query.ToList();
        }
        
        //GET TITLE BOOKMARKS
        public List<Title_Bookmark> GetTitleBookmarks(int id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_bookmarks.Where(x => x.ListId == id);
            return query.ToList();
        }
        
        //GET A TITLE BOOKMARK
        public Title_Bookmark GetTitleBookmark(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.title_bookmarks.Find(id);
        }
        
        //DELETE TITLE BOOKMARK LIST
        public bool deleteTitleBookmarkList(int listid)
        {
            using var ctx = new ImdbContext();
            var dbList = ctx.title_bookmark_list.FirstOrDefault(x => x.Id == listid);
            if (dbList == null)
            {
                return false;
            }

            deleteTitleBookmarks(listid);
            ctx.title_bookmark_list.Remove(dbList);
            ctx.SaveChanges();
            
            return true;
        }

        //DELETE TITLE BOOKMARKS FROM LIST (USED IN DELETE OF TITLE LIST)
        public bool deleteTitleBookmarks(int id)
        {
            using var ctx = new ImdbContext();
            var dbBookmark = GetTitleBookmarks(id);
            if (dbBookmark == null)
            {
                return false;
            }

            foreach (var x in ctx.title_bookmarks.Where(x=>x.ListId == id))
            {
                ctx.title_bookmarks.Remove(x);
            }
            ctx.SaveChanges();
            return true;
        }
        
        //DELETE TITLE BOOKMARK
        public bool deleteTitleBookmark(int id)
        {
            using var ctx = new ImdbContext();
            var dbBookmark = GetTitleBookmark(id);
            if (dbBookmark == null)
            {
                return false;
            }

            ctx.title_bookmarks.Remove(dbBookmark);
            ctx.SaveChanges();
            return true;
        }
        
        //RATE MOVIE
        public bool RateMovie(int userid, int thisRating, string titleid)
        {
            using var ctx = new ImdbContext();
            var query = ctx.rating.Where(x => x.User_Id == userid && x.Title_Id == titleid).ToList();
            //if rating is between 1 and 10... in the gui have drop down. 
            if (thisRating >= 1 && thisRating <= 10 )
            {
                //if haven't been rated before by user then set new rating
                if (query.Count == 0)
                {
                    //Add to users rating list
                    ctx.rating
                        .Add(new Rating {User_Id = userid, Title_Id = titleid, Rating_ = thisRating});
                    
                    //if this movie haven't been voted before, add to DB
                    if (ctx.title_rating.FirstOrDefault(x => x.Title_Id == titleid) == null)
                    {
                        ctx.title_rating.Add(new Title_Rating {Title_Id = titleid});
                    }
                    var thisMovie = ctx.title_rating.FirstOrDefault(x => x.Title_Id == titleid);
                    var thisMovieVotes = thisMovie.Num_Votes;
                    var thisMovieRating = thisMovie.Average_Rating;
                    var calcNewRating = (thisMovieVotes * thisMovieRating + thisRating) / (thisMovieVotes + 1);
                    
                    ctx.title_rating.Update(thisMovie).Entity.Average_Rating = calcNewRating;
                    ctx.title_rating.Update(thisMovie).Entity.Num_Votes = (thisMovieVotes + 1);
                    ctx.SaveChanges();
                    return true;

                }
                //If user HAS ALREADY voted on this movie before. 
                else
                {
                    //Update users rating list
                    var updateRatingList = ctx.rating.FirstOrDefault(x => x.Title_Id == titleid && x.User_Id == userid);
                    var prevRating = updateRatingList.Rating_;
                    var thisMovie = ctx.title_rating.FirstOrDefault(x => x.Title_Id == titleid);
                    var thisMovieVotes = thisMovie.Num_Votes;
                    var thisMovieRating = thisMovie.Average_Rating;
                    var calcNewRating = (((thisMovieRating * thisMovieVotes) - prevRating) + thisRating) / thisMovieVotes;
                    
                    ctx.title_rating.Update(thisMovie).Entity.Average_Rating = calcNewRating;
                    ctx.rating.Update(updateRatingList).Entity.Rating_ = thisRating;
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        
        //DELETE RATING FROM MOVIE
        public bool DeleteRatingFromUser(int userid, string titleid)
        {
            using var ctx = new ImdbContext();
            //calc new rating of movie
            var updateRatingList = ctx.rating.FirstOrDefault(x => x.Title_Id == titleid && x.User_Id == userid);
            if (updateRatingList == null) return false;
            {
                var prevRating = updateRatingList.Rating_;
                var thisMovie = ctx.title_rating.FirstOrDefault(x => x.Title_Id == titleid);
                var thisMovieVotes = thisMovie.Num_Votes;
                var thisMovieRating = thisMovie.Average_Rating;
                var calcNewRating = ((thisMovieRating * thisMovieVotes) - prevRating) / (thisMovieVotes-1);
                    
                ctx.title_rating.Update(thisMovie).Entity.Average_Rating = calcNewRating;
                ctx.title_rating.Update(thisMovie).Entity.Num_Votes = thisMovieVotes - 1;
                ctx.SaveChanges();
                DeleteRatingFromDB(userid, titleid);
                return true;
            }

        }

        //FOR BELOW METHOD, MADE BCS OF AN ERROR;
        /*
         * The instance of entity type 'Rating' cannot be tracked because another
         * instance with the key value '{Id: x}' is already being tracked 
         */
        public bool DeleteRatingFromDB(int userid, string titleid)
        {
            using var ctx = new ImdbContext();
            var dbRating = GetMovieRatingFromUser(userid, titleid);
            ctx.Remove(dbRating);
            ctx.SaveChanges();
            return true;
        }
        
        //GET LIST OF THE USERS RATED MOVIES ... DELETE USERS RATING
        public IList<Rating> GetRatingFromUsers(int userid)
        {
            using var ctx = new ImdbContext();
            var getRating = ctx.rating.Where(r => r.User_Id == userid);
            return getRating.ToList();
        }
        
        //GET SINGLE RATED MOVIE FROM USER (FOR DELETE)
        public Rating GetMovieRatingFromUser(int userid, string titleid)
        {
            using var ctx = new ImdbContext();
            var x = ctx.rating.FirstOrDefault(r => r.User_Id == userid && r.Title_Id == titleid);
            return x;
        }
        
        
        /////////////////////////////////////////////////////////////////////
        /////////////// NOT YET IMPLEMENTED IN THE DATA SERVICE /////////////
        /////////////////////////////////////////////////////////////////////

        //GET LIST OF THE USERS SEARCH HISTORY
        public IList<Search_History> GetSearchHistories(int userid, string id)
        {
            using var ctx = new ImdbContext();
            var x = ctx.search_history.Where(s => s.User_Id == userid);
            return x.ToList();
            
        }
        /////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////
        

    }
}