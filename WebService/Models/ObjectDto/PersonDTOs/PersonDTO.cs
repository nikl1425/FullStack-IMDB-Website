using System.Collections.Generic;
using DataService.Objects;

namespace WebService.ObjectDto
{
    public class PersonDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        
        public List<string?> Professions { get; set; }
        public string Url { get; set; }
    }
}