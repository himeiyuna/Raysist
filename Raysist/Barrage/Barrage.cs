using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// @brief 弾幕クラスの基底クラス
/// </summary>
namespace Raysist
{
    class Barrage : GameComponent
    {
        /// <summary>
        /// @brief 弾数
        /// </summary>
        public int Magazine
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 体力
        /// </summary>
        public float Angle
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 弾速
        /// </summary>
        public float Speed
        {
            set;
            get;
        }
        /// <summary>
        /// @brief 弾の間隔
        /// </summary>
        public int SpaceanInterval
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Barrage(GameContainer container, float angle, Vector3 pos)
            : base(container)
        {
            Position.LocalPosition = pos;
            Angle = angle;
            Speed = 5.0f;
            Magazine = 20;
            SpaceanInterval = 18;//360/Magazine　弾の間隔;
        }

        /// <summary>
        /// @brief 更新関数
        /// </summary>
        public override void Update()
        {
        }

        /// <summary>
        /// @brief 狙いをつける関数
        /// </summary>
        protected void Aim()
        {
            var a = Container.GetComponent<Player>().Position.LocalPosition;
            var b = Position.LocalPosition;
            Vector2 Direction = new Vector2();
            Direction.x = a.x - b.x;
            Direction.y = a.y - b.y;
        }
    }
}
