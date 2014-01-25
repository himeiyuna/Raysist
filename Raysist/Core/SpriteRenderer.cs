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
        internal protected int GraphicHandle 
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
        /// @brief コンストラクタ グラフィックを生成しない
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public SpriteRenderer(GameContainer container) : base(container)
        {
            Scale = 1.0f;
            Radian = 0.0f;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        /// <param name="path">ファイルの場所</param>
        public SpriteRenderer(GameContainer container, string path) : this(container)
        {
            GraphicHandle = ResourceController.Instance.LoadGraphic(path);
        }

        

        /// <summary>
        /// @brief 描画
        /// </summary>
        public override void Update()
        {
            DX.SetWriteZBufferFlag(DX.FALSE);
            var pos = Position.WorldPosition;
            DX.DrawRotaGraphF((int)pos.x, (int)pos.y, (double)Scale, (double)Radian, GraphicHandle, DX.TRUE);
        }
    }

    /// <summary>
    /// @brief 2D画像をアニメーションさせるコンポーネント
    /// </summary>
    class Animator : GameComponent
    {
        /// <summary>
        /// @brief アニメーションさせる対象
        /// </summary>
        private SpriteRenderer Target
        {
            set;
            get;
        }

        /// <summary>
        /// @brief グラフィックハンドルの配列
        /// </summary>
        private int[] GraphicHandles
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 現在のアニメーション画像インデックス
        /// </summary>
        private int CurrentImageIndex
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 現在のフレームカウント
        /// </summary>
        private int CurrentFrameCount
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 更新するフレーム数
        /// </summary>
        public int UpdateFrame
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ループフラグ
        /// </summary>
        public bool Loop
        {
            set;
            get;
        }

        /// <summary>
        /// @brief アニメーション終了時に呼び出される
        /// </summary>
        public Action<Animator> OnFinishAnimation
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込む親</param>
        /// <param name="target">アニメーション対象</param>
        public Animator(GameContainer container, SpriteRenderer target, string path, int num, int col, int row, int width, int height, bool isLoop = true)
            : base(container)
        {
            Target = target;

            GraphicHandles = ResourceController.Instance.LoadDivideGraphic(path, num, width, height);

            Target.GraphicHandle = GraphicHandles[0];
            CurrentImageIndex = 0;
            CurrentFrameCount = 0;
            UpdateFrame = 1;
            Loop = true;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            // 更新フレーム数を超えたら
            if (++CurrentFrameCount > UpdateFrame)
            {
                CurrentFrameCount = 0;

                // 表示画像を切り替えるが、次の画像がなければ最初に戻す
                if (++CurrentImageIndex == GraphicHandles.Length)
                {
                    if (OnFinishAnimation != null)
                    {
                        OnFinishAnimation(this);
                    }

                    if (!Loop) 
                    {
                        Active = false;
                        Target.Active = false;

                    }
                    CurrentImageIndex = 0;
                }

                Target.GraphicHandle = GraphicHandles[CurrentImageIndex];
            }
        }
    }
}
