using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief タイトル画面
    /// </summary>
    class TitleScene : Scene
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public TitleScene()
            : base()
        {
        }

        /// <summary>
        /// @brief リソースをロードする
        /// </summary>
        public override void LoadResource()
        {
        }

        /// <summary>
        /// @brief シーン再生直前に呼び出される初期化処理
        /// </summary>
        public override void EnterScene()
        {
            CollisionManager.Initialize(8, -10.0f, -10.0f, 1610.0f, 910.0f);

            var cf = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new SpriteRenderer(g, "dummy.png"));
                
            });

            var cameraFactory = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Camera(g));

            });
        }

        /// <summary>
        /// @brief 終了処理
        /// </summary>
        public override void LeaveScene()
        {
        }

        /// <summary>
        /// @brief リソースを開放する
        /// </summary>
        public override void UnloadResource()
        {
        }







    }
}
