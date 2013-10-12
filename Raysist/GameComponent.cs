using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    public abstract class GameComponent
    {
        /// <summary>
        /// 座標
        /// </summary>
        private Positioner Position { set; get; }

        /// <summary>
        /// コンポーネントが受け持つ作業の実行
        /// </summary>
        public abstract void Update();
    }
}
