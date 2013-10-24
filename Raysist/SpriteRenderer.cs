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
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        /// <param name="path">ファイルの場所</param>
        public SpriteRenderer(GameContainer container, String path) : base(container)
        {
            GraphicHandle = DX.LoadGraph(path);
        }

        /// <summary>
        /// @brief 描画
        /// </summary>
        public override void Update()
        {
            var pos = Position.WorldPosition;
            DX.DrawGraph((int)pos.x, (int)pos.y, GraphicHandle, 0);
        }
    }
}
