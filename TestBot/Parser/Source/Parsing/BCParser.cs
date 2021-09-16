using AngleSharp.Html.Dom;
using System.Collections.Generic;
using System.Linq;
using TestBot.Parser.Interface;

namespace TestBot.Parser.Source
{
    class BCParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {

            var listName = new List<string>();
            var listPrice = new List<string>();
            var resultList = new List<string>();

            var namesCrpt = document.QuerySelectorAll("span")
               .Where(item => item.ClassName != null && item.ClassName.Contains("blue coin-name--long"));

            var prices = document.QuerySelectorAll("span")
               .Where(item => item.ClassName != null && item.ClassName.Contains("coin-price--num"));

            foreach (var item in namesCrpt)
            {
                listName.Add(item.TextContent);
            }
            foreach (var item in prices)
            {
                listPrice.Add(item.TextContent);
            }

            for (int i = 0; i < listName.Count(); i++)
            {
                resultList.Add((listName[i] + ":" + listPrice[i]).Replace(" ", "").Replace("USD", " USD").Replace(":", ": "));
            }


            return resultList.ToArray();
        }
    }
}
