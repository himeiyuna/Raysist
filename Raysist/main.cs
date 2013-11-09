using DxLibDLL;
using System;
using System.Collections.Generic;

namespace Raysist
{
    class Game
    {
        [STAThread]
        static void Main()
        {
            DX.ChangeWindowMode( DX.TRUE );
            DX.SetGraphMode(1600, 900, 32);

            if ( DX.DxLib_Init() == -1 )
            {
                return;
            }

            DX.SetDrawScreen(DX.DX_SCREEN_BACK);

            var cf = new ContainerFactory((GameContainer g) => 
            {
                g.AddComponent(new Player(g));
                //g.AddComponent(new SpriteRenderer(g, "dummy.png"));
                g.AddComponent(new MeshRenderer(g, "fighter.x"));
            });

            var cameraFactory = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Camera(g));
            });

            var gc = cf.Create();
            var camera = cameraFactory.Create();
            while (DX.ProcessMessage() == 0 && DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 0)
            {
                DX.ClearDrawScreen();

                gc.Update();
                camera.Update();

                DX.ScreenFlip();
            }

            DX.DxLib_End();

            return;
        }
    }
}