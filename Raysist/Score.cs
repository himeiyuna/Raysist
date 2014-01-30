using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief スコアコンポーネント
    /// </summary>
    class ScoreComponent : GameComponent
    {
        /// <summary>
        /// @brief 数字ひとつを表示するコンポーネント
        /// </summary>
        private class Number : GameComponent
        {
            /// <summary>
            /// @brief 数
            /// </summary>
            public int Num
            {
                set;
                get;
            }

            private int[] GraphicHandles
            {
                set;
                get;
            }

            /// <summary>
            /// @brief コンストラクタ
            /// </summary>
            /// <param name="container">自身を組み込むコンテナ</param>
            public Number(GameContainer container) : base(container)
            {
                GraphicHandles = ResourceController.Instance.LoadDivideGraphic("number.png", 10, 32, 32);
            }

            public override void Update()
            {
                var pos = Position.LocalPosition;
                DX.DrawGraphF(pos.x, pos.y, GraphicHandles[Num], DX.TRUE);
            }
        }

        /// <summary>
        /// @brief スコア
        /// </summary>
        private int score;
        public int Score
        {
            set
            {
                score = value;
                var digit = Scores.Length - 1;
                for (var i = score; i > 0; i /= 10)
                {
                    Scores[digit--].Num = i % 10;
                }
            }
            get
            {
                return score;
            }
        }

        /// <summary>
        /// @brief 数字の配列
        /// </summary>
        private Number[] Scores
        {
            set;
            get;
        }


        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public ScoreComponent(GameContainer container) : base(container)
        {
            Scores = new Number[10];

            for (var i = 0; i < Scores.Length; ++i)
            {
                var gc = new GameContainer(container);
                Scores[i] = new Number(gc);
                gc.AddComponent(Scores[i]);
                gc.Position.LocalPosition = new Vector3 { x = i * 32.0f, y = 0.0f, z = 0.0f };
            }

            Score = 1000;
        }


        public override void Update()
        {
            
        }
    }


}
