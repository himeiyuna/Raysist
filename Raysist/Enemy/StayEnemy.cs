using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 滞在する敵コンポーネント
    /// </summary>
    class StayEnemy : GameComponent
    {
        /// <summary>
        /// @brief 滞在期間
        /// </summary>
        private int StayTime
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 滞在フレーム数
        /// </summary>
        private int Frame
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="time">滞在期間</param>
        public StayEnemy(GameContainer container, int time) : base(container, false)
        {
            StayTime = time;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            ++Frame;
            if (Frame >= StayTime)
            {
                Active = false;
            }
        }

        /// <summary>
        /// @brief Activeがfalseになったときに呼び出される
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();

            Container.GetComponent<DisappearEnemy>().Active = true;
        }
    }
}
