using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class Game
    {
        /// <summary>
        /// @brief シングルトンインスタンス
        /// </summary>
        private static Game instance;
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }

        /// <summary>
        /// @brief シーン管理クラス
        /// </summary>
        public SceneController SceneController
        {
            private set;
            get;
        }

        public InputController InputController
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief ゲーム起動
        /// </summary>
        /// <typeparam name="FirstScene">最初のシーン</typeparam>
        public void Run<FirstScene>() where FirstScene : Scene, new()
        {
            DX.ChangeWindowMode(DX.TRUE);
            //DX.SetBackgroundColor(0, 0, 255);
            DX.SetGraphMode(1600, 900, 32);
            //DX.SetFullSceneAntiAliasingMode(4, 2);

            if (DX.DxLib_Init() == -1)
            {
                return;
            }

            this.SceneController = new SceneController(new FirstScene());
            this.SceneController.CurrentScene.LoadResource();
            this.SceneController.CurrentScene.EnterScene();

            InputController = new InputController();

            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.SetUseZBufferFlag(DX.TRUE);

            while (DX.ProcessMessage() == 0 && DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 0)
            {
                DX.ClearDrawScreen();

                InputController.Update();

                this.SceneController.Update();

                DX.ScreenFlip();
            }

            System.GC.Collect();

            DX.DxLib_End();
        }
    }
}
