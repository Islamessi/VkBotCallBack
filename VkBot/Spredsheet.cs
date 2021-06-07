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

namespace VkBot
{
    public class Spredsheet
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Legislators";
        static string SpreedsheetId = "1ZhbX7wI1a92182vUCjVim-02OcWXP5iz4lNfCGBQ7lk";
        static string sheet = "Malikat";
        static SheetsService service;
        public static void ReadEntriesMas2()//Ввод данных пользователей из таблицы с данными
        {
            using (var db = new MyContext())
            {
                //var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
                //user.Score += Program.UsersInfo[WriteOrNot][3] * Program.UsersInfo[WriteOrNot][5];
                //db.SaveChanges();
                
                SpreedsheetId = "1nV16Eu3xerecw1_Gm3tXyTfьQKe_oCOklnTVsx9zO_AA";
                sheet = "LiveBall(Top)";
                int i = 0;
                var range = $"{sheet}!A:D";
                var request = service.Spreadsheets.Values.Get(SpreedsheetId, range);
                var responce = request.Execute();
                var values = responce.Values;
                foreach (var row in values)
                {
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
}
