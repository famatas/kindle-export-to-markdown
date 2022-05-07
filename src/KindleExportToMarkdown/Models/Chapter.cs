namespace KindleExportToMarkdown.Models
{
    public class Chapter : ICloneable
    {
        public string Title { get; set; }

        public List<Subchapter> Subchapters { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
