using System;
using System.Collections.Generic;
using System.Linq;
using DataService;
using DataService.Objects;
using DataService.Services;
using DataService.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic.CompilerServices;

namespace TestQueryConsole
{
    class Program
    {


        static void Main(string[] args)
        {
            using var ctx = new ImdbContext();


            PersonDataService personDataService = new PersonDataService();

            //Console.WriteLine(personDataService.GetPersonKnownTitles("nm0000001"));

            IQueryable<Person> GetPersonBySubstring(string substring)
            {
                var ctx = new ImdbContext();
                var query = ctx.Person.Where(x => x.Name.Contains(substring));
                return query;
            }

            //GetPersonBySubstring("red ast")


            List<Person_Profession> GetProfessionByPersonId(string id)
            {
                var query = ctx.PersonProfessions
                    .Include(c => c.person)
                    .Include(v => v.Profession)
                    .Where(p => p.person.Id == id)
                    .ToList();

                ctx.SaveChanges();

                return query.ToList();
            }



            List<Person_Profession> GetPersonsByProfession (string profession)
            {
                var ctx = new ImdbContext();
                var personList = ctx.PersonProfessions
                    .Include(x => x.person)
                    .Include(c => c.Profession)
                    .Where(personProfession => personProfession.Profession.ProfessionName == profession);
                return personList.ToList();
            }

            IList<OmdbHolder> GetOmdbDatas()
            {
                using var ctx = new ImdbContext();
                var query = ctx.omdb_data
                    .Include(x => x.Title)
                    .Select(x => new OmdbHolder
                    {
                        Id = x.Id,
                        Poster = x.Poster,
                        PrimaryTitle = x.Title.PrimaryTitle
                    })
                    .ToList();

                return query;
            }


            Console.WriteLine(GetPersonsByProfession(GetOmdbDatas().ToString()));







        }
    }
}