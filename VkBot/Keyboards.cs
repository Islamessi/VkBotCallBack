using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot
{
    public class Keyboards
    {
        public static MessageKeyboard YesOrNo
        {
            get
            {
                KeyboardBuilder adminKey = new KeyboardBuilder();
                adminKey.AddButton("Принять", "", KeyboardButtonColor.Positive, "");
                adminKey.AddLine();
                adminKey.AddButton("Отказаться", "", KeyboardButtonColor.Negative, "");
                return adminKey.Build();
            }
        }

        public static IEnumerable<MessageKeyboardButton> ButtonsInCarousel(string payload)
        {
            IEnumerable<MessageKeyboardButton> buttons = new List<MessageKeyboardButton>
            {
                new MessageKeyboardButton
                {
                    Color = KeyboardButtonColor.Positive,
                    Action = new MessageKeyboardButtonAction
                    {
                        Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                        Label = "Поставить", //Надпись на кнопке
                        Payload = payload
                    },
                }
            };
            return buttons;
        }

        public static MessageKeyboard AdminKeyboard //Клавиатура для админов, заданных в листе admins
        {
            get
            {
                KeyboardBuilder adminKey = new KeyboardBuilder() ;
                adminKey.AddButton("ЕВРО-2020", "", KeyboardButtonColor.Positive, "");
                adminKey.AddLine();
                adminKey.AddButton("Добавить матч", "", KeyboardButtonColor.Primary, "");
                adminKey.AddButton("Удалить матч", "", KeyboardButtonColor.Negative, "");
                adminKey.AddLine();
                adminKey.AddButton("Добавить ссылки", "", KeyboardButtonColor.Primary, "");
                adminKey.AddLine();
                adminKey.AddButton("Ввести результат матча", "", KeyboardButtonColor.Primary, "");
                adminKey.AddLine();
                //adminKey.AddButton("Ввести результат матча вчера", "", KeyboardButtonColor.Primary, "");
                //adminKey.AddLine();
                adminKey.AddButton("Все матчи сегодня", "", KeyboardButtonColor.Default, "");
                adminKey.AddLine();
                adminKey.AddButton("Игра \"Пенальти\"", "", KeyboardButtonColor.Positive, "");
                adminKey.AddButton("Пенальти с другом", "", KeyboardButtonColor.Positive, "");
                adminKey.AddLine();
                adminKey.AddButton("Игра \"Прогнозы\"", "", KeyboardButtonColor.Positive, "");
                adminKey.AddButton("Все ставки сегодня", "", KeyboardButtonColor.Default, "");
                adminKey.AddLine();
                adminKey.AddButton("Топ игроков", "", KeyboardButtonColor.Primary, "");
                return adminKey.Build();
            }
        }

        public static MessageKeyboard UserKeyboard // Клавиатура для обычных пользователей
        {
            get
            {
                KeyboardBuilder userKey = new KeyboardBuilder();
                userKey.AddButton("Все матчи сегодня", "", KeyboardButtonColor.Default, "");
                userKey.AddLine();
                userKey.AddButton("Игра \"Пенальти\"", "", KeyboardButtonColor.Positive, "");
                userKey.AddButton("Пенальти с другом", "", KeyboardButtonColor.Positive, "");
                userKey.AddLine();
                userKey.AddButton("Игра \"Прогнозы\"", "", KeyboardButtonColor.Positive, "");
                userKey.AddButton("Все ставки сегодня", "", KeyboardButtonColor.Default, "");
                userKey.AddLine();
                userKey.AddButton("Топ игроков", "", KeyboardButtonColor.Primary, "");
                userKey.SetOneTime();
                return userKey.Build();
                
            }
        }

        public static MessageKeyboard CanselKeyboard // Клавиатура для отмены
        {
            get
            {
                KeyboardBuilder Cans = new KeyboardBuilder();
                Cans.AddButton("Отмена", "", KeyboardButtonColor.Negative, "");
                Cans.SetOneTime();
                return Cans.Build();
            }
        }

        public static MessageKeyboard PenaltyKeyboard
        {
            get
            {
                var buttons = new List<List<MessageKeyboardButton>>
                {
                    new List<MessageKeyboardButton>
                    {
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "1", //Надпись на кнопке
                                Payload = "1"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "2", //Надпись на кнопке
                                Payload = "2",
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "3", //Надпись на кнопке
                                Payload = "3"
                            },
                        },
                    },
                    new List<MessageKeyboardButton>
                    {
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "4", //Надпись на кнопке
                                Payload = "4"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "5", //Надпись на кнопке
                                Payload = "5"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "6", //Надпись на кнопке
                                Payload = "6"
                            },
                        },
                    },
                    new List<MessageKeyboardButton>
                    {
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "7", //Надпись на кнопке
                                Payload = "7"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "8", //Надпись на кнопке
                                Payload = "8"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "9", //Надпись на кнопке
                                Payload = "9"
                            },
                        },
                    },
                };
                var keyboard = new MessageKeyboard
                {
                    Buttons = buttons,
                    OneTime = false,
                    //Inline = true,
                };
                return keyboard;
                //KeyboardBuilder pen = new KeyboardBuilder();
                //pen.AddButton("1", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("2", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("3", "", KeyboardButtonColor.Default, "");
                //pen.AddLine();
                //pen.AddButton("4", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("5", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("6", "", KeyboardButtonColor.Default, "");
                //pen.AddLine();
                //pen.AddButton("7", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("8", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("9", "", KeyboardButtonColor.Default, "");
                //return pen.Build();
            }
        }


        public static MessageKeyboard LevelKeyboard
        {
            get
            {
                var buttons = new List<List<MessageKeyboardButton>>
                {
                    new List<MessageKeyboardButton>
                    {
                        new MessageKeyboardButton
                        {
                            Color = KeyboardButtonColor.Positive,
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "1", //Надпись на кнопке
                                Payload = "1"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "2", //Надпись на кнопке
                                Payload = "2"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "3", //Надпись на кнопке
                                Payload = "3"
                            },
                        },
                    },
                    new List<MessageKeyboardButton>
                    {
                        new MessageKeyboardButton
                        {
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "4", //Надпись на кнопке
                                Payload = "4"
                            },
                        },
                        new MessageKeyboardButton
                        {
                            Color = KeyboardButtonColor.Negative,
                            Action = new MessageKeyboardButtonAction
                            {
                                Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                                Label = "5", //Надпись на кнопке
                                Payload = "5"
                            },
                        },
                    },
                };
                var keyboard = new MessageKeyboard
                {
                    Buttons = buttons,
                    OneTime = false
                };
                return keyboard;

                //KeyboardBuilder pen = new KeyboardBuilder();
                //pen.AddButton("1", "", KeyboardButtonColor.Positive, "");
                //pen.AddButton("2", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("3", "", KeyboardButtonColor.Default, "");
                //pen.AddLine();
                //pen.AddButton("4", "", KeyboardButtonColor.Default, "");
                //pen.AddButton("5", "", KeyboardButtonColor.Negative, "");
                //return pen.Build();
            }
        }
    }
}
