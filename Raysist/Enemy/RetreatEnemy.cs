using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 後退する敵コンポーネント
    /// </summary>
    class RetreatEnemy : GameComponent
    {
        /// <summary>
        /// @brief 後退速度
        /// </summary>
        private float Speed
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="speed">後退速度</param>
        public RetreatEnemy(GameContainer container, float speed) : base(container)
        {
            Speed = speed;
        }

        public override void Update()
        {
            Position.LocalPosition.y -= Speed;

            // TODO:後退しきったら削除
        }
    }
}
