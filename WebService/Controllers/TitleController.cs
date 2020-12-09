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
            titleDto.TypeUrl = "http://localhost:5001/api/type/" + titleType.Type.Id;

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

            titleDto.poster = poster.Poster;
            
            
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
                
                Url = "http://localhost:5001/api/title/" + x.TitleId
            }).ToList();
            

            foreach (var episode in TitleEpisodes)
            {
                episode.ParentTitleName = titleEpisodeParentName;
            }


            return Ok(new {titleDto, TitleGenres, TitleAkases, TitleEpisodes, TitlePersons});
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
                Awards = x.Awards,
                Poster = x.Poster
            }).ToList();

            return Ok(posterItems);
        }

        [HttpGet("Movies", Name = nameof(GetAllMovies))]
        public IActionResult GetAllMovies(int page = 0, int pageSize = 10)
        {
            var query = _dataService.GetAllMovies();
            IList<MoviesDto> movies = query.Select(x => new MoviesDto
            {
                title_id = x.title_id,
                poster = x.poster,
                plot = x.plot,
                runtime = x.runtime,
                genre = x.genre,
                votes = x.votes,
                rating = x.rating,
                type = x.type
            }).ToList();

            return Ok(movies);
        }
        
        
        
        
    }
}