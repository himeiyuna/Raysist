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
        public VerticalShot(GameContainer container, float angle, Vector3 pos, Player p)
            : base(container, angle, pos, p)
        {
            Angle = -(float)Math.PI * 0.5f;
        }
        /// <summary>
        /// @brief 垂直なショットを撃つ関数
        /// </summary>
        public override void Update()
        {
            if (CountDown())
            {
                if (AimFlag)
                {
                    Vector2 Direction = new Vector2();
                    Direction = Aim();
                    Angle = (float)(Math.Atan2(Direction.y, Direction.x));
                }
                var sf = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g, -Angle, Speed, Position.WorldPosition));
                    
                    var b = new BillboardRenderer(g, "dummy.png");
                    b.Scale = 5.0f;
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
