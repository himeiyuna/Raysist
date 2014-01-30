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
        /// @brief 弾と弾の間の角度（ラジアン指定）
        /// </summary>
        private float Theta
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public FanShot(GameContainer container, float angle, Vector3 pos, Player p)
            : base(container, angle, pos, p)
        {
            Angle = angle;

            Magazine = 9;
            Theta = Angle / Magazine;
        }
        /// <summary>
        /// @brief 偶数Wayの生成部分
        /// </summary>
        private void EvenShot()
        {
            for (int i = 1; i <= Magazine / 2.0f; ++i)
            {
                var sf = new ContainerFactory((GameContainer g) =>
                {
                    if (i == 1)
                    {
                        g.AddComponent(new Shot(g, -Angle + Theta / 2, Speed, Position.WorldPosition));
                    }
                    else
                    {
                        g.AddComponent(new Shot(g, -Angle + Theta * i - Theta / 2, Speed, Position.WorldPosition));
                    }
                    var b = new BillboardRenderer(g, "hisiga.png");
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



                var sf2 = new ContainerFactory((GameContainer g) =>
                {
                    if (i == 1)
                    {
                        g.AddComponent(new Shot(g, -Angle - Theta / 2, Speed, Position.WorldPosition));
                    }
                    else
                    {
                        g.AddComponent(new Shot(g, -Angle - Theta * i + Theta / 2, Speed, Position.WorldPosition));
                    }
                    var b = new BillboardRenderer(g, "hisiga.png");
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

                sf2.Create();
            }
        }

        /// <summary>
        /// @brief 奇数Wayの生成部分
        /// </summary>
        private void OddShot()
        {
            var vs = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Shot(g, -Angle, Speed, Position.WorldPosition));

                var b = new BillboardRenderer(g, "hisiga.png");
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

            vs.Create();

            for (int i = 1; i <= Magazine / 2.0f; ++i)
            {

                var sf = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g, -Angle + Theta * i, Speed, Position.WorldPosition));

                    var b = new BillboardRenderer(g, "hisiga.png");
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



                var sf2 = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g, -Angle - Theta * i, Speed, Position.WorldPosition));

                    var b = new BillboardRenderer(g, "hisiga.png");
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

                sf2.Create();
            }
        }

        /// <summary>
        /// @brief 垂直なショットを撃つ関数
        /// </summary>
        public override void Update()
        {
            //ショットを撃つ間隔
            if (CountDown())
            {
                Vector2 Direction = new Vector2();
                //下向きのベクトル
                Direction.x = 0.0f;
                Direction.y = -1.0f;

                //狙いを付けるなら
                if (AimFlag)
                {
                    Direction = Aim();
                }
                Angle = (float)Math.Atan2(Direction.y, Direction.x);
                //奇数か偶数か
                if (Magazine % 2 == 0)
                {
                    EvenShot();
                }
                else
                {
                    OddShot();
                }
            }
        }
    }
}
