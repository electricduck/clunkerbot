using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using ClunkerBot.Api;
using ClunkerBot.Commands;
using ClunkerBot.Data;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels.MessageReturnModels;
using ClunkerBot.Utilities;

namespace ClunkerBot
{
    class Program
    {
        public static ITelegramBotClient BotClient;

        static void Main()
        {
            ConsoleOutputUtilities.ResetConsoleColor();

            StartupMessageUtlities.GenerateStartupMessage();

            ConsoleOutputUtilities.InfoConsoleMessage($"Started at {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss zzz")}");

            ConsoleOutputUtilities.DoingConsoleMessage("Setting up application");
            try {
                SetupApp();
            } catch {
                ConsoleOutputUtilities.ErrorConsoleMessage("Configuration file missing");
                System.Environment.Exit(1);
            }

            ConsoleOutputUtilities.DoingConsoleMessage("Building help dictionary");
            HelpData.CompileHelpDictionary();

            ConsoleOutputUtilities.DoingConsoleMessage("Connecting to Telegram");
            BotClient = new TelegramBotClient(AppSettings.ApiKeys_Telegram);
            

            bool isApiKeyValid = BotClient.TestApiAsync().Result;

            if(!isApiKeyValid) {
                ConsoleOutputUtilities.ErrorConsoleMessage("Connection to Telegram unsuccessful: Invalid API key");
                System.Environment.Exit(1);
            } else {
                ConsoleOutputUtilities.OkayConsoleMessage("Connection to Telegram successful");
            }

            string botId = BotClient.BotId.ToString();
            ConsoleOutputUtilities.InfoConsoleMessage($"ID of bot is {botId}");

            BotClient.OnMessage += Bot_OnMessage;
            //BotClient.OnMessageEdited += Bot_OnMessageEdited;

            BotClient.StartReceiving();
            ConsoleOutputUtilities.OkayConsoleMessage("Listening for triggers");
            ConsoleOutputUtilities.SeparatorConsoleMessage();

            Thread.Sleep(int.MaxValue);
        }

        static void Bot_OnMessage(object sender, MessageEventArgs e) {
            if(e.Message.Text != null) {
                try {
                    var messageText = e.Message.Text.ToString();

                    string command = null;
                    string[] arguments = null;

                    if(messageText.StartsWith("/"))
                    {
                        command = messageText.Split(" ")[0].Replace("/", "").Replace(AppSettings.Config_BotUsername, "").ToLower();
                        ConsoleOutputUtilities.MessageInConsoleMessage(e);

                        if(StringUtilities.CountWords(messageText) == 1) {
                            if(HelpData.HelpDictionary.ContainsKey(command)) {
                                string helpOutput = HelpData.GetHelp(command);
                                MessageApi.SendTextMessage(helpOutput, BotClient, e);
                            } else {
                                arguments = (messageText.Substring(messageText.IndexOf(' ') + 1)).Split(" ");
                                Bot.RunCommand(command, arguments, e);
                            }
                        } else {
                            arguments = (messageText.Substring(messageText.IndexOf(' ') + 1)).Split(" ");
                            Bot.RunCommand(command, arguments, e);
                        }
                    }
                } catch (Exception exception) {
                    ConsoleOutputUtilities.ErrorConsoleMessage(exception.ToString());
                }
            }
        }

        //static void Bot_OnMessageEdited(object sender, MessageEventArgs e) {
        //    string output = CommandsBase.BuildSoftErrorOutput("Message edits are unsupported. Please type the command again into a new message.");
        //    MessageApi.SendTextMessage(output, BotClient, e);
        //}

        public static void SetupApp() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            AppSettings.ApiKeys_OpenWeatherMap = configuration.GetSection("apiKeys")["openWeatherMap"];
            AppSettings.ApiKeys_Telegram = configuration.GetSection("apiKeys")["telegram"];
            AppSettings.ApiKeys_WolframAlpha = configuration.GetSection("apiKeys")["wolframAlpha"];
            AppSettings.Config_Awoo_Repeat = bool.Parse(configuration.GetSection("config").GetSection("awoo")["repeat"]);
            AppSettings.Config_Awoo_Word = configuration.GetSection("config").GetSection("awoo")["word"];
            AppSettings.Config_BotUsername = configuration.GetSection("config")["botUsername"];
            AppSettings.Endpoints_PlateWtf = configuration.GetSection("endpoints")["plate.wtf"];
        }
    }
}
