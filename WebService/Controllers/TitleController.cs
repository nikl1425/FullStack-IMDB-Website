using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataService.Objects;
using DataService.Services;
using Microsoft.AspNetCore.Mvc;
using WebService.ObjectDto;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/title")]
    public class TitleController : ControllerBase
    {
        private ITitleDataService _dataService;
        private readonly IMapper _mapper;
        private GenreController _genreController;
        private const int MaxPageSize = 25;

        public TitleController(ITitleDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetAllMovies))]
        public IActionResult GetAllMovies(int page = 0, int pageSize = 20)
        {
            var movieList = _dataService.GetAllMovies(page, pageSize).Select(CreateDto);
            var numberOfMovies = _dataService.GetNumberOfMovies();

            var pages = (int) Math.Ceiling((double) numberOfMovies / pageSize);

            var prev = (string) null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetAllMovies), new {page = page - 1, pageSize});
            }

            var next = (string) null;

            if (page < pages - 1)
            {
                next = Url.Link(nameof(GetAllMovies), new {page = page + 1, pageSize});
            }


            IList<MoviesDto> movies = movieList.Select(x => new MoviesDto
            {
                title_id = x.title_id,
                title_name = x.title_name,
                poster = x.poster,
                plot = x.plot,
                runtime = x.runtime,
                genre = _dataService.GetTitleGenres(x.title_id).Select(x => x.Genre.Name).ToList(),
                votes = x.votes,
                rating = x.rating,
                type = x.type,
                Url = "http://localhost:5001/api/title/" + x.title_id
            }).ToList();

            var result = new
            {
                pageSizes = new int[] {5, 10, 15, 20},
                count = numberOfMovies,
                pages,
                prev,
                next,
                movieList
            };

            return Ok(result);
        }


        [HttpGet("type/{typeName}", Name = nameof(GetAllMoviesType))]
        public IActionResult GetAllMoviesType(string typeName, int page = 0, int pageSize = 20)
        {
            var movieList = _dataService.GetAllMoviesWithType(typeName, page, pageSize).Select(CreateDto);
            var numberOfMovies = _dataService.GetNumberOfMoviesWithType(typeName);

            var pages = (int) Math.Ceiling((double) numberOfMovies / pageSize);

            var prev = (string) null;

            if (page > 0)
            {
                prev = Url.Link(nameof(GetAllMoviesType), new {page = page - 1, pageSize});
            }

            var next = (string) null;

            if (page < pages - 1)
            {
                next = Url.Link(nameof(GetAllMoviesType), new {page = page + 1, pageSize});
            }


            IList<MoviesDto> movies = movieList.Select(x => new MoviesDto
            {
                title_id = x.title_id,
                title_name = x.title_name,
                poster = x.poster,
                plot = x.plot,
                runtime = x.runtime,
                genre = _dataService.GetTitleGenres(x.title_id).Select(x => x.Genre.Name).ToList(),
                votes = x.votes,
                rating = x.rating,
                type = x.type,
                Url = "http://localhost:5001/api/title/" + x.title_id
            }).ToList();

            var result = new
            {
                pageSizes = new int[] {5, 10, 15, 20},
                count = numberOfMovies,
                pages,
                prev,
                next,
                movieList
            };

            return Ok(result);
        }


