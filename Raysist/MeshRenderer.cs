﻿using System;
using DxLibDLL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
        protected int ModelHandle { set; get; }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        /// <param name="path">Resourceフォルダからのパス</param>
        public MeshRenderer(GameContainer container, string path) : base(container)
        {
            ModelHandle = ResourceController.Instance.LoadModel(path);
        }

        /// <summary>
        /// @brief 描画
        /// </summary>
        public override void Update()
        {
            DX.MV1SetMatrix(ModelHandle, Position.WorldTransform);
            DX.MV1DrawModel(ModelHandle);
        }
    }
}
