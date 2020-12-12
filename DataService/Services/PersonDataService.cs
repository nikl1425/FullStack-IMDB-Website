using System.Collections.Generic;
using System.Linq;
using DataService.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataService.Services
{
    public class PersonDataService : IPersonDataService
    {
        public PersonDataService()
        {
            using var ctx = new ImdbContext();
        }
        
        public List<Person> GetPerson(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.Person.Where(person => person.Id == id);
            return query.ToList();
        }

        public IQueryable<Person> GetPersonBySubstring(string substring)
        {
            var ctx = new ImdbContext();
            var query = ctx.Person.Where(x => x.Name.ToLower().Contains(substring.ToLower()));
            return query;
        }

       
        public List<Person> GetPersons()
        {
            using var ctx = new ImdbContext();
            return ctx.Person.ToList();
        }

        public Person_known_title GetPersonKnownTitle(string person_id, string title_id)
        {
            using var ctx = new ImdbContext();
            return ctx.PersonKnownTitles.Find(person_id, title_id);
        }
        


        //Returns person with all the professions the person has
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
        
        //Returns only the professions of whoever we search
        public List<Person_Profession> GetProfessionByPersonId2(string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.PersonProfessions
                .Include(c => c.person)
                .Include(v => v.Profession)
                .Where(p => p.person.Id == id)
                .ToList();

            ctx.SaveChanges();

            return query.ToList();
        }
        
         
        public List<Person_known_title> GetPersonKnownTitles (string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.PersonKnownTitles
                .Include(x => x.person)
                .Where(c => c.person.Id == id);
            return query.ToList();
        }


        public List<Person_Profession> GetPersonsByProfession (string profession)
        {
            var ctx = new ImdbContext();
            var personList = ctx.PersonProfessions
                .Include(x => x.person)
                .Include(c => c.Profession)
                .Where(personProfession => personProfession.Profession.ProfessionName == profession);
            return personList.ToList();
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
        
        public IList<Person_Profession> GetPersonProfessions (string id)
        {
            using var ctx = new ImdbContext();
            var query = ctx.PersonProfessions
                .Include(x => x.Profession)
                .Where(x => x.PersonId == id)
                .ToList();
            return query;
        }
        
        public List<PersonWithProfession> GetAllProfessions()
        {
            using var ctx = new ImdbContext() ;
            var query = ctx.PersonWithProfessions.FromSqlInterpolated($"select * from professionsforpeoplepage()").ToList();;
            
            return query.ToList();
        }
    }
}