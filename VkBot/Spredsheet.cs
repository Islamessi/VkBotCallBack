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
        static string sheet = "Users(LiveBall)";
        static readonly string SpreedsheetId = "1nV16Eu3xerecw1_Gm3tXyTfQKe_oCOklnTVsx9zO_AA";
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Legislators";
        //static string SpreedsheetId = "1ZhbX7wI1a92182vUCjVim-02OcWXP5iz4lNfCGBQ7lk";
        //static string sheet = "Malikat";
        static SheetsService service;
        //Сохранение, считывание и обновление данных в таблице Users(LiveBall)
        public static void CreateEntry(MyContext user, User user1)
        {
            sheet = "Users(Mcd)";
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
            range += (char)(65) + strochka + ":" + (char)(65 + 5) + strochka;
            var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
            var responce = request.Execute();
            var values = responce.Values;
            var valueRange = new ValueRange();
            var objectList = new List<object>() { user1.Id, user1.VkId, user1.Score, user1.Name,user1.NumSurv };

            valueRange.Values = new List<IList<object>> { objectList };
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreedsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }
    }
}
