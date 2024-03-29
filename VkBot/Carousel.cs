﻿using Cookie.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace VkBot
{
    public class Carousel
    {
        public static List<VkNet.Model.Template.Carousel.CarouselElement> carouselElements = new List<VkNet.Model.Template.Carousel.CarouselElement>();

        public static void AddEllement(string Title, string Description, string Payload)
        {
            IEnumerable<MessageKeyboardButton> buttons = new List<MessageKeyboardButton>
            {
                new MessageKeyboardButton
                {
                    Color = KeyboardButtonColor.Positive,
                    Action = new MessageKeyboardButtonAction
                    {
                        Type = KeyboardButtonActionType.Callback, //Тип кнопки клавиатуры
                        Label = "Положить", //Надпись на кнопке
                        Payload = Payload,
                        
                    },
                }
            };
            carouselElements.Add(new VkNet.Model.Template.Carousel.CarouselElement
            {
                Title = Title,
                Description = Description,
                Buttons = buttons,
                //PhotoId = "-213110775_457239020",
            });
        }

        public static List<VkNet.Model.Template.Carousel.CarouselElement> ReturnCarouselElements()
        {
            return carouselElements;
        }

        public static void RemoveCarouselElements()
        {
            carouselElements.Clear();

            //CallbackController.SendMessage(carouselElements.Count.ToString(), 266006795);
            //int count = carouselElements.Count;
            //for (int i = 0; i < count; i++)
            //{
                
            //    carouselElements.RemoveAt(i);
            //    CallbackController.SendMessage(i.ToString(), 266006795);
            //}
            //CallbackController.SendMessage(carouselElements.Count.ToString(), 266006795);
        }
        //var carouselElements = new List<VkNet.Model.Template.Carousel.CarouselElement>
        //    {
        //        new VkNet.Model.Template.Carousel.CarouselElement
        //        {
        //           Title  = "Барса-Реал",
        //           Description = "16:00",
        //           Buttons = buttons
        //        },
        //        new VkNet.Model.Template.Carousel.CarouselElement
        //        {
        //            Title = "ЦСКА-Зенит",
        //            Description = "19:00",
        //            Buttons = buttons,
        //        }
        //    };

    }
}
