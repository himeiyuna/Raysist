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
        /// @brief 角度
        /// </summary>
        public float Angle
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief 弾速
        /// </summary>
        public float Speed
        {
            private set;
            get;
        }

        public Vector2 Direction
        {
            set;
            get;
        }

        public bool flag
        {
            set;
            get;
        }

        //ここまで
        //----------------------------------------------------

        /// <summary>
        /// @brief コンストラクタ
        /// angle :角度
        /// speed :弾速
        /// pos   :発射位置
        /// </summary>
        public Shot(GameContainer container, float angle, float speed, Vector3 pos)
            : base(container)
        {
            Position.LocalPosition = pos;
            Angle = angle;
            Speed = speed;
        }
        /// <summary>
        /// @brief コンストラクタ
        /// Direction : 向き
        /// speed :弾速
        /// pos   :発射位置
        /// </summary>
        public Shot(GameContainer container, Vector2 direction, float speed, Vector3 pos)
            : base(container)
        {
            Position.LocalPosition = pos;
            Direction = direction;
            Speed = speed;
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
            //if (flag)
            //{
                Position.LocalPosition.x += (float)Math.Cos(Angle) * Speed;
                Position.LocalPosition.y -= (float)Math.Sin(Angle) * Speed;
            //}
            //else
            //{
            //    Position.LocalPosition.x += Direction.x * Speed;
            //    Position.LocalPosition.y -= Direction.y * Speed;
            //}
        }


    }
}
