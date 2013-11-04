using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief コンテナを生成するファクトリクラス
    ///        特化したファクトリを作成する場合はCreate関数をオーバーライドすること
    ///        匿名関数を使用することでも特化させることは可能
    /// </summary>
    class ContainerFactory
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public ContainerFactory()
        {
        }

        /// <summary>
        /// @brief コンテナを生成する
        /// </summary>
        /// <returns>生成されたコンテナ</returns>
        public virtual GameContainer Create()
        {
            return new GameContainer();
        }

        /// <summary>
        /// @brief 親が設定されたコンテナを生成する
        /// </summary>
        /// <param name="parent">親</param>
        /// <returns>生成されたコンテナ</returns>
        public virtual GameContainer Create(GameContainer parent)
        {
            return new GameContainer(parent);
        }

        /// <summary>
        /// @brief コンテナを生成する　
        ///        匿名関数によるコンポーネント初期化
        /// </summary>
        /// <param name="func">実装するコンポーネントのリストを返す関数</param>
        /// <returns>生成されたコンテナ</returns>
        public GameContainer Create(Func<GameContainer, List<GameComponent>> func)
        {
            var ret = new GameContainer();
            
            var components = func(ret);
            foreach (var component in components)
            {
                ret.AddComponent(component);
            }

            return ret;
        }

        /// <summary>
        /// @brief 親が設定されたコンテナを生成する
        ///        匿名関数によるコンポーネント初期化
        /// </summary>
        /// <param name="func">実装するコンポーネントのリストを返す関数</param>
        /// <param name="parent">親</param>
        /// <returns>生成されたコンテナ</returns>
        public GameContainer Create(Func<GameContainer, List<GameComponent>> func, GameContainer parent)
        {
            var ret = new GameContainer(parent);

            var components = func(ret);
            foreach (var component in components)
            {
                ret.AddComponent(component);
            }

            return ret;
        }
    }
}
