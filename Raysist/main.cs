﻿using DxLibDLL;
using System;

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

            GameContainer gc = new Player();

            while (DX.ProcessMessage() == 0 && DX.CheckHitKey(DX.KEY_INPUT_ESCAPE) == 0)
            {
                DX.ClearDrawScreen();

                gc.Update();

                DX.ScreenFlip();
            }

            DX.DxLib_End();

            return;
        }
    }
}