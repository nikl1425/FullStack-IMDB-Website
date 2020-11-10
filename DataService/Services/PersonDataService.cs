using System.Collections.Generic;
using System.Linq;
using DataService.Objects;
using Microsoft.EntityFrameworkCore;

namespace DataService.Services
{
    public class PersonDataService : IPersonDataService
    {
        public PersonDataService()
        {
            using var ctx = new ImdbContext();
        }
        
        public Person GetPerson(string id)
        {
            using var ctx = new ImdbContext();
            //var smthing = ctx.Person.FirstOrDefault(t => t.Id.Trim().ToLower() == id.Trim().ToLower());

            return ctx.Person.Find(id);
        }
        public IList<Person> GetPersons()
        {
            using var ctx = new ImdbContext();
            return ctx.Person.ToList();
        }

        public Person_known_title GetPersonKnownTitle(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.PersonKnownTitles.Find(id);
        }
        

        public Person_Person_Known_Title GetPersonPersonKnownTitle(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.PersonPersonKnownTitles.Find(id);
        }
        
        public Person GetProfessionByPersonId(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.Person
                .Include(x => x.PersonProfessions)
                .ThenInclude(z => z.Profession)
                .AsSingleQuery()
                .FirstOrDefault(c => c.Id == id);
            return query;
        }
        
        public Person GetPersonKnownTitles(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.Person
                .Include(x => x.PersonPersonKnownTitles)
                .ThenInclude(x => x.PersonKnownTitles)
                .AsSingleQuery()
                .FirstOrDefault(x => x.Id == id);
            return query;
        }

        
        public Profession GetPersonAmountByProfession (string profession)
        {
            var ctx = new ImdbContext();
            var personList = ctx.Professions
                .Include(x => x.PersonProfessions)
                .AsSingleQuery()
                .FirstOrDefault(v => v.ProfessionName == profession);

            return personList;
        }
        
        public Person_Profession GetPersonProfession(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.PersonProfessions.Find(id);
        }
        
        public Profession GetProfession(int id)
        {
            using var ctx = new ImdbContext();
            return ctx.Professions.Find(id);
        }
    }
}