using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief 操作不可のプレイヤー
    /// </summary>
    class DisablePlayer : GameComponent
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public DisablePlayer(GameContainer container) : base(container, false)
        {

        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            if(DX.CheckHitKey(DX.KEY_INPUT_1) == 1)
            {
                Active = false;
            }
        }

        /// <summary>
        /// @brief コンポーネントが無効から有効に切り替わった際に呼び出される
        /// </summary>
        public override void OnEnable()
        {
            base.OnEnable();

            Container.GetComponent<Player>().Active = false;
            Container.GetComponent<RectCollider>().Active = false;
        }

        /// <summary>
        /// @brief コンポーネントが有効から無効に切り替わった際に呼び出される
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();

            Container.GetComponent<Player>().Active = true;
            Container.GetComponent<RectCollider>().Active = true;
        }
    }
}