/*
        [HttpGet]
        public IActionResult AllTitles()
        {
            var titles = _dataService.GetOmdbDatas();

            IList<TitleListDto> items = titles.Select(x => new TitleListDto
            {
                Id = x.Id,
                Url = "http://localhost:5001/api/title/" + x.Id,
                PrimaryTitle = x.PrimaryTitle,
                OriginalTitle = x.OriginalTitle,
                IsAdult = x.IsAdult,
                StartYear = x.StartYear,
                EndYear = x.EndYear,
                Poster = x.Poster
            }).ToList();
            
            

            return Ok(items);
        }
*/
        [HttpGet("{id}", Name = nameof(GetTitle))]
        public IActionResult GetTitle(string id)
        {
            var title = _dataService.GetTitle(id);
            var titleGenre = _dataService.GetTitleGenres(id);
            var titleAkas = _dataService.GetTitleAkas(id);
            var titleEpisode = _dataService.GetMoreTitleEpisode(id);
            var titleEpisodeParentName = _dataService.GetTitleEpisodeParentName(id);
            var titlePerson = _dataService.GetTitlePersons(id);
            var titleType = _dataService.GetTitleType(id);
            var poster = _dataService.GetOmdbData(id);
            var titleRating = _dataService.GetTitleRating(id);
            var titleRuntime = _dataService.GetTitleRuntime(id);
            var titlePlot = _dataService.GetOmdbData(id);
            var personsInMovie = _dataService.GetPersonsInMovie(id);

            if (title == null)
            {
                return NotFound();
            }

            var titleDto = _mapper.Map<TitleDto>(title);

            titleDto.Url = Url.Link(nameof(GetTitle), new {id});

            if (titleGenre == null)
            {
                return NotFound();
            }

            titleDto.Type = titleType.Type.TypeName;
            if (titleRuntime == null)
            {
                titleDto.runtime = 0;
            }
            else
            {
                titleDto.runtime = titleRuntime.Runtime;
            }

            titleDto.TypeUrl = "http://localhost:5001/api/type/" + titleType.Type.Id;
            titleDto.Rating = titleRating.Average_Rating;
            if (titlePlot == null)
            {
                titleDto.plot = "";
            }
            else
            {
                titleDto.plot = titlePlot.Plot;
            }


            titleDto.regions = titleAkas.Select(x => x.Region).ToList();
            titleDto.languages = titleAkas.Select(x => x.Language).Where(x => x.Length >= 1).ToList();


            IList<TitleGenreDTO> TitleGenres = titleGenre.Select(x => new TitleGenreDTO
            {
                Name = x.Genre.Name,
                Url = "http://localhost:5001/api/genre/" + x.GenreId
            }).ToList();

            IList<TitleAkasDTO> TitleAkases = titleAkas.Select(x => new TitleAkasDTO
            {
                Name = x.AkasName,
                Region = x.Region,
                Url = "http://localhost:5001/api/title/akas/" + x.Id
            }).ToList();


            IList<TitlePersonDTO> TitlePersons = titlePerson.Select(x => new TitlePersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                Url = "http://localhost:5001/api/name/" + x.Id
            }).ToList();

            if (poster == null)
            {
                titleDto.poster = "https://i.imgur.com/Z2MYNbj.png/large_movie_poster.png";
            }
            else
            {
                titleDto.poster = poster.Poster;
            }


            if (titleEpisode == null)
            {
                return Ok(new
                {
                    titleDto, TitleGenres, TitleAkases, TitlePersons
                });
            }

            IList<TitleEpisodeDto> TitleEpisodes = titleEpisode.Select(x => new TitleEpisodeDto
            {
                Id = x.TitleId,
                TitleName = x.Title.PrimaryTitle,
                Poster = _dataService.GetOmdbData(x.TitleId).Poster ?? _dataService.GetOmdbData("tt11000576").Poster,
                Url = "http://localhost:5001/api/title/" + x.TitleId
            }).ToList();


            foreach (var episode in TitleEpisodes)
            {
                episode.ParentTitleName = titleEpisodeParentName;
            }

            Random rnd = new Random();
            var limitedEpisodes = TitleEpisodes.OrderBy(x => rnd.Next()).Take(6);


            return Ok(new {titleDto, TitleGenres, TitleAkases, limitedEpisodes, TitlePersons});
        }

        private TitleListDto CreateObjectOfTitle(Title title)
        {
            var dto = _mapper.Map<TitleListDto>(title);
            dto.Url = Url.Link(nameof(GetTitle), new {title.Id});
            return dto;
        }

        [HttpGet("akas/{id}", Name = nameof(GetAkas))]
        public IActionResult GetAkas(int id)
        {
            var akas = _dataService.GetAkas(id);
            var akasDto = _mapper.Map<AkasDto>(akas);

            akasDto.AkasType = akas.AkasType.Name;
            akasDto.Url = Url.Link(nameof(GetAkas), new {id});
            akasDto.TitleUrl = "http://localhost:5001/api/title/" + akas.TitleId;

            return Ok(akasDto);
        }

        [HttpGet("img/{id}", Name = nameof(GetTitleImg))]
        public IActionResult GetTitleImg(string id)
        {
            var img = _dataService.GetOmdbData(id);

            var omdbDto = _mapper.Map<Omdb_dataDto>(img);

            return Ok(omdbDto);
        }

        [HttpGet("TopPoster", Name = nameof(GetTopPosters))]
        public IActionResult GetTopPosters()
        {
            var posters = _dataService.GetTopTenPoster();

            IList<TopTenPosterDto> posterItems = posters.Select(x => new TopTenPosterDto
            {
                Id = x.Id,
                Title = _dataService.GetTitle(x.Id).PrimaryTitle,
                Awards = x.Awards,
                Poster = x.Poster
            }).ToList();

            return Ok(posterItems);
        }


        MoviesDto CreateDto(Movies movie)
        {
            var dto = _mapper.Map<MoviesDto>(movie);
            dto.Url = "http://localhost:5001/api/title/" + movie.title_id;
            dto.genre = _dataService.GetTitleGenres(movie.title_id).Select(x => x.Genre.Name).ToList();
            return dto;
        }
    }
}