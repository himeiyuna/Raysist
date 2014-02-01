using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// ゲームを構成するコンテナクラス
    /// </summary>
    public sealed class GameContainer
    {
        private interface ComponentIDBase
        {
            /// <summary>
            /// @brief ID
            /// </summary>
            string ID
            {
                set;
                get;
            }
        }

        /// <summary>
        /// @brief コンポーネントを識別するIDクラス
        /// </summary>
        /// <typeparam name="T">コンポーネント</typeparam>
        private class ComponentID<T> : ComponentIDBase where T : GameComponent
        {
            /// <summary>
            /// @copydoc ComponentIDBase::ID
            /// </summary>
            public string ID
            {
                set;
                get;
            }

            /// <summary>
            /// @brief コンストラクタ
            /// </summary>
            public ComponentID()
            {
                ID = GetType().ToString();
            }
        }

        /// <summary>
        /// @brief ゲームコンポーネントの配列
        /// </summary>
        private Dictionary<string, List<GameComponent>> Components
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 更新フラグ
        /// </summary>
        public bool Active
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 親がアクティブでなかった場合falseを返す
        /// </summary>
        public bool IsActiveInHierarchy
        {
            get
            {
                if (Active)
                {
                    return Position.Parent != null ? Position.Parent.Container.IsActiveInHierarchy : true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// @brief 名前
        /// </summary>
        public String Name
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
            Active = true;
            Position = new Positioner(this);
            Position.Parent = Game.Instance.SceneController.CurrentScene.Root.Position;
            Components = new Dictionary<string, List<GameComponent>>();
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="parent">親</param>
        public GameContainer(GameContainer parent)
        {
            Active = true;
            Position = new Positioner(this);
            Position.Parent = parent.Position;
            Components = new Dictionary<string, List<GameComponent>>();
        }

        /// <summary>
        /// @brief ルート要素作成用コンストラクタ
        /// </summary>
        /// <param name="scene"></param>
        internal GameContainer(Scene scene)
        {
            Active = true;
            Position = new Positioner(this);
            Components = new Dictionary<string, List<GameComponent>>();
        }

        /// <summary>
        /// @brief コンポーネントを追加する
        /// </summary>
        /// <param name="component">追加するコンポーネント</param>
        public void AddComponent<T>(T component) where T : GameComponent
        {
            var id = new ComponentID<T>();

            // キーが存在しなければ
            if (!Components.ContainsKey(id.ID)) 
            {
                // リストを作成
                var list = new List<GameComponent>();
                list.Add(component);

                Components.Add(id.ID, list);
            } 
            else 
            {
                Components[id.ID].Add(component);
            }
            
        }

        /// <summary>
        /// @brief コンポーネントを検索する
        /// </summary>
        /// <typeparam name="T">コンポーネントの型</typeparam>
        /// <returns>コンポーネント</returns>
        public T GetComponent<T>() where T : GameComponent
        {
            var id = new ComponentID<T>();
            if (!Components.ContainsKey(id.ID))
            {
                return null;
            }
            else
            {
                return (T)Components[id.ID].First();
            }
        }

        /// <summary>
        /// @brief コンポーネントを削除する
        /// </summary>
        /// <typeparam name="T">コンポーネント型</typeparam>
        public void RemoveComponent<T>() where T : GameComponent
        {
            foreach (var list in Components)
            {
                foreach (var com in list.Value)
                {
                    if (com is T)
                    {
                        Components[new ComponentID<T>().ID].Clear();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public void Update()
        {
            foreach (var list in Components)
            {
                foreach (var com in list.Value)
                {
                    if (com.Active)
                    {
                        com.Update();
                    }
                }
            }
        }

        /// <summary>
        /// @brief コンテナの破棄
        /// </summary>
        /// <param name="container">破棄する対象</param>
        public static void Destroy(GameContainer container)
        {
            container.Position.Parent.RemoveChild(container.Position);
        }
    }

    /// <summary>
    /// @brief シーンクラス 
    ///        画面を構成するクラス
    /// </summary>
    abstract class Scene
    {
        /// <summary>
        /// @brief 根要素を取得するプロパティ
        /// </summary>
        public GameContainer Root
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief 衝突判定管理
        /// </summary>
        public CollisionManager CollisionManager
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        protected Scene()
        {
            Root = new GameContainer(this);
            CollisionManager = new CollisionManager();
        }

        /// <summary>
        /// @brief リソースの読み込み処理
        /// </summary>
        public abstract void LoadResource();

        /// <summary>
        /// @brief 読み込み完了後に呼び出される
        /// </summary>
        public abstract void EnterScene();

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public virtual void Update()
        {
            CollisionManager.Update();

            var stack = new Stack<Positioner>();
            stack.Push(Root.Position);
            while (stack.Count != 0) 
            {
                // 要素をひとつ取り出す
                var q = stack.Pop();

                // アクティブでなければ子も処理しない
                if (q.Container.Active)
                {
                    // 更新処理
                    q.Container.Update();

                    // 子をキューに追加する
                    foreach (var child in q.GetIterator())
                    {
                        stack.Push(child);
                    }
                }
            }
        }

        /// <summary>
        /// @brief シーン解放前に呼び出される
        /// </summary>
        public abstract void LeaveScene(); 

        /// <summary>
        /// @brief リソースの解放処理
        /// </summary>
        public abstract void UnloadResource();
    }

    /// <summary>
    /// @brief ロード用のシーン
    /// </summary>
    class LoadScene : Scene
    {
        /// <summary>
        /// @brief 読み込むシーン
        /// </summary>
        internal Scene Next
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 読み込み完了かどうかを取得する
        /// </summary>
        public bool IsLoadFinished
        {
            get
            {
                return DX.GetASyncLoadNum() == 0;
            }
        }

        private SpriteRenderer Sprite
        {
            set;
            get;
        }

        /// <summary>
        /// @brief リソースの読み込み
        /// </summary>
        public override void LoadResource()
        {
            ResourceController.Instance.LoadGraphic("loading.png");
        }

        /// <summary>
        /// @brief 初期化処理
        /// </summary>
        public override void EnterScene()
        {
            // 非同期読み込み開始
            DX.SetUseASyncLoadFlag(DX.TRUE);

            // 読み込み
            Next.LoadResource();

            var gc = new GameContainer();
            gc.Position.LocalPosition = new Vector3 { x = 1500.0f, y = 750.0f, z = 0.0f };
            Sprite = new SpriteRenderer(gc, "loading.png");
            gc.AddComponent(Sprite);
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            base.Update();

            Sprite.Radian += (float)Math.PI / 60.0f;
        }

        /// <summary>
        /// @brief 終了処理
        /// </summary>
        public override void LeaveScene()
        {
            // 非同期読み込み終了
            DX.SetUseASyncLoadFlag(DX.FALSE);
        }

        /// <summary>
        /// @brief リソースの後処理
        /// </summary>
        public override void UnloadResource()
        {
            
        }
    }

    /// <summary>
    /// @brief シーンを管理するクラス
    /// </summary>
    sealed class SceneController
    {
        /// <summary>
        /// @brief 現在のシーン
        /// </summary>
        public Scene CurrentScene
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief 新しいシーン
        /// </summary>
        private LoadScene LoadingScene
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
            // シーン更新
            CurrentScene.Update();

            // 新しいシーンが無ければそのまま更新処理をかける
            if (LoadingScene != null)
            {
                // ロード中処理
                LoadingScene.Update();

                // ロードが終了していたら
                if (LoadingScene.IsLoadFinished)
                {
                    // 現在のシーンの後処理
                    CurrentScene.LeaveScene();
                    CurrentScene.UnloadResource();

                    // ローディングシーンの後処理
                    LoadingScene.LeaveScene();
                    LoadingScene.UnloadResource();

                    // 遷移
                    CurrentScene = LoadingScene.Next;
                    CurrentScene.EnterScene();

                    LoadingScene = null;

                    // ガベージコレクション起動
                    System.GC.Collect();
                }
            }
        }

        /// <summary>
        /// @brief シーンを変更する 即座には切り替わらない
        /// </summary>
        /// <typeparam name="T">読み込むシーン</typeparam>
        /// <typeparam name="L">ロード用のシーン</typeparam>
        public void ChangeScene<T, L>() where T : Scene, new() where L : LoadScene, new()
        {
            LoadingScene = new L();
            LoadingScene.Next = new T();
            LoadingScene.LoadResource();
            LoadingScene.EnterScene();
        }
    }
}
