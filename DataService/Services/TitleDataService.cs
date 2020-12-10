using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataService.Objects;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Type = System.Type;

namespace DataService.Services
{
    public class TitleDataService : ITitleDataService
    {
        public TitleDataService()
        {
            using var ctx = new ImdbContext();
        }

        public Genre GetGenre(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.genre.Find(id);
        }

        public List<Genre> GetGenres()
        {
            using var ctx = new ImdbContext();
            return ctx.genre.ToList();
        }


        public IList<Title_Search> TitleSearches(string titleid)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_search
                .Where(s => s.Id.Equals(titleid));
            return query.ToList();
        }

        public Title GetTitle(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title.Find(id);
            return query;
        }

        public Title getTitleEpisode(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title.Where(x => x.Id == id)
                .Include(x => x.TitleEpisode).FirstOrDefault();
            return query;
        }


        public IList<OmdbHolder> GetOmdbDatas()
        {
            using var ctx = new ImdbContext();
            var query = ctx.omdb_data
                .Include(x => x.Title)
                .Select(x => new OmdbHolder
                {
                    Id = x.Id,
                    Poster = x.Poster,
                    PrimaryTitle = x.Title.PrimaryTitle,
                    OriginalTitle = x.Title.OriginalTitle,
                    IsAdult = x.Title.IsAdult,
                    StartYear = x.Title.StartYear,
                    EndYear = x.Title.EndYear
                })
                .ToList();

            return query;


        }

        

        public IList<Title_Genre> GetTitleGenres (string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_genre
                .Include(x => x.Genre)
                .Where(x => x.TitleId == id)
                .ToList();
            return query;
        }

        public Title getTitleGenreName(string id)
        {
            using var ctx = new ImdbContext();

            // Denne title har 3 genre derfor er den udvalgt.

            var query = ctx.title
                .Include(x => x.TitleGenres)
                .ThenInclude(o => o.Genre)
                .AsSingleQuery()
                .FirstOrDefault(o => o.Id == id);

            return query;
        }


        public IList<Title_Person> getTitlePersons(string id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.TitlePersons
                .Where(x => x.TitleId == id)
                .ToList();
            return query;
        }

        public Title getTitlePersonName(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title
                .Include(x => x.TitlePersons)
                .ThenInclude(x => x.Person)
                .FirstOrDefault(x => x.Id == id);
            return query;
        }

        public List<Title_Genre> GetGenreTitles(int id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_genre
                .Include(x => x.Title)
                .Include(x => x.Genre)
                .Where(x => x.GenreId == id)
                .ToList();

            return query;
        }

        public List<Akas> GetTitleAkas(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.akas
                .Where(x => x.TitleId == id)
                .ToList();

            return query;
        }

        public Akas GetAkas(int id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.akas
                .Where(x => x.Id == id)
                .Include(x => x.AkasType)
                .FirstOrDefault();

            return query;
        }

        public Title_Episode GetTitleEpisode(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.title_episode
                .Where(x => x.TitleId == id)
                .FirstOrDefault();
            return query;
        }

        public IList<Title_Episode> GetMoreTitleEpisode(string id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.title_episode
                .Where(x => x.TitleId == id)
                .FirstOrDefault();

            if (query == null)
            {
                return null;
            }


            var query2 = ctx.title_episode
                .Where(x => x.ParentId == query.ParentId)
                .Include(x => x.Title)
                .ToList();

            return query2;
        }

        public string GetTitleEpisodeParentName(string id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.title_episode
                .Where(x => x.TitleId == id)
                .FirstOrDefault();

            if (query == null)
            {
                return null;
            }

            var query2 = ctx.title
                .Where(x => x.Id == query.ParentId)
                .Select(x => x.PrimaryTitle)
                .FirstOrDefault();


            return query2;
        }

        public List<Person> GetTitlePersons(string id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.TitlePersons
                .Include(x => x.Person)
                .Where(x => x.TitleId == id)
                .Select(x => x.Person)
                .Distinct()
                .ToList();

            return query;
        }

        public Title GetTitleType(string id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.title
                .Include(x => x.Type)
                .FirstOrDefault();
                

            return query;
        }

        public List<TitleType> GetAllTypes()
        {
            using var ctx = new ImdbContext();

            var query = ctx.type.ToList();

            return query;
        }

        public TitleType GetType(int id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.type.FirstOrDefault(x => x.Id == id);

            return query;
        }

        public List<Title> GetTypeTitles(int id)
        {
            using var ctx = new ImdbContext();

            var query = ctx.title
                .Include(x => x.Type)
                .Where(x => x.Type.Id == id)
                .ToList();
            return query;
        }

        public OmdbData GetOmdbData(string id)
        {
         using var ctx = new ImdbContext();

         var query = ctx.omdb_data
             .Where(x => x.Id == id)
             .FirstOrDefault();

         return query;
         
         
        }

        public List<TopPoster> GetTopTenPoster()
        {
            using var ctx = new ImdbContext();

            var result = ctx.TopPosters.FromSqlInterpolated($"select * from top10homeposter()");
                

            return result.ToList();
        }

        public List<Movies> GetAllMovies(int page, int pageSize)
        {
            using var ctx = new ImdbContext() ;
            var query = ctx.Movies
                .FromSqlInterpolated($"select * from titlesformoviepage()")
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();;
            
            return query.ToList();
        }

        public int GetNumberOfMovies()
        {
            using var ctx = new ImdbContext();
            var query = ctx.Movies.FromSqlInterpolated($"select * from titlesformoviepage()").Count();
            return query;
        }
        
     

    }
}