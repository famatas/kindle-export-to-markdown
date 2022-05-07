namespace KindleExportToMarkdown.Models
{
    public class Highlight : ICloneable
    {
        public string Content { get; set; }
        public string Page { get; set; }

        public object Clone()
        {
            return (Highlight)MemberwiseClone();
        }
    }
}
