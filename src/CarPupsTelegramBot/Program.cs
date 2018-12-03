using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using CarPupsTelegramBot.Api;
using CarPupsTelegramBot.Commands;
using CarPupsTelegramBot.Data;
using CarPupsTelegramBot.Models.ReturnModels.MessageReturnModels;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot
{
    class Program
    {
        public static int awooCount = 1;
        public static ITelegramBotClient botClient;

        static void Main()
        {
            SetupApp();
            HelpData.CompileHelpDictionary();   

            botClient = new TelegramBotClient(AppSettings.ApiKeys_Telegram);
            
            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Thread.Sleep(int.MaxValue);
        }

        static void Bot_OnMessage(object sender, MessageEventArgs e) {
            if(e.Message.Text != null) {
                try {
                    ConsoleOutputUtilities.MessageInConsoleMessage(e);
                    
                    var messageText = e.Message.Text.ToString();

                    var command = messageText.Split(" ")[0].Replace("/", "").Replace("@CarPups_bot", "").ToLower();
                    string[] arguments = null;

                    Console.WriteLine(StringUtilities.CountWords(messageText));

                    if(StringUtilities.CountWords(messageText) == 1) {
                        if(HelpData.HelpDictionary.ContainsKey(command)) {
                            string helpOutput = HelpData.GetHelp(command, false);
                            MessageApi.SendTextMessage(helpOutput, botClient, e);
                        } else {
                            arguments = (messageText.Substring(messageText.IndexOf(' ') + 1)).Split(" ");
                            RunCommand(command, arguments, e);
                        }
                    } else {
                        arguments = (messageText.Substring(messageText.IndexOf(' ') + 1)).Split(" ");
                        RunCommand(command, arguments, e);
                    }
                } catch {

                }
            }
        }

        static void RunCommand(string command, string[] arguments, MessageEventArgs telegramMessageEvent)
        {
            switch(command) {
                case "awoo":
                        string awooOutput;

                        if(AppSettings.Config_Awoo_Repeat) {
                            awooOutput = String.Concat(Enumerable.Repeat(AppSettings.Config_Awoo_Word, awooCount));
                            awooCount++;
                        } else {
                            awooOutput = AppSettings.Config_Awoo_Word;
                        }

                        MessageApi.SendTextMessage(awooOutput, botClient, telegramMessageEvent);
                    break;
                case "calculate0to60":
                        string calculate0To60Output = "";

                        if(arguments.Length == 4) {
                            calculate0To60Output = ZeroToSixty.Calculate(arguments[0], arguments[1], arguments[2], arguments[3]);
                        } else if(arguments.Length == 5) {
                            calculate0To60Output = ZeroToSixty.Calculate(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("calculate0to60", false), botClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(calculate0To60Output, botClient, telegramMessageEvent);
                    break;
                case "calculatejourneyprice":
                        string calculateJourneyPriceOutput = "";
                
                        if (arguments.Length == 3) {
                            calculateJourneyPriceOutput = JourneyPrice.Calculate(arguments[0], arguments[1], arguments[2]);
                        }
                
                        MessageApi.SendTextMessage(calculateJourneyPriceOutput, botClient, telegramMessageEvent);
                    break;
                case "getfuelly":
                        ImageMessageReturnModel getFuellyOutput = null;

                        if (arguments.Length == 1) {
                            getFuellyOutput = Fuelly.Get(arguments[0]);
                        } else if (arguments.Length == 2) {
                            getFuellyOutput = Fuelly.Get(arguments[0], arguments[1]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("getfuelly", false), botClient, telegramMessageEvent);
                        }

                        MessageApi.SendPhotoMessage(getFuellyOutput, botClient, telegramMessageEvent);
                    break;
                case "guessmileage":
                        string guessMileageOutput = "";

                        if (arguments.Length == 3) {
                           guessMileageOutput = Mileage.Guess(arguments[0], Convert.ToInt32(arguments[1]), arguments[2]);
                        } else if (arguments.Length == 4)  {
                           guessMileageOutput = Mileage.Guess(arguments[0], Convert.ToInt32(arguments[1]), arguments[2], arguments[3]);
                        }

                        MessageApi.SendTextMessage(guessMileageOutput, botClient, telegramMessageEvent);
                    break;
                case "info":
                        string infoOutput = "";
                        infoOutput = Info.Get();
                        MessageApi.SendTextMessage(infoOutput, botClient, telegramMessageEvent);
                    break;
                case "parseplate":
                        string parsePlateOutput = "";

                        if(arguments.Length == 1) {
                            parsePlateOutput = Plate.Parse(arguments[0], "gb");
                        } else if (arguments.Length == 2) {
                            parsePlateOutput = Plate.Parse(arguments[0], arguments[1]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("parseplate", false), botClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(parsePlateOutput, botClient, telegramMessageEvent);

                    break;
                //case "setfuelly":
                //        string setFuellyOutput = "";
                //
                //        if (arguments.Length == 1) {
                //            setFuellyOutput = Fuelly.Set(arguments[0]);
                //        }
                //
                //        MessageApi.SendTextMessage(setFuellyOutput, botClient, telegramMessageEvent);
                //    break;
            }
        }

        public static void SetupApp() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            AppSettings.ApiKeys_Telegram = configuration.GetSection("apiKeys")["telegram"];
            AppSettings.Config_Awoo_Repeat = bool.Parse(configuration.GetSection("config").GetSection("awoo")["repeat"]);
            AppSettings.Config_Awoo_Word = configuration.GetSection("config").GetSection("awoo")["word"];

            //Console.WriteLine(configuration.GetConnectionString("Storage"));
        }
    }
}
