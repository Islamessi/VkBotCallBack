using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using System.Threading;
using VkNet.Model.Keyboard;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.IO;
using Cookie.Controllers;

namespace VkBot
{
    public class Spredsheet
    {
        public static string sheet = "Users(LiveBall)";
        static readonly string SpreedsheetId = "1nV16Eu3xerecw1_Gm3tXyTfQKe_oCOklnTVsx9zO_AA";
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Legislators";
        //static string SpreedsheetId = "1ZhbX7wI1a92182vUCjVim-02OcWXP5iz4lNfCGBQ7lk";
        //static string sheet = "Malikat";
        static SheetsService service;
        //Сохранение, считывание и обновление данных в таблице Users(LiveBall)
        public static void ReadEntriesMas()//Ввод данных пользователей из таблицы с данными
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            int i = 0;
            var range = $"{sheet}!A:E";
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            using (var db = new MyContext())
            {
                foreach (var row in values)
                {

                    User user = new User
                    {
                        Id = Convert.ToInt32(row[0]),
                        VkId = Convert.ToInt32(row[1]),
                        Score = Convert.ToInt32(row[2]),
                        FirstName = row[3].ToString(),
                        LastName = row[4].ToString()
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    //CallbackController.SendMessage(row[0].ToString() + " " + row[1].ToString() + "" + row[2].ToString() + " " + row[3].ToString()
                    //    + " " + row[4].ToString(), 266006795);
                    //a.SetMas(i, 0, row[0].ToString());
                    //a.SetMas(i, 1, row[1].ToString());
                    //a.SetMas(i, 2, row[2].ToString());
                    //a.SetMas(i, 6, row[3].ToString());
                    i++;
                }
            }
            //a.SetNumUsers(i);

        }
        public static void UpdateEntry(User user)
        {
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            
            string strochka;
            strochka = (user.Id).ToString();
            var range = $"{sheet}!";
            range += (char)(65 + 2) + strochka + ":" + (char)(65 + 2) + strochka;
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            var valueRange = new ValueRange();
            var objectList = new List<object>() { user.Score.ToString() };

            valueRange.Values = new List<IList<object>> { objectList };
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreedsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }
        public static void CreateEntry(MyContext user, User user1)
        {

            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            string strochka;
            strochka = (user.Users.Count()).ToString();
            var range = $"{sheet}!";
            range += (char)(65) + strochka + ":" + (char)(65 + 5 ) + strochka;
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            var valueRange = new ValueRange();
            var objectList = new List<object>() {user1.Id ,user1.VkId, user1.Score, user1.FirstName, user1.LastName };

            valueRange.Values = new List<IList<object>> { objectList };
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreedsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }

        //Сохранение, считывание и обновление данных в таблице Bettings
        
        public static void ReadEntriesMasBettings()//Ввод данных пользователей из таблицы с данными
        {
            sheet = "Bettings";
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            int i = 0;
            var range = $"{sheet}!A:E";
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            using (var db = new MyContext())
            {
                CallbackController.SendMessage(values.Count().ToString(), 266006795);
                foreach (var row in values)
                {
                    Betting betting = new Betting
                    {
                        VkId = Convert.ToInt32(row[1]),
                        DateBetting = Convert.ToDateTime(row[2]),
                        ScoreGame = row[3].ToString(),
                        GameId = Convert.ToInt32(row[4]),
                    };
                    CallbackController.SendMessage(betting.GameId.ToString()+ "aaaa1", 266006795);
                    db.Bettings.Add(betting);
                    CallbackController.SendMessage(betting.Id.ToString() + "aaaa", 266006795);
                    CallbackController.SendMessage(db.Bettings.Count().ToString(), 266006795);
                    db.SaveChanges();
                    i++;
                }
            }
            //a.SetNumUsers(i);

        }
        public static void CreateEntryBettings(MyContext user, Betting betting)
        {
            sheet = "Bettings";
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            string strochka;
            strochka = (user.Bettings.Count()).ToString();
            var range = $"{sheet}!";
            range += (char)(65) + strochka + ":" + (char)(65 + 5) + strochka;
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            var valueRange = new ValueRange();
            var objectList = new List<object>() { betting.Id, betting.VkId, betting.DateBetting, betting.ScoreGame, betting.GameId };
            valueRange.Values = new List<IList<object>> { objectList };
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreedsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }


        //Сохранение, считывание и обновление данных в таблице Games

        public static void ReadEntriesMasGames()//Ввод данных пользователей из таблицы с данными
        {
            sheet = "Games";
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            int i = 0;
            var range = $"{sheet}!A:F";
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            using (var db = new MyContext())
            {
                foreach (var row in values)
                {
                    Game game = new Game
                    {
                        //Id = Convert.ToInt32(row[0]),
                        Team1 = row[1].ToString(),
                        Team2 = row[2].ToString(),
                        Links = row[3].ToString(),
                        DateGame = Convert.ToDateTime(row[4]),
                        Completed = Convert.ToBoolean(row[5])
                    };
                    db.Games.Add(game);
                    db.SaveChanges();
                    i++;
                }
            }
            //a.SetNumUsers(i);

        }
        public static void CreateEntryGames(MyContext user, Game game)
        {
            sheet = "Games";
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            string strochka;
            strochka = (user.Games.Count()).ToString();
            var range = $"{sheet}!";
            range += (char)(65) + strochka + ":" + (char)(65 + 6) + strochka;
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            var valueRange = new ValueRange();
            var objectList = new List<object>() { game.Id, game.Team1, game.Team2, game.Links, game.DateGame, game.Completed };
            valueRange.Values = new List<IList<object>> { objectList };
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreedsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }

        public static void UpdateEntryGames(MyContext user)
        {
            sheet = "Games";
            GoogleCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            string strochka;
            strochka = (user.Games.Count()).ToString();
            var range = $"{sheet}!";
            range += (char)(65+5) + strochka + ":" + (char)(65 + 5) + strochka;
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            var valueRange = new ValueRange();
            var objectList = new List<object>() { true };
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreedsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }
    }
}
