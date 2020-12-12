using System;
using System.Linq;
using DataService.Services;
using DataService.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Xunit;



namespace PortFolio2.Tests
{
    public class UserDataServiceTest
    {
        [Fact]
        public void Loginx()
        {
            var service = new UserDataService();
            var login = service.Login("user3", "user3");
            Assert.True(login);
        }
        [Fact]
        public void GetUser()
        {
            var service = new UserDataService();
            var users = service.GetUser(1);
            Assert.Equal(1, users.Id);
            Assert.Equal("isvalid", users.Surname);
        }

        [Fact]
        public void CreateUser()
        {
            var service = new UserDataService();
            var newUser = service.CreateUser(
                "userTest4", 
                "pw", 
                "TestName", 
                "Testlast", 
                20, 
                "test4@t.com");
            Assert.True(newUser);
        }

        [Fact]
        public void ChangePassword()
        {
            var service = new UserDataService();
            var chpw = service.ChangePassword("pw", "pw", "pw1");
            Assert.True(chpw);
        }

        [Fact]
        public void UpdateUserCorrect()
        {
            var service = new UserDataService();
            var update = service.UpdateUser(10,"userTest", "pwnew", "Testname", 10,  "tt@tt.com");
            Assert.True(update);
        }
        [Fact]
        public void UpdateUserFalse()
        {
            var service = new UserDataService();
            var updateFalse = service.UpdateUser(10,"userTestUpdate", "falsepw", "Testname", 10,  "tt@com");
            Assert.False(updateFalse);
        }
        
        [Fact]
        public void GetRatingFromUser()
        {
            var service = new UserDataService();
            var rating = service.GetRatingFromUsers(1);
            Assert.Equal(1, rating.Count);
            Assert.Equal("tt11972952", rating. First().Title_Id);
            Assert.Equal(1, rating.Last().Rating_);
        }
        

        [Fact]
        public void GetPersonBookmarkLists()
        {
            var service = new UserDataService();
            var personBookmarkList = service.GetUsersPersonBookmarkLists(1);
            Assert.Equal(7, personBookmarkList.Count);
            Assert.Equal("My Fav Directors", personBookmarkList.First().ListName);
            Assert.Equal("x", personBookmarkList.Last().ListName);
        }
        
        [Fact]
        public void GetPersonBookmark()
        {
            var service = new UserDataService();
            var personBookmark = service.GetPersonBookmark(26);
            Assert.Equal(26,personBookmark.Id);
            Assert.Equal("nm0000001",personBookmark.Person_Id);
        }

        [Fact]
        public void GetTitleBookmarks()
        {
            var service = new UserDataService();
            var titleBookmarks = service.GetTitleBookmarks(6);
            Assert.Equal(6, titleBookmarks.First().Id);
            Assert.Equal(5, titleBookmarks.First().ListId);
            Assert.Equal("tt0052520", titleBookmarks.First().TitleId);
        }

        [Fact]
        public void newPersonBookmarkList()
        {
            var service = new UserDataService();
            var newbooklist = service.NewPersonBookmarkList(1, "hejsatest");
            Assert.Equal(1,newbooklist.UserId);
            Assert.Equal("hejsatest",newbooklist.ListName);
            }

        [Fact]
        public void newPersonBookmark()
        {
            var service = new UserDataService();
            var newBookmark = service.NewPersonBookmark("nm0000002", 1);
            Assert.Equal("nm0000002",newBookmark.Person_Id);
            Assert.Equal(1, newBookmark.List_Id);
        }

        [Fact]
        public void RateMovie()
        {
            var service = new UserDataService();
            var rateMovie = service.RateMovie(3, 1, "tt11972952");
            Assert.True(rateMovie);
        }

        [Fact]
        public void GetRating()
        {
            var service = new UserDataService();
            var getRating = service.GetMovieRatingFromUser(3, "tt11972952");
            Assert.Equal(1, getRating.Rating_);
        }

        [Fact]
        public void DeleteRating()
        {
            var service = new UserDataService();
            var delRating = service.DeleteRatingFromUser(3, "tt11972952");
            Assert.True(delRating);
        }
        
    }
}