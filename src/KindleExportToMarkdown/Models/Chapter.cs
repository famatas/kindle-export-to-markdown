namespace KindleExportToMarkdown.Models
{
    public class Chapter
    {
        public string Title { get; set; }

        public List<Subchapter> Subchapters { get; set; }
    }
}
