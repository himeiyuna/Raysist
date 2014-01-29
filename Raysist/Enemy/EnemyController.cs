using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    class EnemyController : Timeline
    {
        private enum EnemyType
        {
            None = -1,
            BehindAppearEnemy,
            InfrontApeearEnemy
        }

        /// <summary>
        /// @brief プレイヤーの位置
        /// </summary>
        private Player PlayerPosition
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 敵種類判別テーブル
        /// </summary>
        private static readonly Dictionary<string, EnemyType> EnemyTypes = new Dictionary<string, EnemyType>
        {
            {"BehindAppearEnemy", EnemyType.BehindAppearEnemy},
            {"InfrontAppearEnemy", EnemyType.InfrontApeearEnemy}
        };


        public EnemyController(GameContainer container, Player p) : base(container, "enemy.csv")
        {
            PlayerPosition = p;
        }

        protected override void OnUpdateTimeline(string[] record)
        {
            ContainerFactory factory;

            switch (EnemyTypes[record[1]])
            {
                case EnemyType.BehindAppearEnemy:
                    factory = new BehindAppearEnemyFactory(new Vector3 { x = float.Parse(record[2]), y = float.Parse(record[3]), z = float.Parse(record[4]) }, 
                        bool.Parse(record[5]) ? BehindAppearEnemyFactory.Direction.LEFT : BehindAppearEnemyFactory.Direction.RIGHT, 
                        float.Parse(record[6]), PlayerPosition);
                    
                    factory.Create();
                    break;
                case EnemyType.InfrontApeearEnemy:
                    factory = new InfrontAppearEnemyFactory(new Vector3 { x = float.Parse(record[2]), y = float.Parse(record[3]), z = float.Parse(record[4]) },
                        bool.Parse(record[5]) ? InfrontAppearEnemyFactory.Direction.LEFT : InfrontAppearEnemyFactory.Direction.RIGHT,
                        int.Parse(record[6]));
                    factory.Create();
                    break;
                default:
                    break;
            }
        }
    }
}
