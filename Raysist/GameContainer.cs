using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// ゲームを構成するコンテナクラス
    /// </summary>
    public sealed class GameContainer 
    {
        /// <summary>
        /// @brief ゲームコンポーネントの配列
        /// </summary>
        private List<GameComponent> Components 
        {
            set; 
            get;
        }

        /// <summary>
        /// @brief 位置情報を取得するプロパティ
        /// </summary>
        public Positioner Position 
        { 
            private set; 
            get;
        }

        /// <summary>
        /// @brief デフォルトコンストラクタ
        /// </summary>
        public GameContainer()
        {
            Position = new Positioner();
            Components = new List<GameComponent>();
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public GameContainer(GameContainer parent) : this()
        {
            Position.Parent = parent.Position;
        }

        /// <summary>
        /// @brief コンポーネントを追加する
        /// </summary>
        /// <param name="component">追加するコンポーネント</param>
        public void AddComponent(GameComponent component)
        {
            Components.Add(component);
        }

        /// <summary>
        /// @brief コンポーネントを検索する
        /// </summary>
        /// <typeparam name="T">コンポーネントの型</typeparam>
        /// <returns>コンポーネント</returns>
        public T GetComponent<T>() where T : GameComponent
        {
            return (from i in Components where i is T select i).FirstOrDefault() as T;
        }

        /// <summary>
        /// @brief コンポーネントを削除する
        /// </summary>
        /// <typeparam name="T">コンポーネント型</typeparam>
        public void RemoveComponent<T>() where T : GameComponent
        {
            var i = 0;
            foreach (var com in Components)
            {
                if (com is T)
                {
                    Components.RemoveAt(i);
                }
                ++i;
            }
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        internal void Update()
        {
            foreach (var child in Components)
            {
                child.Update();
            }
        }
    }

    /// <summary>
    /// @brief ゲームコンテナを管理するクラス
    /// </summary>
    public sealed class GameContainerController
    {
        /// <summary>
        /// @brief コンテナの配列
        /// </summary>
        private List<GameContainer> Containers
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public GameContainerController()
        {
            Containers = new List<GameContainer>();
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public void Update()
        {
            foreach (var container in Containers)
            {
                container.Update();
            }
        }
    }
}
