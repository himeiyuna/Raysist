using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief 2D画像描画クラス
    /// </summary>
    public class SpriteRenderer : Renderer
    {
        /// <summary>
        /// @brief グラフィックハンドル
        /// </summary>
        protected int GraphicHandle { set; get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        public SpriteRenderer(GameContainer container)
        :   base(container)
        {
        }

        public override void Update()
        {
            //DX.DrawGraph();
        }
    }
}
