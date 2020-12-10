using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataService.Objects;
using DataService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WebService.ObjectDto;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PersonController : ControllerBase
    {
        private IPersonDataService _dataService;
        private ITitleDataService _titleDataService;
        private readonly IMapper _mapper;

        public PersonController(IPersonDataService dataService, ITitleDataService titleDataService ,IMapper mapper)
        {
            _dataService = dataService;
            _titleDataService = titleDataService;
            _mapper = mapper;
        }


        [HttpGet("name/{id}", Name = nameof(GetPerson))]
        public IActionResult GetPerson(string id)
        {
            var person = _dataService.GetPerson(id);
            var profession = _dataService.GetProfessionByPersonId2(id);
            var personKnownTitle = _dataService.GetPersonKnownTitles(id);

            IList<ProfessionDTO> professionDtos = profession.Select(x => new ProfessionDTO
            {
                ProfessionName = x.Profession.ProfessionName,
                Id = x.Profession.Id,
                Url = "http://localhost:5001/api/" + x.Profession.ProfessionName
            }).ToList();

            IList<PersonDTO> personDtos = person.Select(x => new PersonDTO
            {
                Id = x.Id,
                Name = x.Name,
                BirthYear = x.BirthYear,
                DeathYear = x.DeathYear
            }).ToList();

            IList<PersonKnownTitleDTO> personKnownTitleDtos = personKnownTitle.Select(x => new PersonKnownTitleDTO
            {
                Id = x.Id,
                TitleId = x.TitleId,
                TitleName = _titleDataService.GetTitle(x.TitleId).OriginalTitle.ToString(),
                Url = "http://localhost:5001/api/title/" + x.TitleId
            }).ToList();

            return Ok(new {personDtos, professionDtos, personKnownTitleDtos});
        }


        [HttpGet("{profession}")]
        public IActionResult GetPersonsByProfession(string profession)
        {
            var persons = _dataService.GetPersonsByProfession(profession).GetRange(0, 500);
            
            IList<PersonDTO> newPersonDTO = persons.Select(x => new PersonDTO
            {
                Id = x.person.Id,
                Name = x.person.Name,
                BirthYear = x.person.BirthYear,
                DeathYear = x.person.DeathYear,
                Url = "http://localhost:5001/api/name/" + x.person.Id
            }).ToList();

            return Ok(newPersonDTO);

        }
        
        [HttpGet("name/")]
        public IActionResult GetPersons()
        {
            //brug .GetRange(0, 500) til at limit
            var person = _dataService.GetPersons().GetRange(0, 20);
            
            
            IList<PersonDTO> newPersonDTO = person.Select(x => new PersonDTO
            {
                
                Id = x.Id,
                Name = x.Name,
                BirthYear = x.BirthYear,
                DeathYear = x.DeathYear,
                Professions = _dataService.GetPersonProfessions(x.Id).Select(x => x.Profession.ProfessionName).ToList(),
                Url = "http://localhost:5001/api/name/" + x.Id
            }).ToList();
            
            return Ok(newPersonDTO);
        }

        [HttpGet("name/{id}/profession")]
        public IActionResult GetProfession(string id)
        {
            var personProfessions = _dataService.GetProfessionByPersonId(id);
            return Ok(personProfessions);
        }
    }
}