namespace KindleExportToMarkdown.Models
{
    public class Subchapter : ICloneable
    {
        public string Title {  get; set; }
        public List<Highlight> Highlights { get; set; }

        public Subchapter Clone()
        {
            return (Subchapter)MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }
}
