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
        public List<string> ScoreGoalsIcons { get; set; }
        /// <summary>
        /// Значки пропущенных/отбитых ударов
        /// </summary>
        public List<string> MissedGoalsIcons { get; set; }
    }
    public static Penalty operator [](Penalty pem)
}

