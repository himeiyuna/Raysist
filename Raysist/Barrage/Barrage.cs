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
        /// @brief 角度
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
        /// @brief 弾を撃つ間隔
        /// </summary>
        public int Count
        {
            set;
            get;
        }

        /// <summary>
        /// @brief Aimするかどうか
        /// </summary>
        public bool AimFlag
        {
            set;
            get;
        }

        /// <summary>
        /// @brief プレイヤーの位置
        /// </summary>
        public Player PlayerPosition
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Barrage(GameContainer container, float angle, Vector3 pos, Player p)
            : base(container)
        {
            Position.LocalPosition = pos;
            Angle = angle;
            Speed = 5.0f;
            Magazine = 36;
            Count = 10;
            SpaceanInterval = Count;//360/Magazine　弾の間隔;
            AimFlag = false;
            PlayerPosition = p;
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
        protected Vector2 Aim()
        {
            Vector2 Direction = new Vector2();
            var PlayerPos = PlayerPosition.Position.WorldPosition;
            var EnemyPos = Position.WorldPosition;

            Direction.x = (PlayerPos.x - EnemyPos.x);
            Direction.y = (PlayerPos.y - EnemyPos.y);
            return Direction.Normalize();
        }

        /// <summary>
        /// @brief カウントを管理する関数
        /// </summary>
        protected bool CountDown()
        {
            if (Count == 0)
            {
                Count = SpaceanInterval;
                return true;
            }
            else
            {
                --Count;
            }
            return false;
        }
    }
}
