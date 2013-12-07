using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class Shot : GameComponent
    {
        //---------------------------------------------------
        //メンバ変数こっから

        /// <summary>
        /// @brief 体力
        /// </summary>
        public float Angle
        {
            private set;
            get;
        }


        //ここまで
        //----------------------------------------------------

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Shot(GameContainer container, float angle, Vector3 pos)
            : base(container)
        {
            Position.LocalScale *= 0.1f;
            Position.LocalPosition = pos;
            Angle = angle;
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            var wp = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
            var ga = GameScene.GameArea;
            if (wp.x < ga.Left || wp.x > ga.Right ||
                wp.y < ga.Top || wp.y > ga.Bottom)
            {
                // TODO:外に出たら破棄する
                GameContainer.Destroy(Container);
            }

            // 移動処理
            Position.LocalPosition.x += (float)Math.Cos(Angle) * 0.1f;
            Position.LocalPosition.y -= (float)Math.Sin(Angle) * 0.1f;
        }


    }
}
