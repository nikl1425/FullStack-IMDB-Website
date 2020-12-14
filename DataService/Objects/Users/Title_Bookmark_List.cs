using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataService.Objects
{
    public class Title_Bookmark_List
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ListName { get; set; }
        [Required] public IList<Title_Bookmark> TitleBookmarks { get; set; }
    }
}