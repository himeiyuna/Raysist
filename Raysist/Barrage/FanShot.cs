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
        public float Theta
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
            Angle = (float)Math.PI * 0.5f;
            
            Magazine = 4;
            Theta = (float)Math.PI * 0.5f / Magazine;
            //if (Magazine % 2 == 0)
            //{
            //    Magazine /= 2;
            //}
            //else
            //{
            //    Magazine = Magazine / 2 + 1;
            //}
        }
        /// <summary>
        /// @brief ショットの生成部分
        /// </summary>
        private void Shot()
        {
            if (Magazine % 2 != 0)
            {
                var vs = new ContainerFactory((GameContainer g) =>
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

                vs.Create();
            }
            for (int i = 1; i <= Magazine/2.0f; ++i)
            {

                var sf = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g, -Angle + Theta * i, Speed, Position.WorldPosition));

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



                var sf2 = new ContainerFactory((GameContainer g) =>
                {
                    g.AddComponent(new Shot(g, -Angle - Theta * i, Speed, Position.WorldPosition));

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

                sf2.Create();
            }
        }
        /// <summary>
        /// @brief 垂直なショットを撃つ関数
        /// </summary>
        public override void Update()
        {
            if (CountDown())
            {
                //Direction.x = (float)Math.Cos(Angle * Math.PI / 180);
                //Direction.y = (float)Math.Sin(Angle * Math.PI / 180);

                //ラジアン指定
                //sin = (float)Math.Sin(angle - Theta * Magazine);
                //cos = (float)Math.Cos(angle + Theta * Magazine);

                //今向いている向きからθ分だけ回転させたベクトル
                //Direction.x = Direction.x * cos - Direction.y * sin;
                //Direction.y = Direction.x * sin + Direction.y * cos;

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
                Shot();
            }
        }
    }
}
