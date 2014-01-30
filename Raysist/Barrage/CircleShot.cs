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
        /// @brief 弾間距離
        /// </summary>
        public float space
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public CircleShot(GameContainer container, float angle, Vector3 pos, Player p)
            : base(container, angle, pos, p)
        {
            Magazine = 18;
            space = (float)Math.PI / Magazine;
        }

        /// <summary>
        /// @brief 更新関数
        /// </summary>
        public override void Update()
        {
            if (CountDown())
            {
                for (int i = 0; i < Magazine; ++i)
                {
                    var sf = new ContainerFactory((GameContainer g) =>
                    {
                        g.AddComponent(new Shot(g, i * space, Speed, Position.WorldPosition));

                        var b = new BillboardRenderer(g, "弾.png");
                        b.Scale = 15.0f;
                        g.AddComponent(b);

                        var col = new RectCollider(g, (Collider c) =>
                        {
                            var target = c.Container.GetComponent<Player>();
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
}
