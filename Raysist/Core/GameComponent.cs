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
        /// @brief 位置情報
        /// </summary>
        public Positioner Position 
        {
            private set; 
            get;
        }

        /// <summary>
        /// @brief 自身を組み込んでいるコンテナ
        /// </summary>
        public GameContainer Container 
        {
            get
            {
                return Position.Container;
            }
        }

        /// <summary>
        /// @brief trueならUpdateを実行する
        /// </summary>
        private bool active;
        public bool Active
        {
            set
            {
                if (active != value)
                {
                    active = value;
                    if (active)
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
                }
                
            }
            get
            {
                return active;
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">所持する親</param>
        public GameComponent(GameContainer container, bool active = true)
        {
            this.active = active;
            Position = container.Position;
        }

        /// <summary>
        /// @brief コンポーネントが受け持つ作業の実行
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// @brief コンポーネントが無効から有効に変換された際に呼び出される
        /// </summary>
        public virtual void OnEnable()
        {

        }

        /// <summary>
        /// @brief コンポーネントが有効から無効に変換された際に呼び出される
        /// </summary>
        public virtual void OnDisable()
        {

        }
    }
}
