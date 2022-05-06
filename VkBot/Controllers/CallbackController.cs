using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using VkNet.Model.GroupUpdate;
using VkBot;
using System.Linq;
using VkNet;
using VkNet.Model.Keyboard;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using VkNet.Enums.SafetyEnums;
using System.Net;
using System.Text;

namespace Cookie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {

        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private static IConfiguration _configuration;

        public static IVkApi _vkApi { get; set; }

        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {



            // Проверяем, что находится в поле "type" 
            switch (updates.Type)
            {
                // Если это уведомление для подтверждения адреса
                case "confirmation":
                    // Отправляем строку для подтверждения 
                    return Ok(_configuration["Config:Confirmation"]);
                case "message_new":
                    //Spredsheet.ReadEntriesMas2();
                    // Десериализация
                    //SendMessage("aaa", 266006795);
                    using (var db = new MyContext())
                    {
                        var numuser = db.Users.Count();
                        if (numuser == 0)
                        {
                            Spredsheet.ReadEntriesMas();
                            Spredsheet.ReadEntriesMasGames();
                            Spredsheet.ReadEntriesMasBettings();

                        }
                    }
                    //Methods.MessageAboutEndGame(Program.admins);
                    var msg = Message.FromJson(new VkResponse(updates.Object));

                    //var tmp = DateTime.Now.Hour;
                    //if (tmp >= 0 && tmp <= 3)
                    //{
                    //    var a = _vkApi.Messages.GetHistory(new MessagesGetHistoryParams { Count = 1, PeerId = 138153146 }).Messages.ToList();
                    //    if (a[0].Date.Value.Date.AddDays(1) == DateTime.Now.Date)
                    //        SendMessage("Здравствуйте, Анастасия Михайловна!\n" +
                    //        "Просим вас сегодня постараться хорошо поспать))" +
                    //        "\nСладких снов)", 138153146);
                    //}
                    //SendMessage(tmp.ToString(), msg.PeerId);
                    Methods.MainMenu(msg);
                    break;
                case "message_event":
                    var msgev = MessageEvent.FromJson(new VkResponse(updates.Object));
                    //SendMessage(msgev.Payload, 266006795);
                    Methods.MessageEventAsync(msgev);
                    break;
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }

        public static long? SendMessage(string message, long? peerId, VkNet.Model.Template.MessageTemplate template)
        {
            Random rnd = new Random();
            return _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                PeerId = peerId,
                Message = message,
                Template = template,
            }) ;
        }
        public static long? SendMessage(string message, long? peerId)
        {
            Random rnd = new Random();
            return _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                PeerId = peerId,
                Message = message,
            });
        }
        public static long? SendMessage(string message, long? peerId, string adressphotos)
        {
            SendMessage("0", peerId);
            // Получить адрес сервера для загрузки.
            var uploadServer1 = _vkApi.Photo.GetAlbumsCount(); //GetUploadServer(283887796);
            SendMessage("01", peerId);
            SendMessage(uploadServer1.ToString(), peerId);
            // Загрузить файл.
            var wc = new WebClient();
            SendMessage("2", peerId);
            //var responseFile = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, adressphotos));
            SendMessage("3", peerId);
            // Сохранить загруженный файл
            var photos = _vkApi.Photo.Save(new PhotoSaveParams
            {
                //SaveFileResponse = responseFile,
                AlbumId = 283887796,
                GroupId = 213110775,
            });
            SendMessage("4", peerId);
            Random rnd = new Random();
            return _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                PeerId = peerId,
                Message = message,
                Attachments = photos,
            });
        }
        public static long? SendMessage(string message, long? peerId, MessageKeyboard keyboard)
        {

            Random rnd = new Random();
            return _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                PeerId = peerId,
                Message = message,
                Keyboard = keyboard
            });
        }

        public static bool EditMessage(string message, long? peerId, long? MessageId)
        {
            Random rnd = new Random();
            return _vkApi.Messages.Edit(new MessageEditParams
            {
                MessageId = MessageId,
                PeerId = (long)peerId,
                Message = message,
                GroupId = 197872639,
            });
        }

        public static bool IsSendMessage(long? peerID)
        {
            return _vkApi.Messages.IsMessagesFromGroupAllowed(197872639, (ulong)peerID);
        }
    }
}