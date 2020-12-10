using System.Collections.Generic;

namespace WebService.ObjectDto
{
    public class MoviesDto
    {
        public string title_id { get; set; }
        public string title_name { get; set; }
        public string poster { get; set; }
        public string plot { get; set; }
        public string runtime { get; set; }
        
       public List<string> genre { get; set; }
        public string votes { get; set; }
        public string rating { get; set; }
        public string type { get; set; }
        public string Url { get; set; }
    }
}