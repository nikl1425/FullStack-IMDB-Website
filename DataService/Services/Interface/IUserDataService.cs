using System.Collections;
using System.Collections.Generic;
using DataService.Objects;

namespace DataService.Services
{
    public interface IUserDataService
    {
        bool Login(string username, string password, string email);
        User GetUser(int id);
        bool CreateUser(string username, string password, string surname, string lastname, int age, string email);
        bool UpdateUser(int id, string username, string surname, string lastname, int age, string email);
        public bool ChangePassword(string username, string oldpassword, string newpassword);
        bool DeleteUser(int id);
        int GetUserIDByUsername(string username);
        bool NewPersonBookmarkList(int userid, string listName);
        bool NewTitleBookmarkList(int userid, string listName);
        bool NewPersonBookmark(string personid, int listid);
        bool NewTitleBookmark(string titleid, int listid);
        IList<Rating> GetRatingFromUsers(int userid);
        List<Title_Bookmark> GetTitleBookmarks(int id);
        List<Person_Bookmark_list> GetUsersPersonBookmarkLists(int userid);
        List<Person_Bookmark_list> GetPersonBookmarkList(int id);
        List<Title_Bookmark_List> GetTitleBookmarkList(int id);
        List<Title_Bookmark_List> GetUsersTitleBookmarkLists(int userid);
        IList<Person_Bookmark> GetPersonBookmarks(int listid);
        bool deletePersonBookmarkList(int listid);
        bool deleteTitleBookmarkList(int listid);
        bool deletePersonBookmark(int id);
        bool deleteTitleBookmark(int id);
        bool DeleteRatingFromUser(int userid, string titleid);
        bool RateMovie(int userid, int thisRating, string titleid);
    }
}