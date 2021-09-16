using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestBot.Parser.Interface;

namespace TestBot.Parser
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;
        HtmlLoader loader;
        //bool CryptoOrOil { get; set; }//true - crypto, false - oil

        #region
        public IParser<T> Parser { get { return parser; } set { parser = value; } }
        public IParserSettings ParserSettings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(parserSettings);
            }
        }
        #endregion

        public event Action<object, T> OnNewData;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
          
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }


        public async Task Worker()
        {

            var source = await loader.GetSourcePageAsync();

            var domParser = new HtmlParser();

            var document = await domParser.ParseDocumentAsync(source);
            string str = document.Title;

            var result = parser.Parse(document);
            OnNewData?.Invoke(this, result);
        }
    }
}
