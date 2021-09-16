using System;
using System.Collections.Generic;
using System.Text;
using TestBot.Parser.Interface;

namespace TestBot.Parser.Source
{
    class OilSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://index.minfin.com.ua/markets/oil/";
    }
}
