using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief シーンクラス 
    ///        画面を構成するクラス
    /// </summary>
    abstract class Scene : IDisposable
    {
        /// <summary>
        /// @brief 根要素
        /// </summary>
        private GameContainer root;

        /// <summary>
        /// @brief 根要素を取得するプロパティ
        /// </summary>
        public GameContainer Root
        {
            private set
            {
                root = value;
            }
            get
            {
                return root;
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        protected Scene()
        {
            root = new GameContainer();
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public virtual void Update()
        {
            var queue = new Queue<Positioner>();
            queue.Enqueue(Root.Position);
            while (queue.Count != 0) 
            {
                // 要素をひとつ取り出す
                var q = queue.Dequeue();

                // アクティブでなければ子も処理しない
                if (q.Container.IsActive)
                {
                    // 更新処理
                    q.Container.Update();

                    // 子をキューに追加する
                    foreach (var child in q.GetIterator())
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }

        /// <summary>
        /// @brief 解放処理
        /// </summary>
        public abstract void Dispose();
    }

    /// <summary>
    /// @brief シーンを管理するクラス
    /// </summary>
    sealed class SceneController
    {
        /// <summary>
        /// @brief 現在のシーン
        /// </summary>
        private Scene CurrentScene
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 新しいシーン
        /// </summary>
        private Scene NewScene
        {
            set;
            get;
        }

        /// <summary>
        /// @brief シーン遷移用のスタック
        /// </summary>
        private Stack<Scene> SceneStack
        {
            set;
            get;
        }

       
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="firstScene">最初のシーン</param>
        public SceneController(Scene firstScene)
        {
            CurrentScene = firstScene;
            SceneStack = new Stack<Scene>();
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public void Update()
        {
            // 新しいシーンが無ければそのまま更新処理をかける
            if (NewScene != null)
            {
                CurrentScene.Dispose();
                CurrentScene = NewScene;
                NewScene = null;
            }
               
            // シーン更新
            CurrentScene.Update();
        }
    }
}
