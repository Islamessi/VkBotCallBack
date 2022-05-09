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
    }
}
