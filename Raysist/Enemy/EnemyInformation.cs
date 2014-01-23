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
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public EnemyInformation(GameContainer container, int life) : base(container)
        {
            Life = life;    
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public override void Update()
        {
            if (Life <= 0)
            {
                GameContainer.Destroy(Container);

                var gc = new GameContainer();
                gc.Position.LocalPosition = Position.LocalPosition;
                var spriteRenderer = new BillboardRenderer(gc, "explosion.png");
                spriteRenderer.Scale = 64.0f;
                var animator = new Animator(gc, spriteRenderer, "explosion.png", 4, 4, 1, 512, 512, true);
                animator.UpdateFrame = 3;
                gc.AddComponent(spriteRenderer);
                gc.AddComponent(animator);
                
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
