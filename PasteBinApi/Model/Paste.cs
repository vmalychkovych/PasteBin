namespace PasteBinApi.Model
{
    public class Paste
    {
        public int Id { get; set; }
        public string Title {  get; set; }
        public string Content { get; set; }
        public string ContentURL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiritionDate { get; set; }
        public string UrlSlug { get; set; }
        
    }
}
