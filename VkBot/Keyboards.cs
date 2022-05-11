using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot
{
    public class Keyboards
    {
        public static MessageKeyboard UserKeyboard // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("Топ игроков", "", KeyboardButtonColor.Primary, "");
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
                    "проверять свои текцщие знания и познавать культуру причастности Макдоналдс.\n\n" +
                    "Главные правила марафона:\n" +
                    "1. Отвечать на вопросы самому. Этот марафон в первую очередь проводится для нашего личностного развития;\n" +
                    "2. По ОКОНЧАНИИ текущего вопроса можно и даже нужно обсуждать его в беседе, делиться своими эмоциями.\n\n" +
                    "Здесь будут считаться баллы за все твои правильные ответы. Посмотреть свой рейтинг ты можешь в меню," +
                    "нажав кнопочку \"Топ участников\".\n" +
                    "Так же по окончании марафона лучшему участнику будет небольшой приз))\n" +
                    "Хочешь принять учатие?! Жми кнопочку ниже)\n\n" +
                    "Удачи!\n\n" +
                    "P.S. Я постоянно развиваюсь и учусь как все сотрудники Макдоналдс. Поэтому, если у тебя" +
                    " появятся предложения по улучшению либо какие-то вопросы пиши [id266006795|Моему создателю].";
                return str;
            }
        }
    }
}
