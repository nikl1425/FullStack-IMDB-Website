using System.Collections.Generic;

namespace WebService.ObjectDto
{
    public class PersonBookmarkDto
    {
        public int Id { get; set; }
        public int List_Id { get; set; }
        public string Person_Id { get; set; }
        public string PersonName { get; set; }
        public string BirthYear { get; set; }
        public string Url { get; set; }
    }
}