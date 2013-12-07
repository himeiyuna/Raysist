using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief 3D空間に表示する2D画像を描画するコンポーネント
    /// </summary>
    class BillboardRenderer : SpriteRenderer
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="path">ファイルパス</param>
        public BillboardRenderer(GameContainer container, String path) : base(container, path)
        {

        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            DX.SetWriteZBufferFlag(DX.TRUE);
            var pos = Position.WorldPosition;
            DX.DrawBillboard3D(DX.VGet(pos.x, pos.y, pos.z), 0.5f, 0.5f, Scale, Radian, GraphicHandle, DX.TRUE);
        }
    }
}
