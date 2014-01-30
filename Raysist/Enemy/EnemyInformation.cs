using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @breif 敵基本情報コンポーネント
    /// </summary>
    class EnemyInformation : GameComponent
    {
        /// <summary>
        /// @breif HP
        /// </summary>
        public int Life
        {
            protected set;
            get;
        }

        /// <summary>
        /// @breif ショットの間隔
        /// </summary>
        public int Count
        {
            protected set;
            get;
        }

        /// <summary>
        /// @breif ショットの速さ
        /// </summary>
        public float Speed
        {
            protected set;
            get;
        }

        /// <summary>
        /// @breif スコアへのポインタ
        /// </summary>
        public ScoreComponent SC
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public EnemyInformation(GameContainer container, int life, ScoreComponent sc) : base(container)
        {
            Life = life;
            Count = 15;
            Speed = 5.0f;
            SC = sc;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public override void Update()
        {
            --Count;
            if (Count < 0)
            {
                Count = 15;
            }

            if (Position.LocalPosition.x < -1600 || Position.LocalPosition.x > 1600 ||
                Position.LocalPosition.y < -1600 || Position.LocalPosition.y > 1600)
            {
                // TODO:外に出たら破棄する
                GameContainer.Destroy(Container);
            }
            if (Life <= 0)
            {
                GameContainer.Destroy(Container);

                var gc = new GameContainer(Game.Instance.SceneController.CurrentScene.Root.Position.FindChild("Explosion").Container);
                gc.Position.LocalPosition = Position.LocalPosition;
                var spriteRenderer = new BillboardRenderer(gc);
                spriteRenderer.Scale = 64.0f;
                var animator = new Animator(gc, spriteRenderer, "explosion.png", 4, 4, 1, 512, 512, true);
                animator.UpdateFrame = 3;
                gc.AddComponent(spriteRenderer);
                gc.AddComponent(animator);

                SC.Score += 100;

                animator.OnFinishAnimation = (Animator a) =>
                {
                    GameContainer.Destroy(a.Container);
                };
      

            }
        }

        /// <summary>
        /// @brief ダメージを与える関数
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        public void Damage(int damage)
        {
            Life -= damage;
        }
    }
}
