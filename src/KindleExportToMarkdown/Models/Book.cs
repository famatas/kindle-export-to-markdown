namespace KindleExportToMarkdown.Models
{
    public class Book
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public Chapter[] Chapters { get; set; }
        
    }
}
