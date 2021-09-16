using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBot.Parser.Interface;

namespace TestBot.Parser.Source.Parsing
{
    class MoneyCurrency : IParser<string[]>
    {
       
        public string[] Parse(IHtmlDocument document)
        {
            string[] resultCurrency = new string[6];
            string[] result = { "US Dollar", "Euro", "Ruble", "Polish Zloty", "British Pound", "Swiss Franc" };
            var namesCurrency = document.QuerySelectorAll("td");           

            var valueCurrency = document.QuerySelectorAll("td")
                .Where(m => m.ClassName != null && m.ClassName.Contains("mfm-text-nowrap") && m.ClassName != "mfm-text-light-grey mfm-posr");

            for (int i = 0; i < resultCurrency.Length; i++)
            {
                resultCurrency[i] = $"{namesCurrency.ElementAt(i * 4).TextContent.Replace("\n", "")}({result[i]}): " +
                    $"{valueCurrency.ElementAt((i * 2) + 1).TextContent.Replace("\n", "").Replace(" ", "")} UAH";                
            }
            return resultCurrency;
        }
    }
}
