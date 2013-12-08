using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 背景モデル管理を担う
    /// </summary>
    class Stage : GameComponent
    {
        private Timeline Time
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public Stage(GameContainer container, Timeline timeline) : base(container)
        {
            Time = timeline;
        }

        public override void Update()
        {
            // TODO: タイムラインに沿って回転と移動を行う
        }
 	
    }
}
