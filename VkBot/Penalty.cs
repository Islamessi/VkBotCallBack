using Cookie.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkBot
{
    public class Penalty
    {
        /// <summary>
        /// Идентификатор пользователя в вк
        /// </summary>
        public long? PeerId
        {
            get
            {
                return PeerId;
            }
            set
            {
                if (PeerId != null)
                    PeerId = value;
            }
        }
        /// <summary>
        /// Номер удара
        /// </summary>
        public int ImpactNumber { get; set; } = 0;
        /// <summary>
        /// Количество забитых голов
        /// </summary>
        public int ScoredGoals { get; set; } = 0;
        /// <summary>
        /// Пропущено голов
        /// </summary>
        public int MissedGoals { get; set; } = 0;
        /// <summary>
        /// Уровень игры (1-5)
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Значки забитых/незабитых ударов
        /// </summary>
        public List<string> ScoreGoalsIcons { get; set; }
        /// <summary>
        /// Значки пропущенных/отбитых ударов
        /// </summary>
        public List<string> MissedGoalsIcons { get; set; }
    }

    public class Penaltys
    {
        private List<Penalty> penalties = new List<Penalty>();

        public int Count => penalties.Count();

        public int Add(Penalty penalty)
        {
            if (penalty != null)
            {
                CallbackController.SendMessage("aa1", 266006795);
                penalties.Add(penalty);
                CallbackController.SendMessage("aa1", 266006795);
                return penalties.Count - 1;
            }
            else
                return -1;
        }

        public int Remove(Penalty penalty)
        {
            if (penalty != null)
            {
                penalties.Remove(penalty);
                return 1;
            }
            else
            {
                return -1;
            }
        }


        public Penalty this[long? peerId]
        {
            get
            {
                Penalty penalty = penalties.FirstOrDefault(p => p.PeerId == peerId);
                return penalty;
            }
            set { }
        }
    }

}

