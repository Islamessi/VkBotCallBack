﻿using System;
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
        public static List<long?> admins = new List<long?> { 266006795, 343444974};//Лист с информацией о админах, которым доступна расширенная клавиатура 
        //public static List<List<string>> PenaltyScore = new List<List<string>>();
        //public static List<List<string>> PenaltyScore2 = new List<List<string>>();
        //public static List<Penalty> PenaltyGames = new List<Penalty>();
        public static Penaltys Penaltys = new Penaltys();
        public static PenaltysWithFriend PenaltysWithFriend = new PenaltysWithFriend();

        public static List<List<string>> Question = new List<List<string>>
        {
            new List<string>{ "Что такое Soft Care Sensiset?\n" +
                "1. Антисептик для рук\n" +
                "2. Крем для рук\n" +
                "3. Дезинфицирующее мыло для рук\n" +
                "4. Средство для мытья полов", "3"},
            new List<string>{"Что такое Soft Care Aquagard?\n" +
                "1. Средство для мытья полов\n" +
                "2. Средство для дезинфекции поверхностей\n" +
                "3. Средство для полировки искусственной кожи\n" +
                "4. Защитный крем для рук", "4" },
            new List<string>{"Что такое McD DR?\n" +
                "1. Антисептик для рук\n" +
                "2. Крем для полировки для искусственной кожи\n" +
                "3. Средство для ручного мытья посуды\n" +
                "4. Средство для мытья и дезинфекции оборудования и поверхностей\n" +
                "5. Средство для мытья полов","4" },
            new List<string>{"Что такое McD HA?\n" +
                "1. Средство для ручного мытья посуды\n" +
                "2. Дезинфицирующий раствор\n" +
                "3. Средство для мытья полов\n" +
                "4. Средство для мытья посуды в посудомоечной машине\n" +
                "5. Средство для мытья стекол и поверхностей", "1"},
            new List<string>{ "Что такое McD FC conc?\n" +
                "1. Средство для мытья полов\n" +
                "2. Средство для чистки стекол и поверхностей\n" +
                "3. Антисептик для рук\n" +
                "4. Чистящий крем\n" +
                "5. Щелочное моющее средство для жесткой воды", "1"},
            new List<string>{ "Что такое McD SK conc?\n" +
                "1. Средство для стекол и зеркальных поверхностей\n" +
                "2. Концентрированное моющее средство для стекол и различных поверхностей\n" +
                "3. Полироль для всех поверхностей\n" +
                "4. Средство для ручного мытья посуды", "2"},
            new List<string>{ "Что такое Suma Tab D4 Tab?\n" +
                "1. Таблетки для мытья кофемашины\n" +
                "2. Чистящий крем\n" +
                "3. Средство дезинфицирующее. Хлорные таблетки\n" +
                "4. Средство для мытья посуды в посудомоечной машине", "3"},
             new List<string>{ "Что такое Suma Cafè Auto Tab?\n" +
                 "1. Хлорные дезинфицирующие таблетки\n" +
                 "2. Таблетки для ежедневной чистки эспрессо-кофемашин Franke\n" +
                 "3. Средство для полировки искусственной кожи\n" +
                 "4. Хлорные таблетки для дезинфекции кофемашин", "2"},
             new List<string>{ "Что такое Suma Cafè MilkClean?\n" +
                 "1. Моющее средство для очистки от молочных разводов\n" +
                 "2. Кислотное моющее средство для промывки молочных систем кофемашин\n" +
                 "3. Щелочное моющее средство для промывки молочных систем кофемашин\n" +
                 "4. Моющее средство для промывки молочных систем кофемашин ", "2"},
             new List<string>{ "Что такое McD SG?\n" +
                 "1. Полироль для всех поверхностей\n" +
                 "2. Полироль для нержавеющей стали \n" +
                 "3. Полироль для искусственной кожи\n" +
                 "4. Полироль для дерева (декора)", "2"},
             new List<string>{ "Как часто необходимо производить  замену раствора Suma Tab D4  и McD DR?\n" +
                 "1. Каждый час\n" +
                 "2. Каждые два часа\n" +
                 "3. Каждые три часа\n" +
                 "4. Каждые четыре часа", "2"},
             new List<string>{ "Что необходимо мыть и дезинфицировать растворами McD DR после каждого использования?\n" +
                 "1. Детские стульчики\n" +
                 "2. Столешницы\n" +
                 "3. Столовая посуда\n" +
                 "4. Щетки для мытья посуды\n" +
                 "5. Пеленальный столик", "125"},
             new List<string>{ "Выберите верные утверждения:\n" +
                 "1. После мытья полов использованную воду сливайте в трехсекционную мойку\n" +
                 "2. Для мытья полов можно использовать холодный раствор McD FС conc\n" +
                 "3. Необходимо всегда ставить знаки «Осторожно, мокрый пол», когда производится влажная уборка\n" +
                 "4. Убедитесь, что моп чистый и предназначен для использования в той зоне, которую вы собираетесь мыть", "34"},
             new List<string>{ "Методы дезинфекции:\n" +
                 "1. Протирание\n" +
                 "2. Замачивание\n" +
                 "3. Погружение\n", "123"},
             new List<string>{"Перед использованием продезинфицированные полотенца из ведра с раствором Suma Tab D4 " +
                 "необходимо тщательно прополоскать под проточной водой на задней мойке.\n" +
                 "1. Верно\n" +
                 "2. Неверно", "1" },
             new List<string>{ "Минимальное время дезинфекции раствором DR:\n" +
                 "1. 10 минут\n" +
                 "2. 15 минут\n" +
                 "3. 5 минут\n" +
                 "4. 20 минут\n", "3"},

        };

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}