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
        public FanShot(GameContainer container, float angle, Vector3 pos)
            : base(container, angle, pos)
        {
            Theta = (float)Math.PI * 0.05f;
            Magazine = 10;
        }
        /// <summary>
        /// @brief 垂直なショットを撃つ関数
        /// </summary>
        public override void Update()
        {
            if (CountDown())
            {
                float sin, cos, atan;
                Vector2 Direction = new Vector2();
                //下向きのベクトル
                Direction.x = 0.0f;
                Direction.y = -1.0f;
                //ラジアン指定
                sin = (float)Math.Sin(Math.PI - Theta * Magazine);
                cos = (float)Math.Cos(Math.PI - Theta * Magazine);
                //狙いを付けるなら
                if (AimFlag)
                {
                    Aim();
                }
                //x = Math.Cos(rot * Math.PI / 180);
                //y = Math.Sin(rot * Math.PI / 180);

                

                //今向いている向きからθ分だけ回転させたベクトル
                Direction.x = Direction.x * cos - Direction.y * sin;
                Direction.y = Direction.x * sin + Direction.y * cos;
                atan = (float)Math.Atan2(Direction.y, Direction.x);
                for (int i = 1; i < Magazine+1; ++i)
                {
                    var sf = new ContainerFactory((GameContainer g) =>
                    {
                        //if (i / 2 == 0)
                        //{
                            g.AddComponent(new Shot(g, atan + i * Theta, Speed, Position.WorldPosition));
                        //}
                        //else
                        //{
                        //    g.AddComponent(new Shot(g, atan + (i-1) * -Theta, Speed, Position.WorldPosition));
                        //}
                        

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
}
