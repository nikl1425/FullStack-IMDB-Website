using System.Collections.Generic;
using DataService.Objects;

namespace WebService.ObjectDto
{
    public class TitleDto
    {
        public string Id { get; set; }
        public int TypeId { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public string Type { get; set; }
        public string TypeUrl { get; set; }
        public string poster  {get; set;}
        
        //URL
        public string Url { get; set; }
        
        
        // Mangler
        public Title_Rating TitleRating;
        
        public IList<Title_Bookmark> TitleBookmarks { get; set; }
        public IList<Person_Bookmark> PersonBookmarks { get; set; }
        public IList<Person_known_title> PersonKnownTitles { get; set; }
        public TitleRuntime TitleRuntime { get; set; }
    }
}