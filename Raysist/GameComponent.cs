using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief ゲームを構成する部品クラス
    /// </summary>
    public abstract class GameComponent
    {
        /// <summary>
        /// @brief位置情報
        /// </summary>
        public Positioner Position { set; get; }

        /// <summary>
        /// @brief コンポーネントが受け持つ作業の実行
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">所持する親</param>
        public GameComponent(GameContainer container)
        {
            Position = container.Position;
        }
    }
}
