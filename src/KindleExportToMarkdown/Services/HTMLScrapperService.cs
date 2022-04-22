using HtmlAgilityPack;
using KindleExportToMarkdown.Interfaces;

namespace KindleExportToMarkdown.Services
{
    public class HTMLScrapperService : IScrapperService
    {
        public string GetTitle(string document)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(document);

            
            // var htmlBody = htmlDoc.DocumentNode.SelectSingleNode("//body");


            return "";
            //return htmlBody.InnerText;
        }
    }
}
