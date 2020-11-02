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

        private static IVkApi _vkApi { get; set; }

        public static IVkApi VkApi { get; } = _vkApi;

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
                    // Десериализация
                    var msg = Message.FromJson(new VkResponse(updates.Object));
                    Random rnd = new Random();
                    _vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = rnd.Next(),
                        PeerId = msg.PeerId,
                        Message = "11"
                    });
                    //Methods.MainMenu(msg);
                    SendMessage("aa", msg.PeerId);
                    break;
                case "message_event":
                    var msgev = MessageEvent.FromJson(new VkResponse(updates.Object));
                    Methods.MessageEventAsync(msgev);
                    break;
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }
        public static void SendMessage(string message, long? peerId)
        {
            Random rnd = new Random();
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                PeerId = peerId,
                Message = message
            });
        }
        public static void SendMessage(string message, long? peerId, MessageKeyboard keyboard)
        {

            Random rnd = new Random();
            _vkApi.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                PeerId = peerId,
                Message = message,
                Keyboard = keyboard
            });
        }
    }
}