namespace KindleExportToMarkdown.Models
{
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public List<Chapter> Chapters { get; set; }
        
    }
}
