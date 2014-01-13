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
            BehindAppearEnemy = 0
        }

        /// <summary>
        /// @brief 敵種類判別テーブル
        /// </summary>
        private static Dictionary<string, EnemyType> EnemyTypes = new Dictionary<string, EnemyType>
        {
            {"BehindAppearEnemy", 0}
        };


        public EnemyController(GameContainer container) : base(container, "enemy.csv")
        {

        }

        protected override void OnUpdateTimeline(string[] record)
        {
            ContainerFactory factory;

            switch (EnemyTypes[record[1]])
            {
                case EnemyType.BehindAppearEnemy:
                    factory = new BehindAppearEnemyFactory(new Vector3 { x = float.Parse(record[2]), y = float.Parse(record[3]), z = float.Parse(record[4]) }, 
                        new Vector3 { x = float.Parse(record[5]), y = float.Parse(record[6]), z = float.Parse(record[7]) }, float.Parse(record[8]));
                    factory.Create();
                    break;
                default:
                    break;
            }
        }
    }
}
