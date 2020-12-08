using System.Collections.Generic;
using DataService.Objects;

namespace DataService.Services
{
    public interface ITitleDataService
    {
        Genre GetGenre(int id);
        IList<Title_Search> TitleSearches(string titleid);
        Title GetTitle(string id);
        Title getTitleEpisode(string id);
        Title getTitleGenreName(string id);
        IList<Title_Person> getTitlePersons(string id);
        Title getTitlePersonName(string id);

        List<Genre> GetGenres();

        List<Title_Genre> GetGenreTitles(int id);
        

        IList<Title_Genre> GetTitleGenres(string id);

        public List<Akas> GetTitleAkas(string id);

        public Akas GetAkas(int id);

        public Title_Episode GetTitleEpisode(string id);

        public IList<Title_Episode> GetMoreTitleEpisode(string id);

        public string GetTitleEpisodeParentName(string id);

        public List<Person> GetTitlePersons(string id);

        public Title GetTitleType(string id);

        public List<TitleType> GetAllTypes();

        public List<Title> GetTypeTitles(int id);

        public TitleType GetType(int id);

        public IList<OmdbHolder> GetOmdbDatas();

        public OmdbData GetOmdbData(string id);

        public List<TopPoster> GetTopTenPoster();
    }
}