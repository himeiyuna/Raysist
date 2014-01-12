using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @breif 敵コンポーネント
    /// </summary>
    class Enemy : GameComponent
    {
        /// <summary>
        /// @breif HP
        /// </summary>
        private int Life
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public Enemy(GameContainer container, Vector3 pos) : base(container)
        {
            Life = 10;
            Position.LocalPosition = pos;
            Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f);

            var collider = new RectCollider(container, (Collider c) => 
            {
                var target = c.Container.GetComponent<Shot>();
                if (target != null)
                {
                    Life -= 1;
                }
            });
            collider.Width = 10;
            collider.Height = 10;
            container.AddComponent(collider);
            container.AddComponent(new MeshRenderer(container, "fighter.x"));
            container.AddComponent(new BehindAppearEnemy(container, Position.LocalPosition, new Vector3() { x = 0.0f, y = 250.0f, z = 0.0f }));
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public override void Update()
        {
            if (Life <= 0)
            {
                GameContainer.Destroy(Container);
            }

            //Position.LocalPosition.y -= 1.0f;
        }
    }
}
