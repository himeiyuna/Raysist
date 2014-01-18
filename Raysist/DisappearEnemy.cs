using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    class DisappearEnemy : GameComponent
    {
        private const int AnimationTime = 120;

        /// <summary>
        /// @brief 加速度
        /// </summary>
        private float Accel
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 速度
        /// </summary>
        private float Speed
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 目的地
        /// </summary>
        private Vector3 To
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 差分
        /// </summary>
        private Vector3 Diff
        {
            set;
            get;
        }

        /// <summary>
        /// @brief フレーム数
        /// </summary>
        private float Frame
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        /// <param name="to">目的地</param>
        /// <param name="accel">加速度</param>
        public DisappearEnemy(GameContainer container, Vector3 to, float accel) : base(container, false)
        {
            To = to;
            Accel = accel;
            Diff = new Vector3();
        }

        /// <summary>
        /// @brief Activeがtrueになったとき
        /// </summary>
        public override void OnEnable()
        {
            base.OnEnable();

            Diff = To - Position.LocalPosition;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            ++Frame;
            if (Frame >= AnimationTime)
            {
                GameContainer.Destroy(Container);
                return;
            }

            Speed += Accel;

            float r = Frame / (float)AnimationTime;

            var pos = Position.LocalPosition;
            Position.LocalPosition = new Vector3() { x = pos.x + Diff.x * r, y = pos.y + Speed, z = pos.z + Diff.z * r };
        }
    }
}
