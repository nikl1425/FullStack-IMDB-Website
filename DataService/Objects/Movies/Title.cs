using System.Collections;
using System.Collections.Generic;

namespace DataService.Objects
{
    public class Title
    {
        public string Id { get; set; }
        public int TypeId { get; set; }
        public string PrimaryTitle { get; set; }
        public string OriginalTitle { get; set; }
        public bool IsAdult { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        
        


        //MANGLER ALLE KLASSER
        public Title_Rating TitleRating;
        public IList<Title_Genre> TitleGenres { get; set; }
        public IList<Title_Search> TitleSearches { get; set; }
        public IList<Akas> Akases { get; set; }
        public Title_Episode TitleEpisode { get; set; }
        public TitleType Type { get; set; }
        public IList<Title_Person> TitlePersons { get; set; }

        //Måske skal ændres
        public IList<Title_Bookmark> TitleBookmarks { get; set; }

        public IList<Person_known_title> PersonKnownTitles { get; set; }
        
        public TitleRuntime TitleRuntime { get; set; }
        
        public OmdbData OmdbData { get; set; }
        
        
    }
}