using System;
using System.Linq;
using DataService.Objects;
using DataService.Services;
using Xunit;
using DataService.Services;


namespace PortFolio2.Tests
{
    public class TitleDataServiceTest
    {
        [Fact]
        public void GetGenre()
        {
            var service = new TitleDataService();
            var genre = service.GetGenre(1);
            Assert.Equal(1, genre.Id);
            Assert.Equal("Sport", genre.Name);
        }

        [Fact]
        public void getTitle()
        {
            var service = new TitleDataService();
            var title = service.GetTitle("tt9916538");
            Assert.Equal("Kuambil Lagi Hatiku", title.PrimaryTitle);
            Assert.Equal("2019", title.StartYear);
        }

        [Fact]
        public void getTitleEpisodeNumber()
        {
            var service = new TitleDataService();
            var title = service.getTitleEpisode("tt0697644");
            Assert.Equal("tt0098904", title.TitleEpisode.ParentId);
            Assert.Equal(6, title.TitleEpisode.SeasonNumber);
        }

        [Fact]
        public void GetTitleGenreNames()
        {
            var service = new TitleDataService();
            var title = service.getTitleGenreName("tt0078672");
            Assert.Equal("Comedy", title.TitleGenres.First().Genre.Name);
        }

        [Fact]
        public void getTitlePerson()
        {
            var service = new TitleDataService();
            var titlePerson = service.getTitlePersons("tt0098286");
            Assert.Equal(10, titlePerson.Count);
        }

        [Fact]
        public void getTitlePersonName()
        {
            //test midlertidligt unpassable :P
            var service = new TitleDataService();
            var names = service.getTitlePersonName("tt0098286");
            Assert.Equal("George La Fountaine Sr.", names.TitlePersons.First().Person.Name);
        }

        [Fact]
        public void getGenres()
        {
            var service = new TitleDataService();
            var genres = service.GetGenres();
            Assert.Equal(28, genres.Count);
        }

        [Fact]
        public void getTitles()
        {
            var service = new TitleDataService();
            var titles = service.GetTitles();
            Assert.Equal(55076, service.GetTitles().Count);
        }

        [Fact]
        public void getAkas()
        {
            var service = new TitleDataService();
            var Akas = service.GetAkas(14);
            Assert.Equal("tt0052520", Akas.TitleId);
        }

        [Fact]
        public void getEpisodesOfTitle()
        {
            var service = new TitleDataService();
            var Episodes = service.GetMoreTitleEpisode("tt0756483");
            Assert.Equal(235, Episodes.Count);
            Assert.Equal("The One Where Chandler Doesn't Like Dogs", Episodes.First().Title.PrimaryTitle);
        }

        [Fact]
        public void getTypeTitles()
        {
            var service = new TitleDataService();
            var typeTitles = service.GetTypeTitles(1);
            Assert.Equal(142, typeTitles.Count);
        }

        [Fact]
        public void getOmdb()
        {
            var service = new TitleDataService();
            var omdb = service.GetOmdbData("tt10066406");
            Assert.Equal("https://m.media-amazon.com/images/M/MV5BYjAyMTUwMTYtZWNhYi00OTFlLThlNTAtYTNlMGI4Mjc1MjBhXkEyXkFqcGdeQXVyMjYxMDY3NjA@._V1_SX300.jpg"
            , omdb.Poster);

        }
    }
}