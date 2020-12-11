using System.Collections.Generic;

namespace DataService.Objects
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public List<Title_Bookmark_List> TitleBookmarkLists { get; set; }

        //public List<Rating> Ratings { get; set; }
        
        //public List<Person_Bookmark_list> PersonBookmarkLists { get; set; }
        
        private List<Search_History> SearchHistories { get; set; }

    }
}