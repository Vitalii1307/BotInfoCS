using System;
using System.Collections.Generic;
using System.Text;
using TestBot.Parser.Interface;

namespace TestBot.Parser.Source
{
    class BCSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://minfin.com.ua/currency/crypto/";
    }
}
