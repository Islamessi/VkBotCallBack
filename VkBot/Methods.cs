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

    public static class Kompliment
    {
        private static List<string> Kompliments = new List<string> 
        {"Женщине яркой,\n " +
            "Женщине стильной,\n " +
            "Женщине жаркой\n " +
            "И сексапильной\n " +
            " —В светлый, прекрасный\n " +
            "День и момент\n " +
            "Ты мой атласный\n " +
            "Прими комплимент!\n ",

            "Прекрасен внешний облик твой,\n " +
            "И мир твой внутренний прекрасен,\n" +
            "Любуюсь я всегда тобой,\n" +
            "вой взгляд пленительно опасен.",

        "Нежна и привлекательна,\n" +
            "Всегда очаровательна.\n" +
            "Веселая и милая,\n" +
            "Чудесная, красивая!",

        "— У вас такие пушистые pесницы. Когда вы моpгаете, на меня дует!",

        "-А вам кто-нибудь говорил, что вы очень красивы? Нет? Вот блин, какие все честные!",

        "— Вы сегодня прекрасно выглядите! Вчера, наверное, бухали?",

        "Ради таких женщин как ты люди идут воевать, я бы тоже, но немного устал. Завтра пойду",

        "Ты как героиня из сказки о спящей красотке, постоянно спишь",

        "Когда ты идешь, все оборачиваются. Должно быть, травматологи разбогатели из-за тебя",

        "Я тут постою у тебя в спальне, а ты сделай вид, что меня нет и переодевайся",

        "Родителям пообещал, что найду красивую невесту, но ты тоже сойдешь. Поехали",

        "Ты настоящая драгоценность, только жаль, что тебя нельзя продать за миллиарды….",

        "Ты слышала об актрисах Голливуда? Они тоже о тебе не слышали",

        "Ты любишь горячих парней? Нет? Ну что же, ты по адресу",

        "У меня аллергия на красивых девушек. Апчхи!",

        "Ты такая горячая, подожди, сейчас остужу тебя. Надо только показать мой пивной живот",

        "Ты и я идеальная пара, но сам по себе я тоже ничего"
        };
        

        public static void AddKomp(string komp)
        {
            Kompliments.Add(komp);
        }

        public static string RerurnKomp()
        {
            Random rnd = new Random();
            return Kompliments[rnd.Next(0,  Kompliments.Count())];
        }
    }

    public static class Motivation
    {
        private static List<string> Motivations = new List<string>
        {

        "Если вы не победите себя, тогда будете побеждены самим собой. (Наполеон Хилл)",

        "Выбери профессию, которую ты любишь, — и тебе не придется работать ни дня в твоей жизни. (Конфуций)",

        "Не плыви по течению, не плыви против течения — плыви туда, куда тебе надо. (Грейс Хоппер)",

        "Подумай, сколько всего ты мог бы сделать, если бы не чужое мнение. (Стивен Кови)",

        "Когда вы достигнете конца верёвки, завяжите узел и держитесь. (Франклин Рузвель)",

        "Многие люди смотрят на окружающий мир и спрашивают: «Почему?» Я смотрю на окружающий мир с надеждой и спрашиваю: «А почему бы и нет?» (Джордж Бернард Шоу)",

        "Когда кажется, что в жизни всё рушится, я начинаю мечтать о том, что построю на освободившемся месте. (Грейс Хоппер)",

        "Самое большое препятствие — страх. Самая большая ошибка — пасть духом. (Джон Вуден)",

        "Многие люди никогда не сделают прорыв в своей жизни, потому что они отказались выйти из зоны комфорта и сделать шаг в неизвестность…(Наполеон Хилл)",

        "Успеха добивается лишь тот, кто остается после того как все остальные уходят. (Уильям Фидер)",

        "Последняя степень неудачи — это первая ступень успеха. (Карло Досси)",

        "Успешный человек больше сосредоточен на том, чтобы делать правильные вещи вместо того, чтобы делать вещи правильно. (Питер Друкер)",

        "Дорога к успеху – это всегда дорога в гору, и чтобы подняться в нее, нужно приложить усилие. (Вилли Дейвис)",

        "Чем просто хотеть рыбы, лучше начни плести сети, чтобы её поймать. (Китайская мудрость)",

        "Хватит писать боту, иди займись делом (Ислам)",

        "Будь всегда голодным. Будь всегда безрассудным. (Стив Джобс)",

        "На свете нет ничего абсолютно ошибочного. Даже сломанные часы дважды в сутки показывают точное время. (Пауло Коэльо)   ",

        "Всю жизнь цепляться за стабильную работу намного рискованнее, чем пойти на риск, чтобы научиться создавать бизнес. " +
            "Один риск носит временный характер, а другой продолжается всю жизнь. (Роберт Кийосаки)",

        "Если Вы можете себе что-либо отчетливо представить – значит Вы можете этого добиться. " +
            "Единственные барьеры в нашей жизни – это те, которые мы сами себе ставим. (Брайан Трейси)",

        "Инвестиции в знания дают самые высокие дивиденды. (Бенджамин Франклин)",

        "Наш большой недостаток в том, что мы слишком быстро опускаем руки. Наиболее верный путь к успеху – все время пробовать еще один раз. (Томас Эдисон)"
        };



        public static void AddMotiviation(string komp)
        {
            Motivations.Add(komp);
        }

        public static string RerurnMotivation()
        {
            Random rnd = new Random();
            return Motivations[rnd.Next(0, Motivations.Count())];
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
                    //switch (Program.UsersInfo[WriteOrNot][1])
                    //{
                         
                    //}
                }
                //CallbackController._vkApi.Messages.SendMessageEventAnswer($"{msgev.EventId}", (long)msgev.UserId, (long)msgev.PeerId);
            });
        }

        public static void SaveUser(long? peerID)
        {
            using (var db = new MyContext())
            {
                var users = db.Users.Where(a => a.VkId == peerID);
                if (users.Count() == 0)
                {
                    User user = new User
                    {
                        Name = CallbackController._vkApi.Users.Get(new long[]
                        { (long)peerID }).FirstOrDefault().FirstName +
                        " " + CallbackController._vkApi.Users.Get(new long[]
                        { (long)peerID }).FirstOrDefault().LastName,
                        VkId = peerID,
                    };
                    db.Users.Add(user);
                    db.SaveChanges();
                    Spredsheet.CreateEntry(db, user);
                    CallbackController.SendMessage($"Поздравляем," +
                        $" {CallbackController._vkApi.Users.Get(new long[] { (long)peerID }).FirstOrDefault().FirstName}, вы зарегестрировались!",
                        peerID, Keyboards.UserKeyboard);
                }
                else
                {
                    CallbackController.SendMessage("Здравствуйте! Вы уже есть в базе данных.",
                        peerID, Keyboards.UserKeyboard);
                }
            }
        }

        public static void SendAboutQst()
        {
            using (var db = new MyContext())
            {
                var games = db.Games;
                var users = db.Users;
                foreach (var game in games)
                {
                    if (game.DateStart > DateTime.Now && game.DateEnd < DateTime.Now && game.IsPublish == false)
                    {
                        foreach (var user in users)
                        {
                            if (CallbackController._vkApi.Messages.IsMessagesFromGroupAllowed(213110775, (ulong)user.VkId))
                            {
                                CallbackController.SendMessage("", user.VkId);
                                game.IsPublish = true;
                                db.Update(game);
                                db.SaveChanges();
                                Spredsheet.UpdateEntryGames(db, game);
                            }
                        }    
                    }
                }
            }
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
            if (userMessage == "отмена")
            {
                
            }
            else
            {
                if (WriteOrNot == -1)
                {
                    switch (userMessage)
                    {
                        case "11":
                            CallbackController.SendMessage("aaaa", peerID);
                            break;
                        case "начать":
                            CallbackController.SendMessage("Здравствуйте! Вас ждет марафон, в котором будут " +
                                "(и дальше красивые слова)... Если хотите принять участие жмите кнопочку ниже.", 
                                peerID, Keyboards.AgreeGame);
                            break;
                        case "принять участие":
                            SaveUser(peerID);
                            break;
                        case "топ игроков":
                            string vsp3 = "Вот топ 10 🏆\n";
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
                                        vsp3 += $"{jj}) [id{b.VkId}|{b.Name}] - {b.Score} 🍔\n";
                                    }
                                    jj++;
                                }
                                if (mesto > 10)
                                {
                                    var user = users.Where(p => p.VkId == peerID).FirstOrDefault();
                                    vsp3 += $"\nВаш рейтинг:\n" +
                                        $"{mesto}) [id{user.VkId}|{user.Name}] - {user.Score} 🍔";
                                }
                                CallbackController.SendMessage(vsp3, peerID, Keyboards.UserKeyboard);
                            }
                            break;
                        case "добавить вопрос":
                            CallbackController.SendMessage("Добавьте вопрос по формату", peerID);
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(1);
                            break;
                        case "пинг":
                            CallbackController.SendMessage("Понг", peerID);
                            break;
                        case "понг":
                            CallbackController.SendMessage("Пошел нахуй...", peerID);
                            break;
                        case "комплимент":
                            CallbackController.SendMessage(Kompliment.RerurnKomp(), peerID);
                            break;
                        case "пинок":
                            CallbackController.SendMessage(Motivation.RerurnMotivation(), peerID);
                            break;
                        case "кто гений":
                            CallbackController.SendMessage("Мой создатель - Ислам. Да! Он гений!", peerID);
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
                        case 1: //Добавление вопроса
                            //var msg2 = userMessageUpp.Split(' ');
                            try
                            {
                                var vsp3 = userMessageUpp.Split('_');
                                var qst = vsp3[0];
                                DateTime dateStart = Convert.ToDateTime(vsp3[1]);
                                DateTime dateEnd = Convert.ToDateTime(vsp3[2]);
                                byte answer = Convert.ToByte(vsp3[3]);
                                CallbackController.SendMessage(qst + "\n" + dateStart + "\n" + dateEnd + "\n" + answer,
                                //CallbackController.SendMessage(vsp3[0]+"\n"+vsp3[1]+"\n"+vsp3[2]+"\n"+vsp3[3], 
                                   peerID, Keyboards.AdminKeyboard);
                                
                                using (var db = new MyContext())
                                {
                                    Game game = new Game
                                    {
                                        Question = qst,
                                        DateStart = dateStart,
                                        DateEnd = dateEnd,
                                        RightAnswer = answer
                                    };
                                    db.Add(game);
                                    db.SaveChanges();
                                    Spredsheet.CreateEntryGames(db, game);
                                }
                                CallbackController.SendMessage("Вопрос добавлен.", peerID);
                            }
                            catch
                            {
                                CallbackController.SendMessage("Не по формату", peerID, Keyboards.AdminKeyboard);
                            }
                            Program.UsersInfo.RemoveAt(WriteOrNot);
                            break;
                    }
                }
            }
            

        }



        
        
    }
}
