using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief コンテナを生成するファクトリクラス
    /// </summary>
    class ContainerFactory
    {
        /// <summary>
        /// @brief コンテナ初期化の追加処理関数
        /// </summary>
        protected Action<GameContainer> Option
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="createFunction"></param>
        public ContainerFactory(Action<GameContainer> option = null)
        {
            Option = option;
        }

        /// <summary>
        /// @brief コンテナを生成する
        /// </summary>
        /// <returns>生成されたコンテナ</returns>
        public virtual GameContainer Create()
        {
            var ret = new GameContainer();
            if (Option != null)
            {
                Option(ret);
            }
            return ret;
        }
    }
}
