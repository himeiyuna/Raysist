using System;
using DxLibDLL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 3Dモデル描画コンポーネント
    /// </summary>
    public class MeshRenderer : Renderer
    {
        /// <summary>
        /// @brief モデルハンドル
        /// </summary>
        private int ModelHandle { set; get; }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        public MeshRenderer(GameContainer container, String path)
            : base(container)
        {
            ModelHandle = DX.MV1LoadModel(path);
        }

        /// <summary>
        /// @brief 描画
        /// </summary>
        public override void Update()
        {
            DX.MV1SetMatrix(ModelHandle, Position.WorldTransform);
        }
    }
}
