using DxLibDLL;
using System;

namespace Raysist
{
    class Game
    {
        [STAThread]
        static void Main()
        {
            DX.ChangeWindowMode( DX.TRUE );

            DX.SetGraphMode( 800, 600, 32 );

            if ( DX.DxLib_Init() == -1 )
            {
                return;
            }

            {
                DX.DrawBox(100, 100, 200, 150, DX.GetColor( 255, 0, 255 ), DX.TRUE);
                DX.DrawString( 100, 200, "サンプル描画", DX.GetColor( 255, 255, 64 ) );
            }
            Matrix mat = new Matrix();


            DX.WaitKey();

            DX.DxLib_End();

            return;
        }
    }
}