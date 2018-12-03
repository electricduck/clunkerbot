using System;
using System.Collections.Generic;
using System.Linq;
using CarPupsTelegramBot.Models;
using CarPupsTelegramBot.Utilities;

namespace CarPupsTelegramBot.Data
{
    class GarageData
    {
        public void AddUpdateCarPhoto(GarageModel garageItem)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    int dbCount = 0;

                    var result = db.Garage.SingleOrDefault(g => g.Plate == garageItem.Plate);
                    
                    if (result != null)
                    {
                        result.MainImage = garageItem.MainImage;
                        
                        dbCount = db.SaveChanges();
                    } else {
                        db.Garage.Add(garageItem);
                        dbCount = db.SaveChanges();
                    }

                    ConsoleOutputUtilities.DatabaseSaveConsoleMessage(dbCount);
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
            }
        }

        public void AddUpdateGarageItem(GarageModel garageItem)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    int dbCount = 0;

                    var result = db.Garage.SingleOrDefault(g => g.Plate == garageItem.Plate);
                    
                    if (result != null)
                    {
                        result.DriveType = garageItem.DriveType;
                        result.EngineCode = garageItem.EngineCode;
                        result.EngineFuel = garageItem.EngineFuel;
                        result.EngineSize = garageItem.EngineSize;
                        result.EngineType = garageItem.EngineType;
                        result.FuellyId = garageItem.FuellyId;
                        result.Generation = garageItem.Generation;
                        result.MainImage = garageItem.MainImage;
                        result.Make = garageItem.Make;
                        result.Model = garageItem.Model;
                        result.Plate = garageItem.Plate;
                        result.TelegramUserId = garageItem.TelegramUserId;
                        result.Transmission = garageItem.Transmission;
                        result.Trim = garageItem.Trim;
                        
                        dbCount = db.SaveChanges();
                    } else {
                        db.Garage.Add(garageItem);
                        dbCount = db.SaveChanges();
                    }

                    ConsoleOutputUtilities.DatabaseSaveConsoleMessage(dbCount);
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
            }
        }

        public List<GarageModel> GetCarsForUser(long TelegramId)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    var results = db.Garage
                        .Where(g => g.TelegramUserId == TelegramId)
                        .ToList();

                    return results;
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
            }
        }

        public GarageModel GetCarFromGarageById(int id)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    var result = db.Garage.SingleOrDefault(g => g.Id == id);

                    return result;
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
            }
        }

        public GarageModel GetCarFromGarageByPlate(string plate)
        {
            try {
                using (var db = new CarPupsTelegramBotContext())
                {
                    var result = db.Garage.SingleOrDefault(g => g.Plate == plate);

                    return result;
                }
            } catch (Exception e) {
                ConsoleOutputUtilities.ErrorConsoleMessage(e.ToString());
                return null;
            }
        }
    }
}