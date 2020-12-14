using System.Collections.Generic;

namespace DataService.Objects
{
    public class Title_Bookmark
    {
        public int Id { get; set; }
        public string TitleId { get; set; }
        public int ListId { get; set; }
        public Title_Bookmark_List TitleBookmarkList { get; set; }
        public Title Title { get; set; }
        
        //public Title_Bookmark_List TitleBookmarkList { get; set; }

    }
}