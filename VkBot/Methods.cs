using Cookie.Controllers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.GroupUpdate;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;



using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing.Processors.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;

namespace VkBot
{
    
    public static class AsyncMethods
    {
        public async static void Message()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (DateTime.Now.AddHours(3) > Convert.ToDateTime("09:00") &&
                    DateTime.Now.AddHours(3) < Convert.ToDateTime("23:59"))
                        CallbackController.SendMessage("HelloBro12345", 222634461);
                    else
                        break;
                    Thread.Sleep(1000 * 60 * 10);
                    //Task.Delay(1000 * 60);
                }
                Program.IsStartProverka = false;
                CallbackController.SendMessage("Поток остановлен", 266006795);
            });
        }
    }


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
                    CallbackController.SendMessage($"Поздравляю," +
                        $" {CallbackController._vkApi.Users.Get(new long[] { (long)peerID }).FirstOrDefault().FirstName}, вы зарегестрировались!" +
                        $"",
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
                   if (game.DateStart < DateTime.Now.AddHours(3) && game.DateEnd > DateTime.Now.AddHours(3) && game.IsPublish == false)
                    {
                        foreach (var user in users)
                        {
                            if (CallbackController._vkApi.Messages.IsMessagesFromGroupAllowed(213110775, (ulong)user.VkId))
                            {
                                CallbackController.SendMessage("Появился новый вопрос! Вот он:\n" +
                                    game.Question + "\n" +
                                    "Если ответов несколько, ответы указывать в порядке возрастания без разделителей." +
                                    "Например: (Если правильный ответ это 123, то пишем 123 без пробелов и других разделителей).", user.VkId);
                                game.IsPublish = true;
                                db.Update(game);
                                db.SaveChanges();
                                Spredsheet.UpdateEntryGames(db, game);

                            }
                            else
                            {
                                CallbackController.SendMessage($"[id{user.VkId}|{user.Name}]", 266006795);
                            }
                        }    
                    }
                }
            }
        }


        public static void SendQestUser(int numstart, int numend, long? peerID)
        {
            Random rnd1 = new Random();
            var num = rnd1.Next(numstart, numend);
            CallbackController.SendMessage(num.ToString(), peerID);
            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(numstart);//Начиная с какого вопроса [4]
            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(numend);//Заканчивая каким вопросом (на 1 больше)[5]
            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(num);
            string question = Program.Question[num][0];
            SendQuestion(question, peerID);
        }
        public static void TopUsers(long? peerID)
        {
            string vsp3 = "                       Вот топ 10 🏆\n\n\n";
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
                        //vsp3 += $"{jj}) [id{b.VkId}|{b.Name}] - {b.Score} 🍔\n";
                        //vsp3 += $"{jj} )   {b.Name}  -  {b.Score} 🍔\n";
                        vsp3 += String.Format("{0, -3}) {1, -30}  -  {2, 4} \n",
                                                jj, $"[id{b.VkId}|{b.Name}]", b.Score);

                    }
                    jj++;
                }
                if (mesto > 10)
                {
                    var user = users.Where(p => p.VkId == peerID).FirstOrDefault();
                    //vsp3 += $"\nВаш рейтинг:\n" +
                    //     $"{mesto}) [id{user.VkId}|{user.Name}] - {user.Score} 🍔";
                    vsp3 += $"\nВаш рейтинг:\n" + $"{mesto}    {user.Name}    {user.Score} \n";
                        //String.Format("{0, -3}) {1, -30}  -  {2, 4} 🍔\n",
                        //    mesto, $"{user.Name}", user.Score);
                }
                //CallbackController.SendMessage(vsp3, peerID, Keyboards.UserKeyboard);


                var uploadServer = CallbackController._vkApi.Photo.GetMessagesUploadServer((long)peerID);
                var wc = new WebClient();
                try
                {
                    SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(@"/app/aa.jpg");

                    FontFamily fontFamily = SystemFonts.Families.ElementAt(1); //Where(p => p.Name == "aaa");//Get("Tahoma");
                    var font = new Font(fontFamily, 40, FontStyle.Regular);

                    TextOptions options = new TextOptions(font)
                    {
                        Origin = new SixLabors.ImageSharp.PointF(850, 20), // Set the rendering origin.
                        TabWidth = 10, // A tab renders as 8 spaces wide
                        WrappingLength = 10000, // Greater than zero so we will word wrap at 100 pixels wide
                        HorizontalAlignment = HorizontalAlignment.Right // Right align
                    };

                    IBrush brush = Brushes.Horizontal(SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black);
                    IPen pen = Pens.DashDot(SixLabors.ImageSharp.Color.Black, 10);
                    string text = vsp3;
                    //CallbackController.SendMessage(text, 266006795);
                    //text = "Вот топ 10:";
                    // Draws the text with horizontal red and blue hatching with a dash dot pattern outline.
                    image.Mutate(x => x.DrawText(options, text, SixLabors.ImageSharp.Color.Black));

                    image.Save("/app/aaa.jpg");

                }
                catch (Exception e)
                {
                    CallbackController.SendMessage(e.Message, 266006795);
                }

                var result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/aaa.jpg"));
                var photos3 = CallbackController._vkApi.Photo.SaveMessagesPhoto(result);
                Random rnd1 = new Random();
                CallbackController._vkApi.Messages.Send(new MessagesSendParams
                {
                    RandomId = rnd1.Next(), // уникальный
                    Attachments = photos3,
                    //Message = "Message",
                    PeerId = peerID,
                    Keyboard = Keyboards.UserKeyboard,
                });
            }
        }

        public static void SendQuestion(string question, long? peerID)
        {
            var uploadServer = CallbackController._vkApi.Photo.GetMessagesUploadServer((long)peerID);
            var wc = new WebClient();
            string result = "-1";

            if (question == Program.Question[0][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/Sensisept.png"));
            else if (question == Program.Question[1][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/Aquaguard.png"));
            else if (question == Program.Question[2][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/DR.png"));
            else if (question == Program.Question[3][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/HA.png"));
            else if (question == Program.Question[4][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/FC.png"));
            else if (question == Program.Question[5][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/SK.png"));
            else if (question == Program.Question[6][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/D4.png"));
            else if (question == Program.Question[7][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/cafe tab.png"));
            else if (question == Program.Question[8][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/milkclean.png"));
            else if (question == Program.Question[9][0])
                result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/SG.png"));
            if (result == "-1")
            {
                CallbackController.SendMessage(question, peerID);
            }
            else
            {
                var photos3 = CallbackController._vkApi.Photo.SaveMessagesPhoto(result);
                CallbackController.SendMessage(question, peerID, photos3);
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
            if (userMessage == "отмена" || userMessage == "выйти из теста" || userMessage == "выйти в меню")
            {
                if (WriteOrNot != -1)
                {
                    Program.UsersInfo.RemoveAt(WriteOrNot);
                    CallbackController.SendMessage("Меню", peerID, Keyboards.UserKeyboard);
                }
                else
                    CallbackController.SendMessage("Меню", peerID, Keyboards.UserKeyboard);
            }
            else
            {
               


                if (WriteOrNot == -1)
                {
                    switch (userMessage)
                    {
                        case "аа":
                            if (Program.IsStartProverka == false)
                            {
                                AsyncMethods.Message();
                                Program.IsStartProverka = true;
                                CallbackController.SendMessage("Проверка на время опроса начата", peerID);
                            }
                            else
                            {
                                CallbackController.SendMessage("Проверка уже была запущена", peerID);
                            }
                            break;
                        case "начать":
                            CallbackController.SendMessage(Keyboards.Privetstvie,
                                peerID, Keyboards.AgreeGame);
                            break;
                        case "принять участие":
                            SaveUser(peerID);
                            break;
                        case "ку":
                            {
                                //CallbackController._vkApi.IsAuthorized.
                                //CallbackController.SendMessage("sssa", 266006795);


                                //Graphics g = Graphics.FromImage(image);
                                //Font font = new Font("Speedee Condensed", 50);

                                //SolidBrush color = new SolidBrush(Color.Black);

                                //g.DrawString("ЕВАААААААА", font, color, 500, 10);
                                //image.Save(@"/app/aaa.jpg", ImageFormat.Jpeg);

                                var uploadServer = CallbackController._vkApi.Photo.GetMessagesUploadServer((long)peerID);
                                var wc = new WebClient();
                                try
                                {
                                    SixLabors.ImageSharp.Image image = SixLabors.ImageSharp.Image.Load(@"/app/aa.jpg");

                                    FontFamily fontFamily = SystemFonts.Families.ElementAt(1); //Where(p => p.Name == "aaa");//Get("Tahoma");
                                    var font = new Font(fontFamily, 30, FontStyle.Regular);

                                    TextOptions options = new TextOptions(font)
                                    {
                                        Origin = new SixLabors.ImageSharp.PointF(500, 100), // Set the rendering origin.
                                        TabWidth = 8, // A tab renders as 8 spaces wide
                                        WrappingLength = 100, // Greater than zero so we will word wrap at 100 pixels wide
                                        HorizontalAlignment = HorizontalAlignment.Right // Right align
                                    };

                                    IBrush brush = Brushes.Horizontal(SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.Black);
                                    IPen pen = Pens.DashDot(SixLabors.ImageSharp.Color.Black, 10);
                                    string text = "УРАААААААА";

                                    // Draws the text with horizontal red and blue hatching with a dash dot pattern outline.
                                    image.Mutate(x => x.DrawText(options, text, SixLabors.ImageSharp.Color.Black));

                                    image.Save("/app/aaa.jpg");

                                }
                                catch (Exception e)
                                {
                                    CallbackController.SendMessage(e.Message, 266006795);
                                }

                                var result = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/aaa.jpg"));
                                var  photos3 = CallbackController._vkApi.Photo.SaveMessagesPhoto(result);
                                Random rnd2 = new Random();
                                CallbackController._vkApi.Messages.Send(new MessagesSendParams
                                {
                                    RandomId = rnd2.Next(), // уникальный
                                    Attachments = photos3,
                                    Message = "Message",
                                    PeerId = peerID
                                });

                                //    CreateAlbum(new PhotoCreateAlbumParams 
                                //    { 
                                //        GroupId = 213110775, 
                                //        Title = "aaa",
                                //        CommentsDisabled = false,
                                //        UploadByAdminsOnly = false,
                                //        Description = "fff",

                                //    }); //Get(new PhotoGetParams
                                ////{
                                ////    AlbumId = PhotoAlbumType.Id(albumid),
                                ////    OwnerId = 213110775,
                                ////    PhotoIds = new List<string> { "457239017" }

                                ////}) ;
                                //CallbackController.SendMessage("sssa5", 266006795);
                                //Random rnd1 = new Random();
                                //CallbackController._vkApi.Messages.Send(new MessagesSendParams
                                //{
                                //    RandomId = rnd1.Next(), // уникальный
                                //    Attachments =
                                //    new List<VkNet.Model.Attachments.MediaAttachment>()
                                //    {new Photo },
                                //    Message = "Message",
                                //    PeerId = 266006795
                                //});
                                //var skmdksam = new List<VkNet.Model.Attachments.MediaAttachment> { }
                                //CallbackController.SendMessage("sssa", 266006795);
                                //System.Drawing.Image image = System.Drawing.Image.FromFile(@"aa.jpg");
                                //CallbackController.SendMessage("sssa", 266006795);
                                //Graphics g = Graphics.FromImage(image);
                                //CallbackController.SendMessage("sssa", 266006795);
                                //Random rnd = new Random();
                                //CallbackController.SendMessage("sssa", 266006795);
                                //CallbackController._vkApi.Messages.Send(new MessagesSendParams
                                //{
                                //    RandomId = rnd.Next(), // уникальный
                                //    Attachments = (IEnumerable<VkNet.Model.Attachments.MediaAttachment>)image,
                                //    Message = "Message",
                                //    PeerId = 266006795
                                //});
                                //CallbackController.SendMessage("sssa", 266006795);
                            }
                            break;
                        case "топ игроков":
                            //TopUsers(peerID);
                            {
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
                                            //vsp3 += $"{jj}) [id{b.VkId}|{b.Name}] - {b.Score} 🍔\n";
                                            vsp3 += String.Format("{0, -3}) {1, -30}  -  {2, 4} 🍔\n",
                                                jj, $"[id{b.VkId}|{b.Name}]", b.Score);
                                        }
                                        jj++;
                                    }
                                    if (mesto > 10)
                                    {
                                        var user = users.Where(p => p.VkId == peerID).FirstOrDefault();
                                        //vsp3 += $"\nВаш рейтинг:\n" +
                                        //     $"{mesto}) [id{user.VkId}|{user.Name}] - {user.Score} 🍔";
                                        vsp3 += $"\nВаш рейтинг:\n" +
                                            String.Format("{0, -3}) {1, -30}  -  {2, 4} 🍔\n",
                                                mesto, $"[id{user.VkId}|{user.Name}]", user.Score);
                                    }
                                    CallbackController.SendMessage(vsp3, peerID, Keyboards.UserKeyboard);
                                }
                            }
                            break;
                        case "добавить вопрос":
                            if (Program.admins.Contains(peerID))
                            {
                                CallbackController.SendMessage("Добавьте вопрос по формату: " +
                                "Вопрос_Дата начала(mm.dd.yyyy hh:mm)_Дата окончания(mm.dd.yyyy hh:mm)" +
                                "_Правильный ответ(Без разделителей)_Поянительный правильный ответ", peerID);
                                Program.UsersInfo.Add(new List<long?> { peerID });
                                Program.UsersInfo[Program.UsersInfo.Count - 1].Add(1);
                            }
                            break;
                        case "пинг":
                            CallbackController.SendMessage("Понг", peerID);
                            break;
                        case "комплимент":
                            CallbackController.SendMessage(Kompliment.RerurnKomp(), peerID);
                            break;
                        case "тесты":
                            CallbackController.SendMessage("Это новый раздел!\nЗдесь будут тесты по некоторым темам, " +
                                "за которые можно получить бургеры!!! Одно условие - пройди тест на 100% правильно, " +
                                "и получишь 20 бургеров за каждый тест)) Попыток бесконечно много. Но начислю тебе" +
                                " бургеры только один раз)) Также ты не будешь понимать на какой вопрос ты ответил правильно, " +
                                "а на какой - нет. Выбирай какой тест хочешь пройти! \n" +
                                "Если кнопка красная, то ты не прошел тест на 100%. Если зеленая - прошел и получил " +
                                "свои заслуженные бургеры)).", peerID, Keyboards.UserTesty(peerID));
                            break;
                        case "тест по химии":
                        case "тест2":
                            CallbackController.SendMessage("Учти, что тест надо проходить сразу, иначе я " +
                                "аннулирую твою попытку). Если ответов несколько пиши их в порядке возрастания " +
                                "без разделителей (123). \n" +
                                "Удачи!", peerID, Keyboards.CansellKeyboard);
                            

                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(100);//Понимание, что пользователь сейчас в Тесте.
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(101);//Флаг, ответил ли пользователь на все ответы правильно. (101 - да, 102 - нет)
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(1);//Количество вопросов на которые ответил
                            

                            if (userMessage == "тест по химии")
                            {
                                SendQestUser(0, 15, peerID);
                                //Random rnd1 = new Random();
                                //var num = rnd1.Next(0, Program.Question.Count - 1);
                                //Program.UsersInfo[Program.UsersInfo.Count - 1].Add(0);//Начиная с какого вопроса [4]
                                //Program.UsersInfo[Program.UsersInfo.Count - 1].Add(17);//Заканчивая каким вопросом (на 1 больше)[5]
                                //Program.UsersInfo[Program.UsersInfo.Count - 1].Add(num);
                                //string question = Program.Question[num][0];
                                //SendQuestion(question, peerID);
                            }
                            else if (userMessage == "тест2")
                            {
                                SendQestUser(16, 17, peerID);
                            }

                            break;

                        case "сборка бургеров":
                            CallbackController.SendMessage("Выбери бургер который хочешь собрать)", peerID, Keyboards.UserBurgers);
                            break;
                        case "гамбургер":
                        case "чизбургер":
                            Program.UsersInfo.Add(new List<long?> { peerID });
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(2);//пользователь играет в собери бургер
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(0); //первый эллемент (счетчик на каком эллементе сейчас пользоватлеь)
                            Program.UsersInfo[Program.UsersInfo.Count - 1].Add(
                                Program.Burgers.FindIndex(p => p.BurgerName == userMessage));//Каким по счету идет данный бургер в списке Burgers
                            CallbackController.SendMessage("Начни собирать бургер по порядку (как он собираются на производстве)" +
                                "", peerID, Keyboards.UserSostavBurgers);
                            break;
                        case "пинок":
                            CallbackController.SendMessage(Motivation.RerurnMotivation(), peerID);
                            break;
                        case "кто гений":
                            CallbackController.SendMessage("Мой создатель - Ислам. Да! Он гений!", peerID);
                            break;
                        default:
                            try
                            {
                                int vsp4 = Convert.ToInt32(userMessageUpp);
                                using (var db = new MyContext())
                                {
                                    var game1 = db.Games.Where(p => p.IsPublish == true)
                                        .Where(p => p.DateEnd > DateTime.Now.AddHours(3))
                                        .Where(p => p.DateStart < DateTime.Now.AddHours(3));
                                    if (game1.Count() > 0)
                                    {
                                        var game = game1.Last();
                                        var betts = db.Bettings.Where(p => p.Game == game)
                                            .Where(p => p.VkId == peerID);
                                        
                                        if (betts.Count() < 1)
                                        {
                                            Betting betting = new Betting
                                            {
                                                Game = game,
                                                VkId = peerID,
                                                AnswerUser = vsp4,
                                                DateBetting = DateTime.Now,
                                            };
                                            db.Add(betting);
                                            CallbackController.SendMessage("Ответ принят.", peerID);
                                            Spredsheet.CreateEntryBettings(db, betting);
                                            var user = db.Users.Where(p => p.VkId == peerID).FirstOrDefault();
                                            if (vsp4 == game.RightAnswer)
                                            {
                                                user.Score += 1;
                                                string str4 = "Вы ответили правильно! И заработали 1 🍔.\n" +
                                                    "Ждите следующего вопроса!Вот правильный ответ:\n\n";
                                                str4 += game.Answer;
                                                CallbackController.SendMessage(str4, peerID, Keyboards.UserKeyboard);
                                            }
                                            else
                                            {
                                                string str4 = "Вы ответили неправильно. \n" +
                                                    "Ждите следующего вопроса! Правильный ответ:\n\n";
                                                str4 += game.Answer;
                                                CallbackController.SendMessage(str4, peerID, Keyboards.UserKeyboard);
                                            }
                                            user.NumSurv += 1;
                                            db.Update(user);
                                            Spredsheet.UpdateEntry(user);
                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            CallbackController.SendMessage("Вы уже проголосовали в этом опросе. \n" +
                                                "Ждите следующего вопроса!", peerID, Keyboards.UserKeyboard);
                                            //var bet = betts.FirstOrDefault();
                                            //CallbackController.SendMessage(bet.GameId.ToString()+"\n"+
                                             //   bet.VkId.ToString() + "\n" +
                                              //  bet.AnswerUser.ToString() + "\n" +
                                              //  bet.Id.ToString(), 266006795);
                                        }
                                    }
                                    else
                                    {
                                        CallbackController.SendMessage("В данный момент нет действующего опроса.\n" +
                                                "Ждите следующего вопроса!", peerID, Keyboards.UserKeyboard);
                                    }

                                }
                            }
                            catch
                            {
                                if (Program.admins.Contains(peerID))
                                    CallbackController.SendMessage("Меню:", peerID, Keyboards.AdminKeyboard);
                                else
                                    CallbackController.SendMessage("Меню:", peerID, Keyboards.UserKeyboard);
                            }
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
                                int answer = Convert.ToInt32(vsp3[3]);
                                string answer2 = vsp3[4];
                                CallbackController.SendMessage(qst + "\n" + dateStart + "\n" + dateEnd + "\n" 
                                    + answer +"\n" + answer2,
                                //CallbackController.SendMessage(vsp3[0]+"\n"+vsp3[1]+"\n"+vsp3[2]+"\n"+vsp3[3], 
                                   peerID, Keyboards.AdminKeyboard);
                                
                                using (var db = new MyContext())
                                {
                                    Game game = new Game
                                    {
                                        Question = qst,
                                        DateStart = dateStart,
                                        DateEnd = dateEnd,
                                        RightAnswer = answer,
                                        Answer = answer2,
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
                        case 100: //Ответы в тестах
                            try
                            {
                                //CallbackController.SendMessage(Program.UsersInfo[WriteOrNot][3] +" "+Program.UsersInfo[WriteOrNot][4].ToString() +" "+
                                //    Program.UsersInfo[WriteOrNot][5].ToString() +" "+ 
                                //    Program.UsersInfo[WriteOrNot].Count, 266006795);
                                int vsp4 = Convert.ToInt32(userMessageUpp);
                                int indexofquest =
                                    Convert.ToInt32(Program.UsersInfo[WriteOrNot][Program.UsersInfo[WriteOrNot].Count - 1]);
                                int rightanswer = Convert.ToInt32(Program.Question[indexofquest][1]);
                                if (rightanswer != vsp4)
                                {
                                    Program.UsersInfo[WriteOrNot][2] = 102;
                                    CallbackController.SendMessage("В данном вопросе вы ответили неправильно.", peerID);
                                }
                                Random rnd33 = new Random();
                                var num = rnd33.Next((int)Program.UsersInfo[WriteOrNot][4], (int)Program.UsersInfo[WriteOrNot][5]+1);
                                bool flag = true;
                                Program.UsersInfo[WriteOrNot][3] += 1;//учитываем количество вопросов.
                                if (Program.UsersInfo[WriteOrNot][3] <= (int)Program.UsersInfo[WriteOrNot][5]- (int)Program.UsersInfo[WriteOrNot][4]+1)
                                {
                                    while (flag == true)
                                    {
                                        flag = false;
                                        for (int ii = 6; ii <= Program.UsersInfo[WriteOrNot].Count - 1; ii++)
                                        {
                                            if (Program.UsersInfo[WriteOrNot][ii] == num)
                                            {
                                                num = rnd33.Next((int)Program.UsersInfo[WriteOrNot][4], (int)Program.UsersInfo[WriteOrNot][5]+1);
                                                
                                                flag = true;
                                                break;
                                            }
                                        }
                                    }
                                    Program.UsersInfo[WriteOrNot].Add(num);
                                    string question = Program.Question[num][0];
                                    SendQuestion(question, peerID);
                                }
                                else
                                {
                                    if (Program.UsersInfo[WriteOrNot][2] == 102)
                                    {
                                        CallbackController.SendMessage("Вы где-то ответили неправильно(( Попробуйте снова.",
                                            peerID, Keyboards.UserKeyboard);
                                    }
                                    else if (Program.UsersInfo[WriteOrNot][2] == 101)
                                    {
                                        CallbackController.SendMessage("Вы ответили на 100% правильно!!! Поздравляю!",
                                            peerID, Keyboards.UserKeyboard);
                                        using (var db = new MyContext())
                                        {
                                            User user = db.Users.Where(p => p.VkId == peerID).First();
                                            if (user.IsHimia == false)
                                            {
                                                user.IsHimia = true;
                                                user.Score += 20;
                                                Spredsheet.UpdateEntry(user);
                                                db.SaveChanges();
                                                CallbackController.SendMessage("Я начислил вам 20 бургеров) Так держать!\n" +
                                                    "Горжусь!",
                                            peerID, Keyboards.UserKeyboard);
                                            }
                                        }
                                    }
                                    Program.UsersInfo.RemoveAt(WriteOrNot);
                                }
                            }


                            catch(Exception ex)
                            {
                                CallbackController.SendMessage(ex.Message, 266006795);
                                CallbackController.SendMessage("Отправьте, пожалуйста, число " +
                                    "(номер правильного ответа).", peerID);
                            }
                            break;
                        case 2:
                            {
                                CallbackController.SendMessage("aaa", peerID);
                                int numburger = (int)Program.UsersInfo[WriteOrNot][3];
                                var FileNames = Program.Burgers[numburger].FileNames;
                                var ChastiBurger = Program.Burgers[numburger].ChastiBurger;
                                int numInBurger = Program.Burgers[numburger].NumInBurger;
                                int numVopros = (int)Program.UsersInfo[WriteOrNot][2];
                                if (ChastiBurger[numVopros] == userMessage)
                                {
                                    if (numVopros <= numInBurger-1)
                                    {
                                        var uploadServer = CallbackController._vkApi.Photo.GetMessagesUploadServer((long)peerID);
                                        var wc = new WebClient();
                                        var result2 = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, FileNames[numVopros]));
                                        var photos3 = CallbackController._vkApi.Photo.SaveMessagesPhoto(result2);
                                        CallbackController.SendMessage("Верно! Понали дальше!", peerID, photos3);
                                        Program.UsersInfo[WriteOrNot][2]++;
                                    }
                                    if (numVopros == numInBurger-1)
                                    {
                                        var uploadServer = CallbackController._vkApi.Photo.GetMessagesUploadServer((long)peerID);
                                        var wc = new WebClient();
                                        var result2 = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, FileNames[numVopros+1]));
                                        var photos3 = CallbackController._vkApi.Photo.SaveMessagesPhoto(result2);
                                        CallbackController.SendMessage("Иииии вооот он!\n" +
                                             Program.Burgers[numburger].BurgerLastName + Program.Burgers[numburger].BurgerName, 
                                             peerID, Keyboards.UserBurgers, photos3);
                                        Program.UsersInfo.RemoveAt(WriteOrNot);
                                    }
                                }
                                else
                                    CallbackController.SendMessage("Неверно( Попробуй снова.", peerID);
                                //switch (Program.UsersInfo[WriteOrNot][2])
                                //{
                                //    case 1:
                                //        if (userMessage == "верхушка стандартной булочки")
                                //        {
                                //            var uploadServer = CallbackController._vkApi.Photo.GetMessagesUploadServer((long)peerID);
                                //            var wc = new WebClient();
                                //            var result2 = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, @"/app/photoburgers/Верхушка стандартной булочки.png"));
                                //            var photos3 = CallbackController._vkApi.Photo.SaveMessagesPhoto(result2);
                                //            CallbackController.SendMessage("Верно! Понали дальше!", peerID, photos3);
                                //        }
                                //        else
                                //        {
                                //            CallbackController.SendMessage("Неверно( Попробуй снова.", peerID);
                                //        }
                                //        break;
                                //}
                            }
                            break;
                    }
                }
            }
            

        }



        
        
    }
}
