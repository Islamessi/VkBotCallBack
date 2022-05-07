using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cookie.Controllers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VkBot
{
    public class Program
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "Legislators";
        static string SpreedsheetId = "1ZhbX7wI1a92182vUCjVim-02OcWXP5iz4lNfCGBQ7lk";
        static string sheet = "Malikat";
        static SheetsService service;

        public static List<List<long?>> UsersInfo = new List<List<long?>>();//Лист с информацией о том хотят ли добавить или удалить матч 
        public static List<long?> admins = new List<long?> { 266006795};//Лист с информацией о админах, которым доступна расширенная клавиатура 
        //public static List<List<string>> PenaltyScore = new List<List<string>>();
        //public static List<List<string>> PenaltyScore2 = new List<List<string>>();
        //public static List<Penalty> PenaltyGames = new List<Penalty>();
        public static Penaltys Penaltys = new Penaltys();
        public static PenaltysWithFriend PenaltysWithFriend = new PenaltysWithFriend();

        

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}