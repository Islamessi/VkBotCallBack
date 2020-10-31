using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;
using VkNet.Model.GroupUpdate;
using VkBot;

namespace Cookie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>
        private readonly IConfiguration _configuration;

        private readonly IVkApi _vkApi;

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
                    using (MyContext db = new MyContext())
                    {
                        Game game = new Game { Team1 = "111" };
                        db.Add(game);
                        db.SaveChanges();
                    }

                    // Отправим в ответ полученный от пользователя текст
                    _vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime().Millisecond,
                        PeerId = msg.PeerId.Value,
                        Message = msg.Text
                    });
                    break;
                case "message_event":
                    var msgev = MessageEvent.FromJson(new VkResponse(updates.Object));
                    _vkApi.Messages.SendMessageEventAnswer($"{msgev.EventId}", (long)msgev.UserId, (long)msgev.PeerId);
                    break;
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }
    }
}