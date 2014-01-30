using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief タイトル画面
    /// </summary>
    class ResultScene : Scene
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public ResultScene()
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
            //CollisionManager.Initialize(8, -10.0f, -10.0f, 1610.0f, 910.0f);

            var cf = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new SpriteRenderer(g, "title.png"));
                g.Position.LocalPosition.x = 800;
                g.Position.LocalPosition.y = 450;
            });

            var cameraFactory = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Camera(g));

            });

            var gc = cf.Create();

            var camera = cameraFactory.Create().GetComponent<Camera>();
            camera.Position.LocalPosition = new Vector3 { x = 0.0f, y = 0.0f, z = -3000.0f };
            camera.FieldOfView = (float)Math.PI * 0.1f;

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

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (DX.CheckHitKey(DX.KEY_INPUT_SPACE) == 1)
            {
                Game.Instance.SceneController.ChangeScene<EndScene, LoadScene>();
            }
        }
    }
}
