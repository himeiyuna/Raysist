using DxLibDLL;
using System;
using System.Collections.Generic;

namespace Raysist
{
    class Raysist
    {
        [STAThread]
        static void Main()
        {
            Game.Instance.Run<TitleScene>();

            return;
        }
    }
}