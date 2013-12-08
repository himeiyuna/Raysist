using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief ゲーム内時間管理クラス
    /// </summary>
    class Timeline : GameComponent
    {
        /// <summary>
        /// @brief 時間軸
        /// </summary>
        public int Time
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public Timeline(GameContainer container) : base(container)
        {
            Time = 0;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            ++Time;
        }
    }
}
