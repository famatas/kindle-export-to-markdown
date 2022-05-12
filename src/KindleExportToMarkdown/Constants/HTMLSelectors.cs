namespace KindleExportToMarkdown.Constants
{
    public static class HTMLSelectors
    {        
        public const string BookTitle = $"div.{BookClasses.BookClass}";
        public const string Authors = $"div.{BookClasses.AuthorsClass}";
        public const string ChapterTitle = $"div.{BookClasses.ChapterClass}";
        public const string Note = $"div.{BookClasses.NoteClass}";
        public const string HighlightText = $"div.{BookClasses.HighlightClass}";
    }
}
