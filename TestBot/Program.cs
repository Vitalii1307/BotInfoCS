using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TestBot.Parser;
using TestBot.Parser.Source;
using System.Threading;
using TestBot.Parser.Source.Parsing;
using TestBot.Parser.Source.Settings;

namespace TestBot
{
    class Program
    {
        private static string token { get; set; } = "1863340523:AAEHEg3LCLkwTKbACi26i7pYO_0opKPWUcM";
        private static TelegramBotClient client;
        private static ParserWorker<string[]> parserCrypto;
        private static ParserWorker<string[]> parserOil;
        private static ParserWorker<string[]> parserMoney;

        private static string result = null;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            parserCrypto = new ParserWorker<string[]>(new BCParser());
            parserCrypto.ParserSettings = new BCSettings();

            parserOil = new ParserWorker<string[]>(new OilParser());
            parserOil.ParserSettings = new OilSettings();

            parserMoney = new ParserWorker<string[]>(new MoneyCurrency());
            parserMoney.ParserSettings = new MoneyCurrencySettings();

            parserCrypto.OnNewData += Parser_OnNewData;
            parserOil.OnNewData += Parser_OnNewData;
            parserMoney.OnNewData += Parser_OnNewData;
            client.OnMessage += OnMessageHandler;

            client.StartReceiving();

            Thread.Sleep(int.MaxValue);
        }

       

        private static void Parser_OnNewData(object arg1, string[] arg2)
        {
            result = null;

            string temp = null;

            foreach (var item in arg2)
            {
                temp += item + "\n";
            }
            result = temp;
            
        }

        private static async void OnMessageHandler(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
           
            var msg = e.Message;

            if (msg.Text != null)
            {
                Console.WriteLine($"Message came: {msg.Text}");

                switch (msg.Text)
                {

                    case "Hello":
                        SendSticers(msg, "https://cdn.tlgrm.ru/stickers/972/d03/972d03b1-80b4-43ac-8063-80e62b150d91/192/1.webp");
                        break;
                    case "GoodBye":
                        SendSticers(msg, "https://tlgrm.ru/_/stickers/972/d03/972d03b1-80b4-43ac-8063-80e62b150d91/192/29.webp");
                        break;
                    case "Money exchange rates":
                        await parserMoney.Worker();
                        SendParseInfo(msg, result);
                        break;
                    case "Oil price":
                        await parserOil.Worker();
                        SendParseInfo(msg, result);
                        break;
                    case "Crypto currency":
                        await parserCrypto.Worker();
                        SendParseInfo(msg, result);
                        break;
                    default:
                        await client.SendTextMessageAsync(msg.Chat.Id, "Select the desired button", replyToMessageId: msg.MessageId, replyMarkup: GetButtons());
                        break;
                }
            }
        }

        private static IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = "Hello" }, new KeyboardButton { Text = "GoodBye" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Money exchange rates" }, new KeyboardButton { Text = "Oil price" } },
                    new List<KeyboardButton> { new KeyboardButton { Text = "Crypto currency" }}
                }
            };
        }
        private static async void SendSticers(Telegram.Bot.Types.Message msg, string link)
        {
            
            await client.SendStickerAsync(
                            chatId: msg.Chat.Id,
                            sticker: link,
                            replyToMessageId: msg.MessageId,
                            replyMarkup: GetButtons()).ConfigureAwait(false);
        }
        private static async void SendParseInfo(Telegram.Bot.Types.Message msg, string result)
        {
                if (result != null)
                {
                    await client.SendTextMessageAsync(msg.Chat, result, replyMarkup: GetButtons());                   
                }
        }
    }
}
