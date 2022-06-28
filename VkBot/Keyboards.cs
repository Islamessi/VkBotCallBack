using Cookie.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot
{
    public class Keyboards
    {
        public static MessageKeyboard UserSostavBurgers // Клавиатура для обычных пользователей
        {
            get
            {
                var num = (int)Program.UsersInfo[Program.UsersInfo.Count - 1][3];
                List<string> ChastiBurger = Program.Burgers[num].ChastiBurger;
                KeyboardBuilder userKey = new KeyboardBuilder();
                Random rnd = new Random();
                var shuffled = ChastiBurger.OrderBy(_ => rnd.Next()).ToList();
                for (int i=0; i < shuffled.Count-1; i++)
                {
                    for (int j = i+1; j< shuffled.Count; j++)
                    if (shuffled[i] == shuffled[j])
                    {
                        shuffled.RemoveAt(i);
                    }
                }
                for (int i = 0; i < shuffled.Count; i++)
                {
                    if (shuffled[i].Length > 14)
                    {
                        var tmp = shuffled[i];
                        shuffled.RemoveAt(i);
                        shuffled.Add(tmp);
                    }
                }
                for (int i = 0; i < shuffled.Count; i+=2)
                {
                    userKey.AddButton(shuffled[i], "", KeyboardButtonColor.Primary, "");
                    if (shuffled[i].Length > 14 || shuffled[i+1].Length >14)
                    {
                        CallbackController.SendMessage(shuffled[i + 1].Length.ToString(), 266006795);
                        userKey.AddLine();
                    }
                    if (i + 1 < shuffled.Count)
                    {
                        userKey.AddButton(shuffled[i + 1], "", KeyboardButtonColor.Primary, "");
                    }
                    userKey.AddLine();
                }
                userKey.AddButton("Выйти в меню", "", KeyboardButtonColor.Negative, "");
                return userKey.Build();
            }
        }

        public static MessageKeyboard UserBurgers // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                for (int i = 0; i < Program.Burgers.Count; i++)
                {
                    userKey.AddButton(Program.Burgers[i].BurgerName, "", KeyboardButtonColor.Primary, "");
                    userKey.AddLine();
                }
                userKey.AddButton("Выйти в меню", "", KeyboardButtonColor.Negative, "");
                userKey.SetOneTime();
                return userKey.Build();
            }

        }


        public static MessageKeyboard CansellKeyboard // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("1", "", KeyboardButtonColor.Primary, "");
                userKey.AddButton("2", "", KeyboardButtonColor.Primary, "");
                userKey.AddButton("3", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("4", "", KeyboardButtonColor.Primary, "");
                userKey.AddButton("5", "", KeyboardButtonColor.Primary, "");
                userKey.AddButton("6", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("Выйти из теста", "", KeyboardButtonColor.Negative, "");
                return userKey.Build();
            }

        }
        public static MessageKeyboard UserTesty(long? peerID) // Тесты
        {
            using (var db = new MyContext())
            {
                User user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
                KeyboardBuilder userKey = new KeyboardBuilder();
                if (user.IsHimia == true)
                {
                    userKey.AddButton("Тест по химии", "", KeyboardButtonColor.Positive, "");
                }
                else
                {
                    userKey.AddButton("Тест по химии", "", KeyboardButtonColor.Negative, "");
                }
                userKey.SetOneTime();
                return userKey.Build();
            }

        }
        public static MessageKeyboard UserKeyboard // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("Топ игроков", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("Тесты", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("Сборка бургеров", "", KeyboardButtonColor.Primary, "");
                userKey.SetOneTime();
                return userKey.Build();
            }
            
        }
        public static MessageKeyboard AgreeGame // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("Принять участие", "", KeyboardButtonColor.Positive, "");
                userKey.SetOneTime();
                return userKey.Build();
            }
        }
        public static MessageKeyboard VariantAnswer // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("1", "", KeyboardButtonColor.Default, "");
                userKey.AddButton("2", "", KeyboardButtonColor.Default, "");
                userKey.AddLine();
                userKey.AddButton("3", "", KeyboardButtonColor.Default, "");
                userKey.AddButton("4", "", KeyboardButtonColor.Default, "");
                userKey.SetOneTime();
                return userKey.Build();
            }
        }

        public static MessageKeyboard AdminKeyboard
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("Топ игроков", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("Добавить вопрос", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("Тесты", "", KeyboardButtonColor.Primary, "");
                userKey.AddLine();
                userKey.AddButton("Сборка бургеров", "", KeyboardButtonColor.Primary, "");
                userKey.SetOneTime();
                return userKey.Build();
            }
        }
        public static string Privetstvie
        {
            get
            {
                string str = "Добро пожаловать, мой новый друг! Меня зовут Макин. Я рад, что ты решил присоединиться к нашему марафону.\n" +
                    "Тебя ждут каждодневные вопросы, в которых ты будешь узнавать что-то новое для себя, " +
                    "проверять свои текущие знания и познавать культуру причастности Макдоналдс.\n\n" +
                    "Главные правила марафона:\n" +
                    "1. Отвечать на вопросы самому. Этот марафон в первую очередь проводится для нашего личностного развития;\n" +
                    "2. По ОКОНЧАНИИ текущего вопроса можно и даже нужно обсуждать его в беседе, делиться своими эмоциями.\n\n" +
                    "Здесь будут считаться баллы за все твои правильные ответы. Посмотреть свой рейтинг ты можешь в меню," +
                    "нажав кнопочку \"Топ участников\".\n" +
                    "Так же по окончании марафона лучшему участнику будет небольшой приз))\n" +
                    "Хочешь принять учатие?! Жми кнопочку ниже)\n\n" +
                    "Удачи!\n\n" +
                    "P.S. Я постоянно развиваюсь и учусь как все сотрудники Макдоналдс. Поэтому, если у тебя" +
                    " появятся предложения по улучшению, либо какие-то вопросы пиши [id266006795|Моему создателю].";
                return str;
            }
        }
    }
}
