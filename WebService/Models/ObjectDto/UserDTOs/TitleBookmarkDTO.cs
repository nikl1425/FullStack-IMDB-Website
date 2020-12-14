namespace WebService.ObjectDto
{
    public class TitleBookmarkDTO
    {
        public int Id { get; set; }
        public string TitleId { get; set; }
        public int ListId { get; set; }
        public string Url { get; set; }
        public string titleName { get; set; }
        public string prodYear { get; set; }
        public string? Poster { get; set; }
        public string plot { get; set; }
    }
}