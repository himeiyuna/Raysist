using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using System.Reflection;

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
        protected int GraphicHandle 
        {
            set; 
            get;
        }

        /// <summary>
        /// @brief 回転量
        /// </summary>
        public float Radian
        {
            set;
            get;
        }

        /// <summary>
        /// @brief スケール
        /// </summary>
        public float Scale
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 画像の幅を取得するプロパティ
        /// </summary>
        public int Width
        {
            get
            {
                int width;
                int height;
                DX.GetGraphSize(GraphicHandle, out width, out height);

                return width;
            }
        }

        /// <summary>
        /// @brief 画像の高さを取得するプロパティ
        /// </summary>
        public int Height
        {
            get
            {
                int width;
                int height;
                DX.GetGraphSize(GraphicHandle, out width, out height);

                return height;
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        /// <param name="path">ファイルの場所</param>
        public SpriteRenderer(GameContainer container, String path) : base(container)
        {
            GraphicHandle = DX.LoadGraph("Resources\\" + path);
        }

        /// <summary>
        /// @brief 描画
        /// </summary>
        public override void Update()
        {
            var pos = Position.WorldPosition;
            DX.DrawRotaGraphF(pos.x, pos.y, (double)Scale, (double)Radian, GraphicHandle, 0);
        }
    }
}
