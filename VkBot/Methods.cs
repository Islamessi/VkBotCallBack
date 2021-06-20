using Cookie.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;

namespace VkBot
{

    public static class Helper
    {
        public static Game CreateGame(this Game game, string Team1, string Team2, DateTime DateGame, string Links)
        {
            game.Team1 = Team1;
            game.Team2 = Team2;
            game.DateGame = DateGame;
            game.Links = Links;
            return game;
        }
    }

    public class Methods
    {
        public async static void MessageEventAsync(MessageEvent msgev)
        {
            await Task.Run(() =>
            {
                char[] vsp = ":();'!, .?".ToCharArray();
                string userMessage = msgev.Payload;
                long? peerID = msgev.PeerId;
                int WriteOrNot = -1;
                int vsp2 = 0;
                foreach (var us in Program.UsersInfo)
                {
                    if (us[0] == peerID)
                    {
                        WriteOrNot = vsp2;
                        break;
                    }
                    vsp2++;
                }
                if (WriteOrNot != -1)
                {
                    switch (Program.UsersInfo[WriteOrNot][1])
                    {
                        case 9://игра пенальти
                            try
                            {
                                var bbb = Convert.ToInt32(userMessage);
                                if (Program.Penaltys[peerID].ImpactNumber % 2 == 0)
                                    PenaltyGameGoolKiper(WriteOrNot, userMessage, peerID);
                                else if (Program.Penaltys[peerID].ImpactNumber % 2 == 1)
                                    PenaltyGameForward(WriteOrNot, userMessage, peerID);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Выберите число от 1 до 9!", peerID);
                            }
                            break;
                        case 10://игра пенальти
                            try
                            {
                                int vsp3 = Convert.ToInt32(userMessage);
                                Program.UsersInfo[WriteOrNot][1] = 9;
                                Program.UsersInfo[WriteOrNot].Add(vsp3);
                                Program.Penaltys[peerID].Level = vsp3;
                                //Program.PenaltyGames
                                Program.Penaltys[peerID].MessageId = CallbackController.SendMessage("Уровень выбран, начинайте игру)", peerID, Keyboards.PenaltyKeyboard);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Выберите уровень от 1 до 5 на клавиатуре!", peerID);
                            }
                            break;
                        case 12:
                            try
                            {
                                var bbb = Convert.ToInt32(userMessage);
                                if (Program.PenaltysWithFriend[peerID].PeerId1 == peerID && Program.PenaltysWithFriend[peerID].ImpactNumber % 2 == 0)
                                {
                                    CallbackController.SendMessage("1", 266006795);
                                    PenaltyWithFriendGameGoolKiper(WriteOrNot, userMessage, peerID);
                                }
                                else if (Program.PenaltysWithFriend[peerID].PeerId1 == peerID && Program.PenaltysWithFriend[peerID].ImpactNumber % 2 == 1)
                                {
                                    CallbackController.SendMessage("2", 266006795);
                                    PenaltyWithFriendGameForward(WriteOrNot, userMessage, peerID);
                                }
                                else if (Program.PenaltysWithFriend[peerID].PeerId2 == peerID && Program.PenaltysWithFriend[peerID].ImpactNumber % 2 == 0)
                                {
                                    CallbackController.SendMessage("3", 266006795);
                                    PenaltyWithFriendGameForward(WriteOrNot, userMessage, peerID);
                                }
                                else if (Program.PenaltysWithFriend[peerID].PeerId2 == peerID && Program.PenaltysWithFriend[peerID].ImpactNumber % 2 == 1)
                                {
                                    CallbackController.SendMessage("4", 266006795);
                                    PenaltyWithFriendGameGoolKiper(WriteOrNot, userMessage, peerID);
                                }
                            }
                            catch
                            {
                                CallbackController.SendMessage("Выберите число от 1 до 9!", peerID);
                            }
                            break;
                    }
                }
                CallbackController._vkApi.Messages.SendMessageEventAnswer($"{msgev.EventId}", (long)msgev.UserId, (long)msgev.PeerId);
            });
        }

