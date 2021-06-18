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
        public long? PeerId { get; set; }
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
        public List<string> ScoreGoalsIcons { get; set; } = new List<string>();
        /// <summary>
        /// Значки пропущенных/отбитых ударов
        /// </summary>
        public List<string> MissedGoalsIcons { get; set; } = new List<string>();
    }

    public class PenaltyWithFriend
    {
        /// <summary>
        /// Идентификатор первого пользователя в вк
        /// </summary>
        public long? PeerId1 { get; set; }

        /// <summary>
        /// Идентификатор второго пользователя в вк
        /// </summary>
        public long? PeerId2 { get; set; }

        /// <summary>
        /// Выбор куда бить/ловить первого игрока
        /// </summary>
        public int ChoosingFirstPlayer { get; set; } = 0;

        /// <summary>
        /// Выбор куда бить/ловить второго игрока
        /// </summary>
        public int ChoosingSecondPlayer { get; set; } = 0;

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
        public List<string> ScoreGoalsIcons { get; set; } = new List<string>();

        /// <summary>
        /// Значки пропущенных/отбитых ударов
        /// </summary>
        public List<string> MissedGoalsIcons { get; set; } = new List<string>();
    }

    public class Penaltys
    {
        private List<Penalty> penalties = new List<Penalty>();

        public int Count => penalties.Count();

        public int Add(Penalty penalty)
        {
            if (penalty != null)
            {
                penalties.Add(penalty);
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

    public class PenaltysWithFriend
    {
        private List<PenaltyWithFriend> penalties = new List<PenaltyWithFriend>();

        public int Count => penalties.Count();

        public int Add(PenaltyWithFriend penalty)
        {
            if (penalty != null)
            {
                penalties.Add(penalty);
                return penalties.Count - 1;
            }
            else
                return -1;
        }

        public int Remove(PenaltyWithFriend penalty)
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


        public PenaltyWithFriend this[long? peerId1]
        {
            get
            {
                PenaltyWithFriend penalty = penalties.FirstOrDefault(p => p.PeerId1 == peerId1);
                return penalty;
            }
        }
        public PenaltyWithFriend this[long peerId2]
        {
            get
            {
                PenaltyWithFriend penalty = penalties.FirstOrDefault(p => p.PeerId2 == peerId2);
                return penalty;
            }
        }
    }

}

