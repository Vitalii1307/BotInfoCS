using System;
using System.Collections.Generic;
using System.Text;
using TestBot.Parser.Interface;

namespace TestBot.Parser.Source.Settings
{
    class MoneyCurrencySettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://minfin.com.ua/currency/";
    }
}
