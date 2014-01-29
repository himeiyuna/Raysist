using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
/// <summary>
/// @brief 円形の弾撃つクラス
/// </summary>
namespace Raysist
{
    class CircleShot : Barrage
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public CircleShot(GameContainer container, float angle, Vector3 pos, Player p)
            : base(container, angle, pos, p)
        {
        }

        /// <summary>
        /// @brief 更新関数
        /// </summary>
        public override void Update()
        {
            for (int i = 0; i < Magazine; i++)
            {
                var sf = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g, i * SpaceanInterval, Speed, Position.WorldPosition));

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
}
