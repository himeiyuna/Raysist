using System;
using DxLibDLL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    class Player : GameContainer
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Player() : base()
        {
            AddComponent(new SpriteRenderer(this, "dummy.png"));
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            if (DX.CheckHitKey(DX.KEY_INPUT_W) == 0)
            {
                Position.LocalPosition.x
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 0)
            {
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 0)
            {
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_D) == 0)
            {
            }

            base.Update();
        }
    }
}
