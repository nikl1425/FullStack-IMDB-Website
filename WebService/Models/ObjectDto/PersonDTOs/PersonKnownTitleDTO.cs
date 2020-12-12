using System.Collections.Generic;
using DataService.Objects;

namespace WebService.ObjectDto
{
    public class PersonKnownTitleDTO
    {
        public string Id { get; set; }
       
        public string TitleId { get; set; }
        
        public string TitleName { get; set; }
        
        public string Poster { get; set; }
        
        public string ProductionYear { get; set; }

        public string Url { get; set; }
    }
}