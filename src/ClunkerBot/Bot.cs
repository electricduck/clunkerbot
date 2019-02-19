using System;
using System.Linq;
using Telegram.Bot.Args;
using ClunkerBot.Api;
using ClunkerBot.Commands;
using ClunkerBot.Data;
using ClunkerBot.Models;
using ClunkerBot.Models.ReturnModels.MessageReturnModels;
using ClunkerBot.Utilities;

namespace ClunkerBot
{
    class Bot
    {
        public static int awooCount = 1;

        public static void RunCommand(string command, string[] arguments, MessageEventArgs telegramMessageEvent)
        {
            Bot botClass = new Bot();

            UserModel currentTelegramUser = botClass.GetCurrentTelegramUser(telegramMessageEvent);
                
            string joinedArguments = String.Join(" ", arguments);

            Console.WriteLine($"{currentTelegramUser.TelegramName}");
            
            switch(command) {
                // case "addcar":
                // case "addcartogarage":
                //         string addCarToGarageOutput = "";

                //         if(arguments.Length == 11) {
                //             addCarToGarageOutput = Garage.AddCarTo(
                //                 currentTelegramUser,
                //                 arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8], arguments[9], arguments[10]
                //             );
                //         }

                //         MessageApi.SendTextMessage(addCarToGarageOutput, Program.BotClient, telegramMessageEvent);
                //     break;
                case "ask":
                case "wa":
                        string askOutput = "";

                        if(joinedArguments.Length > 0)
                        {
                            askOutput = WolframAlpha.Ask(joinedArguments);
                        }
                        else
                        {
                            MessageApi.SendTextMessage(HelpData.GetHelp("ask"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(askOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "awoo":
                        string awooOutput;

                        if(AppSettings.Config_Awoo_Repeat) {
                            awooOutput = String.Concat(Enumerable.Repeat(AppSettings.Config_Awoo_Word, awooCount));
                            awooCount++;
                        } else {
                            awooOutput = AppSettings.Config_Awoo_Word;
                        }

                        MessageApi.SendTextMessage(awooOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "brexit":
                        string brexitOutput = Brexit.TimeUntil();
                        MessageApi.SendTextMessage(brexitOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "calculate0to60":
                case "0to60":
                        string calculate0To60Output = "";

                        if(arguments.Length == 4) {
                            calculate0To60Output = ZeroToSixty.Calculate(arguments[0], arguments[1], arguments[2], arguments[3]);
                        } else if(arguments.Length == 5) {
                            calculate0To60Output = ZeroToSixty.Calculate(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
                        } else if(arguments.Length == 6) {
                            calculate0To60Output = ZeroToSixty.Calculate(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5]);
                        } else if(arguments.Length == 7) {
                            calculate0To60Output = ZeroToSixty.Calculate(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("calculate0to60"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(calculate0To60Output, Program.BotClient, telegramMessageEvent);
                    break;
                case "calculatejourneyprice":
                case "journeyprice":
                case "tripprice":
                        string calculateJourneyPriceOutput = "";
                
                        if (arguments.Length == 3) {
                            calculateJourneyPriceOutput = JourneyPrice.Calculate(arguments[0], arguments[1], arguments[2]);
                        }
                
                        MessageApi.SendTextMessage(calculateJourneyPriceOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "findavailableplate":
                case "findplate":
                        string findAvailablePlateOutput = "";

                        if(arguments.Length == 1) {
                            findAvailablePlateOutput = AvailablePlate.Find(arguments[0], "gb");
                        } else if(arguments.Length == 2) {
                            findAvailablePlateOutput = AvailablePlate.Find(arguments[0], arguments[1]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("findavailableplate"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(findAvailablePlateOutput, Program.BotClient, telegramMessageEvent);
                    break;
                // case "getcar":
                // case "getcarfromgarage":
                //         ImageMessageReturnModel getCarFromGarageOutput = null;

                //         if(arguments.Length == 1) {
                //             getCarFromGarageOutput = Garage.GetCarFrom(arguments[0]);
                //         }

                //         if(String.IsNullOrEmpty(getCarFromGarageOutput.PhotoUrl)) {
                //             MessageApi.SendTextMessage(getCarFromGarageOutput.Caption, Program.BotClient, telegramMessageEvent);
                //         } else {
                //             MessageApi.SendPhotoMessage(getCarFromGarageOutput, Program.BotClient, telegramMessageEvent);
                //         }
                //     break;
                case "getfuelly":
                case "fuelly":
                        ImageMessageReturnModel getFuellyOutput = null;

                        if (arguments.Length == 1) {
                            getFuellyOutput = Fuelly.Get(arguments[0]);
                        } else if (arguments.Length == 2) {
                            getFuellyOutput = Fuelly.Get(arguments[0], arguments[1]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("getfuelly"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendPhotoMessage(getFuellyOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "getobdcode":
                case "getobd":
                case "obd":
                case "getobd2code":
                case "getobd2":
                case "obd2":
                        string getObdCodeOutput = "";

                        if(arguments.Length == 1) {
                            getObdCodeOutput = OBDCode.Get(arguments[0]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("getobdcode"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(getObdCodeOutput, Program.BotClient, telegramMessageEvent);
                    break;
                // case "getgarage":
                //         string getGarageOutput = "";

                //         if(arguments.Length == 1) {
                //             getGarageOutput = Garage.Get(arguments[0]);
                //         } else {
                //             MessageApi.SendTextMessage(HelpData.GetHelp("getgarage"), Program.BotClient, telegramMessageEvent);
                //         }

                //         MessageApi.SendTextMessage(getGarageOutput, Program.BotClient, telegramMessageEvent);
                //     break;
                case "getweather":
                case "weather":
                    string getWeatherOutput = "";

                    if(arguments.Length > 0) {
                        string getWeather_fullDetailsTrigger = " full";

                        var getWeather_RequstedLocation = joinedArguments
                            .Replace(getWeather_fullDetailsTrigger, String.Empty);

                        // TODO: Add some way of forcing C/F/K only?
                        if(joinedArguments.Contains(getWeather_fullDetailsTrigger)) {
                            getWeatherOutput = Weather.Get(getWeather_RequstedLocation, false);
                        } else {
                            getWeatherOutput = Weather.Get(getWeather_RequstedLocation, true);
                        }
                    } else {
                        MessageApi.SendTextMessage(HelpData.GetHelp("getweather"), Program.BotClient, telegramMessageEvent);
                    }

                    MessageApi.SendTextMessage(getWeatherOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "guessmileage":
                case "mileage":
                        string guessMileageOutput = "";

                        if (arguments.Length == 3) {
                           guessMileageOutput = Mileage.Guess(arguments[0], Convert.ToInt32(arguments[1]), arguments[2]);
                        } else if (arguments.Length == 4)  {
                           guessMileageOutput = Mileage.Guess(arguments[0], Convert.ToInt32(arguments[1]), arguments[2], arguments[3]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("guessmileage"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(guessMileageOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "help":
                        string helpOutput = "";

                        if(arguments.Length == 0) {
                            helpOutput = Help.Get();
                        } else if(arguments.Length == 1) {
                            helpOutput = Help.Get(arguments[0]);
                        }

                        MessageApi.SendTextMessage(helpOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "info":
                        string infoOutput = "";
                        infoOutput = Info.Get();
                        MessageApi.SendTextMessage(infoOutput, Program.BotClient, telegramMessageEvent);
                    break;
                case "parseplate":
                case "plate":
                        string parsePlateOutput = "";

                        if(arguments.Length == 1) {
                            parsePlateOutput = Plate.Parse(arguments[0]);
                        } else if (arguments.Length == 2 || arguments.Length == 6) {
                            parsePlateOutput = Plate.Parse(arguments[0], arguments[1]);
                        } else {
                            MessageApi.SendTextMessage(HelpData.GetHelp("parseplate"), Program.BotClient, telegramMessageEvent);
                        }

                        MessageApi.SendTextMessage(parsePlateOutput, Program.BotClient, telegramMessageEvent);

                    break;
                // case "setcarphoto":
                //         string setCarPhotoOutput = "";

                //         if(arguments.Length == 2) {
                //             setCarPhotoOutput = Garage.SetCarPhoto(arguments[0], arguments[1]);
                //         }

                //         MessageApi.SendTextMessage(setCarPhotoOutput, Program.BotClient, telegramMessageEvent);
                //     break;
                default:
                    ConsoleOutputUtilities.WarnConsoleMessage($@"Command '{command}' doesn't exist");
                    break;
            }
        }

        public UserModel GetCurrentTelegramUser(MessageEventArgs telegramMessageEvent)
        {
            UserModel user = new UserModel
            {
                TelegramId = telegramMessageEvent.Message.From.Id,
                TelegramUsername = telegramMessageEvent.Message.From.Username,
                TelegramName = telegramMessageEvent.Message.From.FirstName + " " + telegramMessageEvent.Message.From.LastName
            };

            return user;
        }
    }
}