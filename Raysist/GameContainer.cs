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
    public abstract class GameContainer 
    {
        private static readonly Positioner Root = new Positioner();

        /// <summary>
        /// @brief ゲームコンポーネントの配列
        /// </summary>
        private List<GameComponent> Components { set; get; }

        /// <summary>
        /// @brief 位置情報を取得するプロパティ
        /// </summary>
        public Positioner Position { set; get; }

        /// <summary>
        /// @brief デフォルトコンストラクタ
        /// </summary>
        public GameContainer()
        {
            Position = new Positioner();
            Components = new List<GameComponent>();

            Position.Parent = Root;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public GameContainer(GameContainer parent)
        {
            Position = new Positioner();
            Components = new List<GameComponent>();

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
        /// @brief 更新
        /// </summary>
        public virtual void Update()
        {
            foreach (var child in Components)
            {
                child.Update();
            }
        }
    }
}