        public static void MainMenu(Message msg)
        {
            char[] vsp = ":();'!, .?".ToCharArray();
            string userMessage = msg.Text.ToLower().Trim(vsp);
            string userMessageUpp = msg.Text;
            long? peerID = msg.PeerId;
            int WriteOrNot = -1;
            int vsp2 = 0;
            foreach (var us in Program.UsersInfo)
            {
                if (us[0] == peerID)
                {
                    WriteOrNot = vsp2;
                    break;
                }
                vsp2++;
            }
            if (WriteOrNot == -1)
            {
                switch (userMessage)
                {
                    case "начать":
                        if (Program.admins.Contains(peerID))
                            CallbackController.SendMessage("Мы то сообщество, которое поможет тебе найти ссылки на матчи и поделать ставки без реальных денег.", peerID, Keyboards.AdminKeyboard);
                        else
                            CallbackController.SendMessage("Мы то сообщество, которое поможет тебе найти ссылки на матчи и поделать ставки без реальных денег.", peerID, Keyboards.UserKeyboard);
                        break;
                    case "добавить матч":
                        if (Program.admins.Contains(peerID))
                        {
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(1);
                            CallbackController.SendMessage("Теперь введите, пожалуйста, данные о матче по формату:\n" +
                                "\"Команда1 Команда2 Дата_игры Ссылки(через пробел)\"", peerID, Keyboards.CanselKeyboard);
                        }
                        break;
                    case "все матчи сегодня":
                        Methods.AllGames(Program.admins, peerID, "Вот все матчи на сегодня:\n\n", false, DateTime.Now.Date);
                        break;
                    case "удалить матч":
                        if (Program.admins.Contains(peerID))
                        {
                            Methods.AllGames(Program.admins, peerID, "Выберите матч, который хотите удалить", true, DateTime.Now.Date);
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(2);
                        }
                        break;
                    case "добавить ссылки":
                        if (Program.admins.Contains(peerID))
                        {
                            Methods.AllGames(Program.admins, peerID, "", true, DateTime.Now.Date);
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(3);
                        }
                        break;
                    case "игра \"прогнозы\"":
                        using (var db = new MyContext())
                        {
                            var user = db.Users.Where(p => p.VkId == peerID);
                            if (user.Count() == 0)
                            {
                                var users = CallbackController._vkApi.Users.Get(new long[] { (long)peerID }).FirstOrDefault();
                                User user1 = new User { VkId = peerID, FirstName = users.FirstName, LastName = users.LastName };
                                db.Users.Add(user1);
                                db.SaveChanges();
                                Spredsheet.CreateEntry(db, user1);
                            }
                        }
                        Methods.AllGames(Program.admins, peerID, "", true, DateTime.Now.Date);
                        Program.UsersInfo.Add(new List<long?> { peerID });
                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(5);
                        break;
                    case "все ставки сегодня":
                        string allMatch = "Вот ваши ставки на сегодня:\n\n";
                        using (var db = new MyContext())
                        {
                            var dat = DateTime.Now.Date;
                            var bettings = db.Bettings.Where(p => p.DateBetting >= dat)
                                .Intersect(db.Bettings.Where(p => p.DateBetting <= dat.AddDays(1)))
                                .Intersect(db.Bettings.Where(p => p.VkId == peerID));
                            foreach (var b in bettings)
                            {
                                var game = db.Games.Where(p => p.Id == b.GameId).FirstOrDefault();
                                allMatch += $"{game.Team1}-{game.Team2} {b.ScoreGame}\n";
                            }
                        }
                        CallbackController.SendMessage(allMatch, peerID);
                        break;
                    case "топ игроков":
                        string vsp3 = "Вот топ 10 игроков 🏆\n";
                        int mesto = 0;
                        using (var db = new MyContext())
                        {
                            var users = db.Users.OrderByDescending(p => p.Score);
                            int jj = 1;
                            foreach (var b in users)
                            {
                                if (b.VkId == peerID) mesto = jj;
                                if (jj >= 11 && mesto != 0) break;
                                if (jj < 11)
                                {
                                    vsp3 += $"{jj}) [id{b.VkId}|{b.FirstName} {b.LastName}] - {b.Score} ⚽\n";
                                }
                                jj++;
                            }
                            if (mesto > 10)
                            {
                                var user = users.Where(p => p.VkId == peerID).FirstOrDefault();
                                vsp3 += $"\nВаш рейтинг:\n" +
                                    $"{mesto}) [id{user.VkId}|{user.FirstName} {user.LastName}] - {user.Score} ⚽";
                            }
                            CallbackController.SendMessage(vsp3, peerID);
                        }
                        break;
                    case "ввести результат матча":
                        if (Program.admins.Contains(peerID))
                        {
                            Methods.AllGames(Program.admins, peerID, "Выберите матч, к которому хотите ввести результат.", true, DateTime.Now.Date);
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(7);
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(1);
                        }
                        break;
                    case "ввести результат матча вчера":
                        if (Program.admins.Contains(peerID))
                        {
                            Methods.AllGames(Program.admins, peerID, "Выберите матч, к которому хотите ввести результат.", true,
                                DateTime.Now.AddDays(-1).Date);
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(7);
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(2);
                        }
                        break;
                    case "игра \"пенальти\"":
                        using (var db = new MyContext())
                        {
                            var user = db.Users.Where(p => p.VkId == peerID);
                            if (user.Count() == 0)
                            {
                                var users = CallbackController._vkApi.Users.Get(new long[] { (long)peerID }).FirstOrDefault();
                                User user1 = new User { VkId = peerID, FirstName = users.FirstName, LastName = users.LastName };
                                db.Users.Add(user1);
                                db.SaveChanges();
                                Spredsheet.CreateEntry(db, user1);
                            }
                        }
                        //Program.PenaltyGames.Add(new Penalty { PeerId = peerID });
                        Penalty _penalty = new Penalty { PeerId = peerID };
                        Program.Penaltys.Add(_penalty);
                        Program.UsersInfo.Add(new List<long?> { peerID });
                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(10);
                        CallbackController.SendMessage("Цель данной игры, забить больше по пенальти и победить)) " +
                            "Вы первым стоите на воротах, перед вами 9 кнопок. Вы выбираете, ту, " +
                            "куда прыгаете. Бот рандомно выбирает куда бить, если вы попали в ту же область, " +
                            "вы ловите мяч. После 5 ударов вы бьете 5 раз, а бот становится на ворота. В случае победы вы получите " +
                            "все голы, которые смогли забить) Удачи!)\n\n Выберите сложность игры. Если вы победите, то кол-во забитых " +
                            "голов умножится на этот коэффициент!", peerID, Keyboards.LevelKeyboard);
                        break;

                    case "пинг":
                        CallbackController.SendMessage("Понг", peerID);
                        break;
                    case "понг":
                        CallbackController.SendMessage("Пошел нахуй...", peerID);
                        break;
                    case "пенальти с другом":
                        Program.UsersInfo.Add(new List<long?> { peerID });
                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(10);
                        CallbackController.SendMessage("Перешлите одно сообщение того человека, с которым вы хотите сыграть в пенальти.", peerID, Keyboards.CanselKeyboard);
                        break;
                    default:
                        if (Program.admins.Contains(peerID))
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        else
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.UserKeyboard);
                        break;
                }
            }
            else
            {
                switch (Program.UsersInfo[WriteOrNot][1])
                {
                    case 1://Добавление матча
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        }
                        else
                        {
                            var Info = userMessageUpp.Split(' ');
                            var oneOrTwo = 3;
                            DateTime date = new DateTime();
                            try
                            {
                                date = Convert.ToDateTime(Info[2] + " " + Info[3]);
                                oneOrTwo = 4;
                            }
                            catch
                            {
                                date = Convert.ToDateTime(Info[2]);
                            }
                            //var vsp3 = Convert.ToDateTime(Info[0]);
                            //var date = Info[2].Split('-');
                            //var date2 = "";
                            //if (date.Length == 2)
                            //    date2 = Convert.ToDateTime(date[0] + " " + date[1]).ToString();
                            //else
                            //    date2 = Convert.ToDateTime(date[0]).ToString();
                            try
                            {
                                using (var db = new MyContext())
                                {
                                    var links = "";
                                    for (int i = oneOrTwo; i < Info.Length; i++)
                                    {
                                        links += Info[i] + " ";
                                    }
                                    Game game = new Game
                                    {
                                        Team1 = Info[0],
                                        Team2 = Info[1],
                                        DateGame = date,
                                        Links = links
                                    };
                                    game.CreateGame(Info[0], Info[1], date, links);
                                    CallbackController.SendMessage(game.DateGame + " " + game.Team1, peerID, Keyboards.AdminKeyboard);
                                    db.Games.Add(game);
                                    db.SaveChanges();
                                    Spredsheet.CreateEntryGames(db, game);

                                }
                                Program.UsersInfo.RemoveAt(WriteOrNot);
                                CallbackController.SendMessage("Этот матч успешно добавлен ✔", peerID, Keyboards.AdminKeyboard);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Введите, пожалуйста, данные о матче по формату:\n" +
                            "\"Команда1 Команда2 Дата_игры Ссылки(через пробел)\"", peerID);
                            }
                        }
                        break;
                    case 2://Удаление матча
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        }
                        else
                        {
                            var Info = userMessageUpp.Split('-');
                            var vsp3 = DateTime.Now.Date;
                            try
                            {
                                using (var db = new MyContext())
                                {
                                    Game game = db.Games.Where(p => p.Team1 == Info[0])
                                        .Intersect(db.Games.Where(p => p.Team2 == Info[1])).
                                        Intersect(db.Games.Where(p => p.DateGame >= vsp3)).
                                        Intersect(db.Games.Where(p => p.DateGame <= vsp3.AddDays(1))).FirstOrDefault();
                                    db.Games.Remove(game);
                                    db.SaveChanges();
                                }
                                Program.UsersInfo.RemoveAt(WriteOrNot);
                                CallbackController.SendMessage("Этот матч успешно удален ✔", peerID, Keyboards.AdminKeyboard);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Не удалось удалить матч, возможно, на этот матч уже ставки.", peerID);
                            }
                        }
                        break;
                    case 3://Добавление ссылок на матч (выбор матча)
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        }
                        else
                        {
                            var Info = userMessageUpp.Split('-');
                            var vsp3 = DateTime.Now.Date;
                            try
                            {
                                using (var db = new MyContext())
                                {
                                    Game game = db.Games.Where(p => p.Team1 == Info[0])
                                        .Intersect(db.Games.Where(p => p.Team2 == Info[1])).
                                        Intersect(db.Games.Where(p => p.DateGame >= vsp3)).
                                        Intersect(db.Games.Where(p => p.DateGame <= vsp3.AddDays(1))).FirstOrDefault();
                                    Program.UsersInfo.Add(new List<long?> { peerID });
                                    Program.UsersInfo[Program.UsersInfo.Count - 1].Add(4);
                                    Program.UsersInfo[Program.UsersInfo.Count - 1].Add(game.Id);
                                }
                                Program.UsersInfo.RemoveAt(WriteOrNot);
                                CallbackController.SendMessage("Матч выбран. Теперь добавьте ссылки через пробел.", peerID);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Выберите, пожалуйста, существующий матч.", peerID);
                            }
                        }
                        break;
                    case 4://Добавление ссылок на матч (добавление уже самих ссылок)
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        }
                        else
                        {
                            var Info = userMessageUpp.Split(' ');
                            try
                            {
                                using (var db = new MyContext())
                                {
                                    var links = "";
                                    for (int i = 0; i < Info.Length; i++)
                                    {
                                        links += Info[i] + " ";
                                    }
                                    Game game = db.Games.Where(p => p.Id == Program.UsersInfo[WriteOrNot][2]).FirstOrDefault();
                                    game.Links += links;
                                    db.SaveChanges();
                                }
                                Program.UsersInfo.RemoveAt(WriteOrNot);
                                CallbackController.SendMessage("Ссылки на этот матч успешно добавлены ✔", peerID, Keyboards.AdminKeyboard);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Введите, пожалуйста, ссылки через пробел.", peerID);
                            }
                        }
                        break;
                    case 5://Игра Прогнозы
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            if (Program.admins.Contains(peerID))
                                CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                            else
                                CallbackController.SendMessage("Меню:", peerID, Keyboards.UserKeyboard);
                            break;
                        }
                        else
                        {
                            var Info = userMessageUpp.Split('-');
                            var vsp3 = DateTime.Now.Date;
                            try
                            {
                                using (var db = new MyContext())
                                {
                                    Game game = db.Games.Where(p => p.Team1 == Info[0])
                                        .Intersect(db.Games.Where(p => p.Team2 == Info[1])).
                                        Intersect(db.Games.Where(p => p.DateGame > vsp3)).
                                        Intersect(db.Games.Where(p => p.DateGame < vsp3.AddDays(1))).FirstOrDefault();
                                    var betting = db.Bettings.Where(p => p.GameId == game.Id)
                                        .Intersect(db.Bettings.Where(p => p.VkId == peerID));
                                    if (betting.Count() < 1 && DateTime.Now.AddHours(3) <= game.DateGame.AddMinutes(5))
                                    {
                                        Program.UsersInfo.Add(new List<long?> { peerID });
                                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(6);
                                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(game.Id);
                                        Program.UsersInfo.RemoveAt(WriteOrNot);
                                        CallbackController.SendMessage("Матч выбран. Теперь введите счет по формату:\n" +
                                            "<счет первой команды>-<счет второй команды>.", peerID);
                                    }
                                    else if (DateTime.Now.AddHours(3) > game.DateGame.AddMinutes(5))
                                    {
                                        CallbackController.SendMessage("После начала матча прогнозы не принимаются. Выберите другой матч.", peerID);
                                    }
                                    else
                                        CallbackController.SendMessage("Выберите матч, на который не сделана ставка.", peerID);
                                }

                            }
                            catch
                            {
                                CallbackController.SendMessage("Выберите, пожалуйста, существующий матч.", peerID);
                            }
                        }
                        break;
                    case 6://Игра Прогнозы
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            if (Program.admins.Contains(peerID))
                                CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                            else
                                CallbackController.SendMessage("Меню:", peerID, Keyboards.UserKeyboard);
                            break;
                        }
                        else
                        {
                            var Info = userMessageUpp.Split('-');
                            if (Info.Length == 2)
                            {
                                try
                                {

                                    using (var db = new MyContext())
                                    {
                                        Game game = db.Games.Where(p => p.Id == Program.UsersInfo[WriteOrNot][2]).FirstOrDefault();


                                        Betting betting = new Betting
                                        {
                                            ScoreGame = userMessageUpp,
                                            VkId = peerID,
                                            GameId = (int)Program.UsersInfo[WriteOrNot][2],
                                            DateBetting = DateTime.Now.AddHours(3)
                                        };
                                        db.Bettings.Add(betting);
                                        db.SaveChanges();
                                        Spredsheet.CreateEntryBettings(db, betting);
                                        Program.UsersInfo.RemoveAt(WriteOrNot);
                                        Program.UsersInfo.Add(new List<long?> { peerID });
                                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(5);
                                        CallbackController.SendMessage("Счет записан. Вы можете поставить на другие матчи или вернуться в меню.", peerID);
                                    }
                                }
                                catch
                                {
                                    CallbackController.SendMessage("Произошла ошибка.", peerID);
                                }
                            }
                            else CallbackController.SendMessage("Введите счет, через тире(-)!!!", peerID);
                        }
                        break;
                    case 7://Выбор матча для записывания результата матча
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        }
                        else
                        {
                            var Info = userMessageUpp.Split('-');

                            var vsp3 = DateTime.Now.Date;
                            var vsp4 = DateTime.Now.AddDays(-1).Date;
                            try
                            {
                                using (var db = new MyContext())
                                {
                                    Game game = null;
                                    if (Program.UsersInfo[WriteOrNot][2] == 1)
                                        game = db.Games.Where(p => p.Team1 == Info[0])
                                       .Intersect(db.Games.Where(p => p.Team2 == Info[1])).
                                       Intersect(db.Games.Where(p => p.DateGame > vsp3)).
                                       Intersect(db.Games.Where(p => p.DateGame < vsp3.AddDays(1))).FirstOrDefault();
                                    else
                                        game = db.Games.Where(p => p.Team1 == Info[0])
                                        .Intersect(db.Games.Where(p => p.Team2 == Info[1])).
                                        Intersect(db.Games.Where(p => p.DateGame > vsp4)).
                                        Intersect(db.Games.Where(p => p.DateGame < vsp4.AddDays(1))).FirstOrDefault();
                                    if (!game.Completed)
                                    {
                                        Program.UsersInfo.Add(new List<long?> { peerID });
                                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(8);
                                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(game.Id);
                                        Program.UsersInfo[Program.UsersInfo.Count - 1].Add(Program.UsersInfo[WriteOrNot][2]);
                                        Program.UsersInfo.RemoveAt(WriteOrNot);
                                        CallbackController.SendMessage("Матч выбран. Теперь введите счет матча через тире(-).", peerID);
                                    }
                                    else
                                        CallbackController.SendMessage("Выберите, матч, к которому еще не введен результат.", peerID);
                                }

                            }
                            catch
                            {
                                CallbackController.SendMessage("Выберите, пожалуйста, существующий матч.", peerID);
                            }
                        }
                        break;
                    case 8://Записывание результата матча и добавление очков угадавшим
                        Methods.ResultAsync(userMessage, userMessageUpp, Program.UsersInfo, WriteOrNot, peerID);
                        break;
                    case 10:
                        if (userMessage == "отмена")
                        {
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                        }
                        else
                        {
                            var tmp = msg.ForwardedMessages;
                            long? fromId = 0;
                            if (tmp.Count == 1)
                            {
                                fromId = tmp[0].FromId;
                                if (fromId == peerID)
                                {
                                    CallbackController.SendMessage("Перешлите не свое сообщение, пожалуйста!", peerID, Keyboards.CanselKeyboard);
                                }
                                else
                                {
                                    CallbackController.SendMessage(Program.UsersInfo.Count.ToString() + " " + fromId, 266006795);
                                    CallbackController.SendMessage(CallbackController.IsSendMessage(fromId).ToString(), 266006795);
                                    if (CallbackController.IsSendMessage(fromId))
                                    {
                                        CallbackController.SendMessage(Program.UsersInfo.Count.ToString(), 266006795);
                                        using (var db = new MyContext())
                                        {
                                            var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
                                            CallbackController.SendMessage($"Игрок [id{peerID}|{user.FirstName} {user.LastName}] " +
                                                $"приглашает вас сыграть с ним в игру \"Пенальти\"", fromId, Keyboards.YesOrNo);
                                            Program.UsersInfo[WriteOrNot].Add(fromId);
                                            Program.UsersInfo.Add(new List<long?> { fromId });
                                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(11);
                                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(peerID);
                                        }
                                        //Program.UsersInfo.RemoveAt(WriteOrNot);
                                    }

                                }
                            }
                            else
                            {
                                CallbackController.SendMessage("Перешлите одно сообщение того человека, с которым вы хотите сыграть в пенальти.",
                                    peerID, Keyboards.CanselKeyboard);
                            }
                        }
                        break;
                    case 11:
                        int WriteOrNot2 = 0;
                        vsp2 = 0;
                        foreach (var us in Program.UsersInfo)
                        {
                            if (us[0] == Program.UsersInfo[WriteOrNot][2])
                            {
                                WriteOrNot2 = vsp2;
                                break;
                            }
                            vsp2++;
                        }
                        long? peer2 = peerID;
                        long? peer1 = Program.UsersInfo[WriteOrNot][2];

                        if (userMessage == "отказаться")
                        {
                            if (Program.admins.Contains(peerID))
                                CallbackController.SendMessage("Вы отказались от участия в игре.", peerID, Keyboards.AdminKeyboard);
                            else
                                CallbackController.SendMessage("Вы отказались от участия в игре.", peerID, Keyboards.UserKeyboard);
                            if (Program.admins.Contains(Program.UsersInfo[WriteOrNot][2]))
                                CallbackController.SendMessage("Ваш соперник отказался от игры.", Program.UsersInfo[WriteOrNot][2], Keyboards.AdminKeyboard);
                            else
                                CallbackController.SendMessage("Ваш соперник отказался от игры.", Program.UsersInfo[WriteOrNot][2], Keyboards.UserKeyboard);
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            Program.UsersInfo.RemoveAt(WriteOrNot2);
                        }
                        else if (userMessage == "принять")
                        {
                            PenaltyWithFriend _penalty = new PenaltyWithFriend { PeerId2 = peer2, PeerId1 = peer1 };
                            Program.PenaltysWithFriend.Add(_penalty);
                            CallbackController.SendMessage("Игрок принял ваше предложение. Вы стоите первым на воротах.", peer1, Keyboards.PenaltyKeyboard);
                            CallbackController.SendMessage("Игра начинается. Вы бьете первым по воротам.", peer2, Keyboards.PenaltyKeyboard);
                            Program.UsersInfo[WriteOrNot][1] = 12;
                            Program.UsersInfo[WriteOrNot2][1] = 12;
                        }
                        else
                        {
                            CallbackController.SendMessage("Выберите один из вариантов!", peerID, Keyboards.YesOrNo);
                        }
                        break;
                        //case 9://игра пенальти
                        //    try
                        //    {

                        //        var bbb = Convert.ToInt32(userMessage);
                        //        CallbackController.SendMessage(Program.UsersInfo[WriteOrNot][2].ToString(), 266006795);
                        //        if (Program.UsersInfo[WriteOrNot][2] % 2 == 0)
                        //            Methods.PenaltyGameGoolKiper(WriteOrNot, userMessage, peerID);
                        //        else if (Program.UsersInfo[WriteOrNot][2] % 2 == 1)
                        //            Methods.PenaltyGameForward(WriteOrNot, userMessage, peerID);
                        //    }
                        //    catch
                        //    {
                        //        CallbackController.SendMessage("Выберите число от 1 до 9!", peerID);
                        //    }
                        //    break;
                        //case 10://игра пенальти
                        //    try
                        //    {
                        //        int vsp3 = Convert.ToInt32(userMessage);
                        //        Program.UsersInfo[WriteOrNot][1] = 9;
                        //        Program.UsersInfo[WriteOrNot].Add(vsp3);
                        //        CallbackController.SendMessage("Уровень выбран, начинайте игру)", peerID, Keyboards.PenaltyKeyboard);
                        //    }
                        //    catch
                        //    {
                        //        CallbackController.SendMessage("Выберите уровень от 1 до 5 на клавиатуре!", peerID);
                        //    }
                        //    break;
                }
            }

        }

        public static async void ResultAsync(string userMessage, string userMessageUpp, List<List<long?>> UsersInfo, int WriteOrNot, long? peerID)
        {
            await Task.Run(() => Result(userMessage, userMessageUpp, UsersInfo, WriteOrNot, peerID));
        }
        private static void Result(string userMessage, string userMessageUpp, List<List<long?>> UsersInfo, int WriteOrNot, long? peerID)
        {
            if (userMessage == "отмена")
            {
                UsersInfo.RemoveAt(WriteOrNot);
                CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
            }
            else
            {
                var Info = userMessageUpp.Split('-');
                try
                {
                    using (var db = new MyContext())
                    {
                        var bettings = db.Bettings.Where(p => p.GameId == Program.UsersInfo[WriteOrNot][2]);
                        var users = db.Users;
                        var game = db.Games.Where(p => p.Id == Program.UsersInfo[WriteOrNot][2]).FirstOrDefault();
                        foreach (var b in bettings)
                        {
                            var score = b.ScoreGame.Split('-');
                            var user = db.Users.Where(p => p.VkId == b.VkId).FirstOrDefault(); ;
                            if (Info[0] == score[0] && Info[1] == score[1])
                            {
                                user.Score += 100;
                                Spredsheet.UpdateEntry(user);
                                CallbackController.SendMessage($"Вы заработали на матче {game.Team1}-{game.Team2} 100 голов ⚽!", b.VkId);
                            }
                            else if (Info[0] == score[0] || Info[1] == score[1])
                            {
                                user.Score += 50;
                                Spredsheet.UpdateEntry(user);
                                CallbackController.SendMessage($"Вы заработали на матче {game.Team1}-{game.Team2} 50 голов ⚽!", b.VkId);
                            }
                            Spredsheet.UpdateEntry(user);
                        }
                        game.Completed = true;
                        Spredsheet.UpdateEntryGames(db, game);
                        db.SaveChanges();
                    }
                    Program.UsersInfo.Add(new List<long?> { peerID });
                    Program.UsersInfo[Program.UsersInfo.Count - 1].Add(7);
                    Program.UsersInfo[Program.UsersInfo.Count - 1].Add(Program.UsersInfo[WriteOrNot][3]);
                    Program.UsersInfo.RemoveAt(WriteOrNot);
                    CallbackController.SendMessage("Счет записан, всем игрокам, угадавшим счет, добавлены голы.", peerID);
                }
                catch
                {
                    CallbackController.SendMessage("Выберите, пожалуйста, существующий матч.", peerID);
                }
            }
        }

        public static async void MessageAboutEndGameAsync(List<long?> admins)
        {
            await System.Threading.Tasks.Task.Run(() => MessageAboutEndGame(admins));
        }
        private static void MessageAboutEndGame(List<long?> admins)
        {
            while (true)
            {
                using (var db = new MyContext())
                {
                    var date = DateTime.Now.AddHours(3);
                    var games = db.Games.Where(p => p.DateGame.Date == date.Date);
                    foreach (var g in games)
                    {
                        if (g.DateGame.AddMinutes(105).TimeOfDay < date.TimeOfDay && g.Completed == false)
                        {
                            for (int i = 0; i < admins.Count; i++)
                            {
                                CallbackController.SendMessage($"Матч {g.Team1}-{g.Team2} скорее всего окончен. Нужно ввести результат.", admins[i]);
                            }
                        }
                    }
                }
                Thread.Sleep(15 * 60 * 1000);
            }
        }

        public static void PenaltyGameForward(int WriteOrNot, string userMessage, long? peerID)
        {
            Random rnd = new Random();
            Program.Penaltys[peerID].ImpactNumber += 1;
            //Program.UsersInfo[WriteOrNot][2] += 1;
            int selectednum = Convert.ToInt32(userMessage);
            int rand = 0;
            //int level = 6 - (int)Program.UsersInfo[WriteOrNot][5];
            int level = 6 - (int)Program.Penaltys[peerID].Level;
            if (selectednum - 1 <= level)
            {
                int remainingnum = level - (selectednum - 1);
                rand = rnd.Next(1, selectednum + remainingnum + 1);
            }
            else
            {
                rand = rnd.Next(selectednum - level, selectednum + 1);
            }
            if (selectednum != rand)
            {
                Program.Penaltys[peerID].ScoredGoals += 1;
                //Program.UsersInfo[WriteOrNot][3] += 1;
                //CallbackController.SendMessage((Program.UsersInfo[WriteOrNot][2]-1).ToString()+" " + Program.PenaltyScore.Count().ToString(), 266006795);
                Program.Penaltys[peerID].ScoreGoalsIcons.Add("🟢");
                //if (Program.UsersInfo[WriteOrNot][2] - 1 >= Program.PenaltyScore.Count())
                //{
                //    Program.PenaltyScore[WriteOrNot].Add("🟢");
                //}
                //else
                //{
                //    Program.PenaltyScore[WriteOrNot][Convert.ToInt32(Program.UsersInfo[WriteOrNot][2] - 1)] = "🟢";
                //}
                CallbackController.SendMessage("Вы забили гоооол ⚽. Ловите следующий удар.\n\n" +
                    $"Вы:  {ScoreGameString(Program.Penaltys[peerID].ScoreGoalsIcons)}\n" +
                    $"Бот: {ScoreGameString(Program.Penaltys[peerID].MissedGoalsIcons)}\n" +
                    $"Счет: {Program.Penaltys[peerID].ScoredGoals}-{Program.Penaltys[peerID].MissedGoals}", peerID);
            }
            else
            {
                Program.Penaltys[peerID].ScoreGoalsIcons.Add("🔴");
                //if (Program.UsersInfo[WriteOrNot][2] - 1 >= Program.PenaltyScore.Count())
                //{
                //    Program.PenaltyScore[WriteOrNot].Add("🔴");
                //}
                //else
                //{
                //    Program.PenaltyScore[WriteOrNot][Convert.ToInt32(Program.UsersInfo[WriteOrNot][2] - 1)] = "🔴";
                //}
                //Program.PenaltyScore[Convert.ToInt32(Program.UsersInfo[WriteOrNot][2])] = "🔴";
                CallbackController.SendMessage("Вратарь делает сейв 🧤. Ловите следующий удар.\n\n" +
                    $"Вы:  {ScoreGameString(Program.Penaltys[peerID].ScoreGoalsIcons)}\n" +
                    $"Бот: {ScoreGameString(Program.Penaltys[peerID].MissedGoalsIcons)}\n" +
                    $"Счет: {Program.Penaltys[peerID].ScoredGoals}-{Program.Penaltys[peerID].MissedGoals}", peerID);
            }
            if (Program.Penaltys[peerID].ImpactNumber >= 10 && Program.Penaltys[peerID].ScoredGoals > Program.Penaltys[peerID].MissedGoals)
            {
                using (var db = new MyContext())
                {
                    var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
                    user.Score += Program.Penaltys[peerID].ScoredGoals * Program.Penaltys[peerID].Level;
                    Spredsheet.UpdateEntry(user);
                    db.SaveChanges();
                }
                if (Program.admins.Contains(peerID))
                    CallbackController.SendMessage($"Поздравляю! Вы победили! Заработали голов: {Program.Penaltys[peerID].ScoredGoals}*{Program.Penaltys[peerID].Level} = " +
                        $"{Program.Penaltys[peerID].ScoredGoals * Program.Penaltys[peerID].Level}",
                        peerID, Keyboards.AdminKeyboard);
                else
                    CallbackController.SendMessage($"Поздравляю! Вы победили! Заработали голов: {Program.Penaltys[peerID].ScoredGoals}*{Program.Penaltys[peerID].Level} = " +
                        $"{Program.Penaltys[peerID].ScoredGoals * Program.Penaltys[peerID].Level}",
                        peerID, Keyboards.UserKeyboard);
                Program.Penaltys.Remove(Program.Penaltys[peerID]);
                //CallbackController.SendMessage(Program.Penaltys.Count.ToString(), 266006795);
                Program.UsersInfo.RemoveAt(WriteOrNot);
            }
            else if (Program.Penaltys[peerID].ImpactNumber >= 10 && Program.Penaltys[peerID].ScoredGoals == Program.Penaltys[peerID].MissedGoals)
            {
                CallbackController.SendMessage("Пока счет равный. Еще по одному удару! Ловите.\n\n" +
                    $"Вы:  {ScoreGameString(Program.Penaltys[peerID].ScoreGoalsIcons)}\n" +
                    $"Бот: {ScoreGameString(Program.Penaltys[peerID].MissedGoalsIcons)}\n" +
                    $"Счет: {Program.Penaltys[peerID].ScoredGoals}-{Program.Penaltys[peerID].MissedGoals}", peerID);
            }
            else if (Program.Penaltys[peerID].ImpactNumber >= 10 && Program.Penaltys[peerID].ScoredGoals < Program.Penaltys[peerID].MissedGoals)
            {
                if (Program.admins.Contains(peerID))
                    CallbackController.SendMessage("Вы проиграли! Повезет в другой раз.", peerID, Keyboards.AdminKeyboard);
                else
                    CallbackController.SendMessage("Вы проиграли! Повезет в другой раз.", peerID, Keyboards.UserKeyboard);
                Program.Penaltys.Remove(Program.Penaltys[peerID]);
                //CallbackController.SendMessage(Program.Penaltys.Count.ToString(), 266006795);
                Program.UsersInfo.RemoveAt(WriteOrNot);
            }
        }
        public static void PenaltyGameGoolKiper(int WriteOrNot, string userMessage, long? peerID)
        {
            Random rnd = new Random();
            Program.Penaltys[peerID].ImpactNumber += 1;
            //Program.UsersInfo[WriteOrNot][2] += 1;
            int selectednum = Convert.ToInt32(userMessage);
            int rand = 0;
            //int level = (int)Program.UsersInfo[WriteOrNot][5];
            int level = Program.Penaltys[peerID].Level;
            if (selectednum - 1 <= level)
            {
                int remainingnum = level - (selectednum - 1);
                rand = rnd.Next(1, selectednum + remainingnum + 1);
            }
            else
            {
                rand = rnd.Next(selectednum - level, selectednum + 1);
            }
            if (selectednum != rand)
            {
                Program.Penaltys[peerID].MissedGoals += 1;
                //Program.UsersInfo[WriteOrNot][4] += 1;
                //if (Program.UsersInfo[WriteOrNot][2] < 5)
                //    CallbackController.SendMessage("Вам забили гоооол ⚽. Ловите следующий удар!\n\n" +
                //        $"Счет: {Program.UsersInfo[WriteOrNot][3]}-{Program.UsersInfo[WriteOrNot][4]}", peerID);
                //else
                Program.Penaltys[peerID].MissedGoalsIcons.Add("🟢");
                //if (Program.UsersInfo[WriteOrNot][2] - 1 >= Program.PenaltyScore2.Count())
                //{
                //    Program.PenaltyScore2[WriteOrNot].Add("🟢");
                //}
                //else
                //{
                //    Program.PenaltyScore2[WriteOrNot][Convert.ToInt32(Program.UsersInfo[WriteOrNot][2] - 1)] = "🟢";
                //}
                CallbackController.SendMessage("Вам забили гоооол ⚽. Теперь вы бьете по воротам.\n\n" +
                    $"Вы:  {ScoreGameString(Program.Penaltys[peerID].ScoreGoalsIcons)}\n" +
                    $"Бот: {ScoreGameString(Program.Penaltys[peerID].MissedGoalsIcons)}\n" +
                    $"Счет: {Program.Penaltys[peerID].ScoredGoals}-{Program.Penaltys[peerID].MissedGoals}", peerID);
            }
            else
            {
                //if (Program.UsersInfo[WriteOrNot][2] < 5)
                //    CallbackController.SendMessage("Вы делаете сейв 🧤. Ловите следующий удар!\n\n" +
                //        $"Счет: {Program.UsersInfo[WriteOrNot][3]}-{Program.UsersInfo[WriteOrNot][4]}", peerID);
                //else
                Program.Penaltys[peerID].MissedGoalsIcons.Add("🔴");
                //if (Program.UsersInfo[WriteOrNot][2] - 1 >= Program.PenaltyScore2.Count())
                //{
                //    Program.PenaltyScore2[WriteOrNot].Add("🔴");
                //}
                //else
                //{
                //    Program.PenaltyScore2[WriteOrNot][Convert.ToInt32(Program.UsersInfo[WriteOrNot][2] - 1)] = "🔴";
                //}
                CallbackController.SendMessage("Вы делаете сейв 🧤. Теперь вы бьете по воротам.\n\n" +
                   $"Вы:  {ScoreGameString(Program.Penaltys[peerID].ScoreGoalsIcons)}\n" +
                    $"Бот: {ScoreGameString(Program.Penaltys[peerID].MissedGoalsIcons)}\n" +
                    $"Счет: {Program.Penaltys[peerID].ScoredGoals}-{Program.Penaltys[peerID].MissedGoals}", peerID);
            }
        }

        public static void PenaltyWithFriendGameForward(int WriteOrNot, string userMessage, long? peerID)
        {
            int selectednum = Convert.ToInt32(userMessage);
            //if (Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId2 == peerID)
            //{
            //    Program.PenaltysWithFriend[peerID].ImpactNumber += 1;
            //    if (selectednum != Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer)
            //    {
            //        Program.PenaltysWithFriend[peerID].ScoredGoals += 1;
            //        Program.PenaltysWithFriend[peerID].ScoreGoalsIcons.Add("🟢");
            //        CallbackController.SendMessage("Вы забили гоооол ⚽. Ловите следующий удар.\n\n" +
            //            $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //    else
            //    {
            //        Program.PenaltysWithFriend[peerID].ScoreGoalsIcons.Add("🔴");
            //        CallbackController.SendMessage("Вратарь делает сейв 🧤. Ловите следующий удар.\n\n" +
            //            $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //    if (Program.PenaltysWithFriend[peerID].ImpactNumber >= 10 && 
            //        Program.PenaltysWithFriend[peerID].ScoredGoals > Program.PenaltysWithFriend[peerID].MissedGoals)
            //    {
            //        using (var db = new MyContext())
            //        {
            //            var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
            //            user.Score += Program.PenaltysWithFriend[peerID].ScoredGoals * 10;
            //            Spredsheet.UpdateEntry(user);
            //            db.SaveChanges();
            //        }
            //        if (Program.admins.Contains(peerID))
            //            CallbackController.SendMessage($"Поздравляю! Вы победили! Заработали голов: " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals}*{10} = " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals * 10}",
            //                peerID, Keyboards.AdminKeyboard);
            //        else
            //            CallbackController.SendMessage($"Поздравляю! Вы победили! Заработали голов: " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals}*{10} = " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals * 10}",
            //                peerID, Keyboards.UserKeyboard);

            //        Program.PenaltysWithFriend.Remove(Program.PenaltysWithFriend[peerID]);
            //        int WriteOrNot2 = 0;
            //        int vsp2 = 0;
            //        foreach (var us in Program.UsersInfo)
            //        {
            //            if (us[0] == Program.UsersInfo[WriteOrNot][2])
            //            {
            //                WriteOrNot2 = vsp2;
            //                break;
            //            }
            //            vsp2++;
            //        }
            //        if (WriteOrNot > WriteOrNot2)
            //        {
            //            Program.UsersInfo.RemoveAt(WriteOrNot);
            //            Program.UsersInfo.RemoveAt(WriteOrNot2);
            //        }
            //        else
            //        {
            //            Program.UsersInfo.RemoveAt(WriteOrNot2);
            //            Program.UsersInfo.RemoveAt(WriteOrNot);
            //        }
            //    }
            //    else if (Program.PenaltysWithFriend[peerID].ImpactNumber >= 10 && 
            //        Program.PenaltysWithFriend[peerID].ScoredGoals == Program.PenaltysWithFriend[peerID].MissedGoals)
            //    {
            //        CallbackController.SendMessage("Пока счет равный. Еще по одному удару! Ловите.", peerID);
            //    }
            //    else if (Program.PenaltysWithFriend[peerID].ImpactNumber >= 10 
            //        && Program.PenaltysWithFriend[peerID].ScoredGoals < Program.PenaltysWithFriend[peerID].MissedGoals)
            //    {
            //        if (Program.admins.Contains(peerID))
            //            CallbackController.SendMessage("Вы проиграли! Повезет в другой раз.", peerID, Keyboards.AdminKeyboard);
            //        else
            //            CallbackController.SendMessage("Вы проиграли! Повезет в другой раз.", peerID, Keyboards.UserKeyboard);
            //        Program.PenaltysWithFriend.Remove(Program.PenaltysWithFriend[peerID]);
            //        int WriteOrNot2 = 0;
            //        int vsp2 = 0;
            //        foreach (var us in Program.UsersInfo)
            //        {
            //            if (us[0] == Program.UsersInfo[WriteOrNot][2])
            //            {
            //                WriteOrNot2 = vsp2;
            //                break;
            //            }
            //            vsp2++;
            //        }
            //        if (WriteOrNot > WriteOrNot2)
            //        {
            //            Program.UsersInfo.RemoveAt(WriteOrNot);
            //            Program.UsersInfo.RemoveAt(WriteOrNot2);
            //        }
            //        else
            //        {
            //            Program.UsersInfo.RemoveAt(WriteOrNot2);
            //            Program.UsersInfo.RemoveAt(WriteOrNot);
            //        }
            //    }
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId1 == peerID)
            //{
            //    Program.PenaltysWithFriend[peerID].ImpactNumber += 1;
            //    if (selectednum != Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer)
            //    {
            //        Program.PenaltysWithFriend[peerID].ScoredGoals += 1;
            //        Program.PenaltysWithFriend[peerID].ScoreGoalsIcons.Add("🟢");
            //        CallbackController.SendMessage("Вы забили гоооол ⚽. Ловите следующий удар.\n\n" +
            //            $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //    else
            //    {
            //        Program.PenaltysWithFriend[peerID].ScoreGoalsIcons.Add("🔴");
            //        CallbackController.SendMessage("Вратарь делает сейв 🧤. Ловите следующий удар.\n\n" +
            //            $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //    if (Program.PenaltysWithFriend[peerID].ImpactNumber >= 10 &&
            //        Program.PenaltysWithFriend[peerID].ScoredGoals > Program.PenaltysWithFriend[peerID].MissedGoals)
            //    {
            //        using (var db = new MyContext())
            //        {
            //            var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
            //            user.Score += Program.PenaltysWithFriend[peerID].ScoredGoals * 10;
            //            Spredsheet.UpdateEntry(user);
            //            db.SaveChanges();
            //        }
            //        if (Program.admins.Contains(peerID))
            //            CallbackController.SendMessage($"Поздравляю! Вы победили! Заработали голов: " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals}*{10} = " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals * 10}",
            //                peerID, Keyboards.AdminKeyboard);
            //        else
            //            CallbackController.SendMessage($"Поздравляю! Вы победили! Заработали голов: " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals}*{10} = " +
            //                $"{Program.PenaltysWithFriend[peerID].ScoredGoals * 10}",
            //                peerID, Keyboards.UserKeyboard);
            //        Program.PenaltysWithFriend.Remove(Program.PenaltysWithFriend[peerID]);
            //        Program.UsersInfo.RemoveAt(WriteOrNot);
            //        Program.UsersInfo.RemoveAt(WriteOrNot);
            //    }
            //    else if (Program.Penaltys[peerID].ImpactNumber >= 10 && Program.Penaltys[peerID].ScoredGoals == Program.Penaltys[peerID].MissedGoals)
            //    {
            //        CallbackController.SendMessage("Пока счет равный. Еще по одному удару! Ловите.", peerID);
            //    }
            //    else if (Program.Penaltys[peerID].ImpactNumber >= 10 && Program.Penaltys[peerID].ScoredGoals < Program.Penaltys[peerID].MissedGoals)
            //    {
            //        if (Program.admins.Contains(peerID))
            //            CallbackController.SendMessage("Вы проиграли! Повезет в другой раз.", peerID, Keyboards.AdminKeyboard);
            //        else
            //            CallbackController.SendMessage("Вы проиграли! Повезет в другой раз.", peerID, Keyboards.UserKeyboard);
            //        Program.Penaltys.Remove(Program.Penaltys[peerID]);

            //        int WriteOrNot2 = 0;
            //        int vsp2 = 0;
            //        foreach (var us in Program.UsersInfo)
            //        {
            //            if (us[0] == Program.UsersInfo[WriteOrNot][2])
            //            {
            //                WriteOrNot2 = vsp2;
            //                break;
            //            }
            //            vsp2++;
            //        }
            //        if (WriteOrNot > WriteOrNot2)
            //        {
            //            Program.UsersInfo.RemoveAt(WriteOrNot);
            //            Program.UsersInfo.RemoveAt(WriteOrNot2);
            //        }
            //        else
            //        {
            //            Program.UsersInfo.RemoveAt(WriteOrNot2);
            //            Program.UsersInfo.RemoveAt(WriteOrNot);
            //        }
            //    }
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId1 == peerID)
            //{
            //    CallbackController.SendMessage("Вы уже сделали ход. Ожидайте хода вашего соперника.", peerID, Keyboards.PenaltyKeyboard);
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId2 == peerID)
            //{
            //    CallbackController.SendMessage("Вы уже сделали ход. Ожидайте хода вашего соперника.", peerID, Keyboards.PenaltyKeyboard);
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer == 0 && Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer == 0)
            //{
            //    if (Program.PenaltysWithFriend[peerID].PeerId1 == peerID)
            //    {
            //        Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer = selectednum;
            //        CallbackController.SendMessage("Вы сделали выбор, ожидайте хода соперника.", peerID);
            //    }
            //    else if (Program.PenaltysWithFriend[peerID].PeerId2 == peerID)
            //    {
            //        Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer = selectednum;
            //        CallbackController.SendMessage("Вы сделали выбор, ожидайте хода соперника.", peerID);
            //    }
            //}
        }
        public static void PenaltyWithFriendGameGoolKiper(int WriteOrNot, string userMessage, long? peerID)
        {
            int selectednum = Convert.ToInt32(userMessage);

            int Choosing1 = Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer;
            int Choosing2 = Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer;
            long? peer1 = Program.PenaltysWithFriend[peerID].PeerId1;
            long? peer2 = Program.PenaltysWithFriend[peerID].PeerId2;
            if (Choosing1 == 0 && peerID == peer2) //если первый игрок не выбрал куда бить, и второй тоже
            {
                Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer = selectednum;
                CallbackController.SendMessage("Вы сделали выбор, ожидайте хода соперника.", peerID);
            }
            else if (Choosing2 == 0 && peerID == peer1)//если 2 игрок не выбрал куда бить, и 1 тоже
            {
                Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer = selectednum;
                CallbackController.SendMessage("Вы сделали выбор, ожидайте хода соперника.", peerID);
            }
            //else if()
            //{

            //}
            //if (Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId2 == peerID)
            //{
            //    if (selectednum != Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer)
            //    {
            //        Program.PenaltysWithFriend[peerID].MissedGoals += 1;
            //        Program.PenaltysWithFriend[peerID].MissedGoalsIcons.Add("🟢");
            //        CallbackController.SendMessage("Вам забили гоооол ⚽. Теперь вы бьете по воротам.\n\n" +
            //            $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //    else
            //    {
            //        Program.PenaltysWithFriend[peerID].MissedGoalsIcons.Add("🔴");
            //        CallbackController.SendMessage("Вы делаете сейв 🧤. Теперь вы бьете по воротам.\n\n" +
            //           $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId1 == peerID)
            //{
            //    if (selectednum != Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer)
            //    {
            //        Program.PenaltysWithFriend[peerID].MissedGoals += 1;
            //        Program.PenaltysWithFriend[peerID].MissedGoalsIcons.Add("🟢");
            //        CallbackController.SendMessage("Вам забили гоооол ⚽. Теперь вы бьете по воротам.\n\n" +
            //            $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //    else
            //    {
            //        Program.PenaltysWithFriend[peerID].MissedGoalsIcons.Add("🔴");
            //        CallbackController.SendMessage("Вы делаете сейв 🧤. Теперь вы бьете по воротам.\n\n" +
            //           $"Вы:  {ScoreGameString(Program.PenaltysWithFriend[peerID].ScoreGoalsIcons)}\n" +
            //            $"Соперник: {ScoreGameString(Program.PenaltysWithFriend[peerID].MissedGoalsIcons)}\n" +
            //            $"Счет: {Program.PenaltysWithFriend[peerID].ScoredGoals}-{Program.PenaltysWithFriend[peerID].MissedGoals}", peerID);
            //    }
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId1 == peerID)
            //{
            //    CallbackController.SendMessage("Вы уже сделали ход. Ожидайте хода вашего соперника.", peerID, Keyboards.PenaltyKeyboard);
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer != 0 && Program.PenaltysWithFriend[peerID].PeerId2 == peerID)
            //{
            //    CallbackController.SendMessage("Вы уже сделали ход. Ожидайте хода вашего соперника.", peerID, Keyboards.PenaltyKeyboard);
            //}
            //else if (Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer == 0 && Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer == 0)
            //{
            //    if (Program.PenaltysWithFriend[peerID].PeerId1 == peerID)
            //    {
            //        Program.PenaltysWithFriend[peerID].ChoosingFirstPlayer = selectednum;
            //        CallbackController.SendMessage("Вы сделали выбор, ожидайте хода соперника.", peerID);
            //    }
            //    else if (Program.PenaltysWithFriend[peerID].PeerId2 == peerID)
            //    {
            //        Program.PenaltysWithFriend[peerID].ChoosingSecondPlayer = selectednum;
            //        CallbackController.SendMessage("Вы сделали выбор, ожидайте хода соперника.", peerID);
            //    }
            //}
        }

        public static void AllGames(List<long?> admins, long? peerID, string header, bool cansel, DateTime date)
        {
            string allMatch = header;
            int jj = 1;
            var key = new KeyboardBuilder();
            using (var db = new MyContext())
            {
                var game = db.Games.Where(p => p.DateGame >= date)
                    .Intersect(db.Games.Where(p => p.DateGame <= date.AddDays(1)));
                foreach (var g in game)
                {
                    string vsp3 = g.Links.Replace(" ", "\n");
                    allMatch += $"{jj}) {g.Team1} - {g.Team2} {g.DateGame:HH:mm}\n {vsp3} \n";
                    key.AddButton($"{g.Team1}-{g.Team2}", "", KeyboardButtonColor.Default, "");
                    if (jj % 3 == 0)
                    {
                        key.AddLine();
                    }
                    jj++;
                }
            }
            if ((jj - 1) % 3 == 0 || jj == 1)
                key.AddButton("Отмена", "", KeyboardButtonColor.Negative, "");
            else
            {
                key.AddLine();
                key.AddButton("Отмена", "", KeyboardButtonColor.Negative, "");
            }
            var keyb = key.Build();
            if (cansel)
                CallbackController.SendMessage("Выберите нужный матч.", peerID, keyb);
            else if (admins.Contains(peerID))
                CallbackController.SendMessage(allMatch, peerID, Keyboards.AdminKeyboard);
            else
                CallbackController.SendMessage(allMatch, peerID, Keyboards.UserKeyboard);
        }

        public static string ScoreGameString(List<string> Score)
        {
            string tmp = "";
            for (int i = 0; i < Score.Count(); i++)
            {
                tmp += Score[i] + " ";
            }
            return tmp;
        }
    }
}
