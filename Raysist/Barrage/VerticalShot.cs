using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
/// <summary>
/// @brief 垂直に弾撃つクラス
/// </summary>
namespace Raysist
{
    class VerticalShot : Barrage
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public VerticalShot(GameContainer container, float angle, Vector3 pos)
            : base(container, angle, pos)
        {
        }
        /// <summary>
        /// @brief 垂直なショットを撃つ関数
        /// </summary>
        public override void Update()
        {
            var sf = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Shot(g, (float)Math.PI * 0.5f, -Speed, Position.WorldPosition));

                var b = new BillboardRenderer(g, "dummy.png");
                b.Scale = 5.0f;
                g.AddComponent(b);

                var col = new RectCollider(g, (Collider c) =>
                {
                    var target = c.Container.GetComponent<EnemyInformation>();
                    if (target != null)
                    {
                        GameContainer.Destroy(g);
                    }
                });
                col.Width = 10.0f;
                col.Height = 10.0f;

                g.AddComponent(col);
            });

            sf.Create();
        }
    }
}
