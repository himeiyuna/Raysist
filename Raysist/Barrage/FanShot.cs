using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
/// <summary>
/// @brief 扇状の弾撃つクラス
/// </summary>
namespace Raysist
{
    class FanShot : Barrage
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public FanShot(GameContainer container, float angle, Vector3 pos)
            : base(container, angle, pos)
        {
        }
        /// <summary>
        /// @brief 垂直なショットを撃つ関数
        /// </summary>
        public override void Update()
        {
            Vector2 Direction = new Vector2();
            Direction.x = Position.WorldPosition.x;
            Direction.y = Position.WorldPosition.y;
            float sin, cos, atan;


            sin = (float)Math.Sin(90);
            cos = (float)Math.Cos(90);

            Direction.x = Direction.x * cos - Direction.y * sin;
            Direction.y = Direction.x * sin + Direction.y * cos;
            atan = (float)Math.Atan2(Direction.y, Direction.x);
            for (int i = 0; i < Magazine; i++)
            {
                var sf = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g,
                        //((float)Math.PI * 0.5f)
                        atan
                        , Speed, Position.WorldPosition));

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
