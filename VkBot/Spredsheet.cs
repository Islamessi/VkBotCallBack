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
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Legislators";
        //static string SpreedsheetId = "1ZhbX7wI1a92182vUCjVim-02OcWXP5iz4lNfCGBQ7lk";
        //static string sheet = "Malikat";
        static SheetsService service;
        public static void ReadEntriesMas2()//Ввод данных пользователей из таблицы с данными
        {
            CallbackController.SendMessage("ss", 266006795);
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
            CallbackController.SendMessage("ss", 266006795);
                //var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
                //user.Score += Program.UsersInfo[WriteOrNot][3] * Program.UsersInfo[WriteOrNot][5];
                //db.SaveChanges();
                CallbackController.SendMessage("1/0", 266006795);
                string SpreedsheetId = "1nV16Eu3xerecw1_Gm3tXyTfьQKe_oCOklnTVsx9zO_AA";
                CallbackController.SendMessage("1/1", 266006795);
                string sheet = "LiveBall(Top)";
                CallbackController.SendMessage("1/2", 266006795);
                int i = 0;
                CallbackController.SendMessage("1/3", 266006795);
                var range = $"{sheet}!A:E";
                CallbackController.SendMessage("1", 266006795);
                var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
                CallbackController.SendMessage("2", 266006795);
                var responce = request.Execute();
                CallbackController.SendMessage("3", 266006795);
                var values = responce.Values;
                CallbackController.SendMessage("sss", 266006795);
                foreach (var row in values)
                {
                    CallbackController.SendMessage("ssss", 266006795);
                    Cookie.Controllers.CallbackController.SendMessage(row[0].ToString()+" "+ row[1].ToString()+""+ row[2].ToString() + " " + row[3].ToString()
                        + " "+ row[4].ToString(), 266006795);
                    //a.SetMas(i, 0, row[0].ToString());
                    //a.SetMas(i, 1, row[1].ToString());
                    //a.SetMas(i, 2, row[2].ToString());
                    //a.SetMas(i, 6, row[3].ToString());
                    i++;
                }
                //a.SetNumUsers(i);
            
        }
    }
}
