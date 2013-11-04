using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief カメラクラス
    /// </summary>
    class Camera : GameComponent
    {
        /// <summary>
        /// @brief ビュー変換行列を取得するプロパティ
        /// </summary>
        public Matrix ViewTransform { set; get; }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントを実装するコンテナ</param>
        public Camera(GameContainer container) : base(container)
        {
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            DX.SetCameraViewMatrix(ViewTransform);
        }
    }
}
