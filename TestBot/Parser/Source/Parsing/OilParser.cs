using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBot.Parser.Interface;

namespace TestBot.Parser.Source
{
    class OilParser : IParser<string[]>
    {
        string[] Titles = new string[2];
        string[] BrandOil = new string[2];
        string[] Price = new string[2];

        public string[] Parse(IHtmlDocument document)
        {
            var titlesNames = document.QuerySelectorAll("th");

            var brandNames = document.QuerySelectorAll("td")
            .Where(m => m.HasAttribute("align") && m.GetAttribute("align").Contains("left"));

            var priceValue = document.QuerySelectorAll("big");

            for (int i = 0; i < Titles.Length; i++)
            {
                Titles[i] = titlesNames[i].TextContent;
                BrandOil[i] = brandNames.ElementAt(i).TextContent;
                Price[i] = priceValue[i].TextContent;
            } 

            return ResultString();
        }

        private string[] ResultString() 
        {
            string[] result = new string[3];
            result[1] = BrandOil[0] + ": " + Price[0] + " USD";
            result[2] = BrandOil[1] + ": " + Price[1] + " USD";
            return result;
        }
    }
}
