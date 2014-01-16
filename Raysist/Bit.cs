using DxLibDLL;
using System;
using System.Runtime.InteropServices;

namespace Raysist
{
    class Bit : GameComponent
    {
        private const int MaxEnergy = 120;

        /// <summary>
        /// @brief ビット番号
        /// </summary>
        public enum BitIndex
        {
            BIT_LEFT,
            BIT_RIGHT
        }

        /// <summary>
        /// @brief 親
        /// </summary>
        private Player Player
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ビット番号(0,1)
        /// </summary>
        private BitIndex Index
        {
            get;
            set;
        }

        /// <summary>
        /// @brief レーザーの親
        /// </summary>
        private GameContainer Lazer
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ショットの間隔
        /// </summary>
        private const int ShotInterval = 10;

        /// <summary>
        /// @brief ショットのカウンタ
        /// </summary>
        private int ShotCounter
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 回転量
        /// </summary>
        private int Rot
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 向いている方向
        /// </summary>
        public float Angle
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief 移動速度
        /// </summary>
        private float Speed
        {
            set;
            get;
        }

        /// <summary>
        /// @brief エネルギー 0になると自機に戻る
        /// </summary>
        public int Energy
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 切り離されていればfalse
        /// </summary>
        public bool IsDock
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="player">親</param>
        /// <param name="position">初期位置</param>
        public Bit(GameContainer container, Player player, BitIndex index, GameContainer lazer)
            : base(container)
        {
            Energy = MaxEnergy;      // 2秒分
            Player = player;
            Index = index;
            ShotCounter = 0;
            Speed = 5.0f;

            Rot = 90;
            Angle = Rot * -(float)Math.PI / 180;
            Lazer = lazer;

            IsDock = true;


            if (index == BitIndex.BIT_LEFT)
            {
                Position.LocalPosition = new Vector3 { x = -10.0f, y = -5.0f, z = 0.0f };
                container.Name = "BitLeft";
                //LRFlag = true;
            }
            else
            {
                Position.LocalPosition = new Vector3 { x = 10.0f, y = -5.0f, z = 0.0f };
                container.Name = "BitRight";
                //LRFlag = true;
            }
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            // 自機についているときはエネルギー回復
            if (IsDock)
            {
                ++Energy;
                if (MaxEnergy < Energy)
                {
                    Energy = MaxEnergy;
                }

                ++ShotCounter;
                if (ShotCounter > ShotInterval)
                {
                    ShotCounter = 0;

                    // ショットを放つ
                    var sf = new ContainerFactory((GameContainer g) =>
                    {
                        g.AddComponent(new Shot(g, (float)Math.PI * -0.5f, Position.WorldPosition));

                        var b = new BillboardRenderer(g, "dummy.png");
                        b.Scale = 5.0f;
                        g.AddComponent(b);

                        var col = new RectCollider(g, (Collider c) =>
                        {
                            var target = c.Container.GetComponent<Enemy>();
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
            else
            {
                --Energy;
                //--Energy;
                Move();
                Lazer.GetComponent<Raypier>().Active = true;

                // 0になれば
                if (Energy <= 0)
                {
                    Dock();
                }
            }
        }

        /// <summary>
        /// @brief 自機に戻る
        /// </summary>
        public void Dock()
        {
            // TODO:徐々に自機に近づく処理をする
            var a = Position.Parent.LocalPosition - Position.LocalPosition;
            var b = a.Normalize;
            var c = Math.Atan2(a.x, a.y);
            Position.LocalPosition.x += (float)Math.Cos(c) * Speed;
            Position.LocalPosition.y += -(float)Math.Sin(c) * Speed;
            //----------------------------------

            Lazer.GetComponent<Raypier>().Active = false;

            // 自機に戻る
            Position.Parent = Player.Position;

            // 位置のリセット
            if (Index == BitIndex.BIT_LEFT)
            {
                Position.LocalPosition = new Vector3 { x = -10.0f, y = -5.0f, z = 0.0f };
            }
            else
            {
                Position.LocalPosition = new Vector3 { x = 10.0f, y = -5.0f, z = 0.0f };
            }

            // 回転のリセット
            Position.LocalRotation = new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f);

            IsDock = true;
        }

        /// <summary>
        /// @brief ビットの移動
        /// </summary>
        private void Move()
        {
            ++Energy;
            /*
             * http://dixq.net/forum/viewtopic.php?f=3&t=6697
             * 現在の角度を目標の方向に近い方で回転させるという意味だとしたら
現在の角度と目標の方向を三角関数に放り込んで2次元のベクトルを作り、
外積の符号を見るとよろしいかと思います。
             * */
            
            //if ((Rot >= 270 && Rot <= 360) || (Rot <= 90 && Rot >= 0)) //上半分
            //{
            //    LRFlag = true;
            //}
            //if (Rot <= 270 && Rot >= 90)//下半分
            //{
            //    LRFlag = false;
            //}

            int dir = 0x0000;
            var EightDirection = Game.Instance.InputController.XController.GetStick8Direction(false, 10000);
            if (EightDirection == XGameController.StickDirection.LEFTDOWN)
            {//左下
                dir |= 0x0110;
            }
            else if (EightDirection == XGameController.StickDirection.LEFTUP)
            {//左上
                dir |= 0x1100;
            }
            else if (EightDirection == XGameController.StickDirection.RIGHTDOWN)
            {//右下
                dir |= 0x0011;
            }
            else if (EightDirection == XGameController.StickDirection.RIGHTUP)
            {//右上
                dir |= 0x1001;
            }

            // 移動処理
            if (DX.CheckHitKey(DX.KEY_INPUT_UP) == 1
                 || EightDirection == XGameController.StickDirection.UP)
            {
                dir |= 0x1000;
                var Direciton = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };
                var b = new Quaternion(Position.LocalAxisZ, Angle);//Zziku Angle分回転する回転量
                var c = Direciton * b;//Angle分回転した向きベクトル

                var Purpose = new Vector3 { x = 0.0f, y = -1.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

                


                if (
                    (Rot < 270 && Rot > 90) 
                    ||
                    ((Rot > 270 && Rot <= 360) || (Rot >= 0 && Rot < 90))
                    )
                {
                    if (Cross.z >= 0)
                        Rot -= 3;
                    else
                        Rot += 3;

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 60);
                }
                else
                {
                    Rot = 90;
                }
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_DOWN) == 1
                 || EightDirection == XGameController.StickDirection.DOWN)
            {
                dir |= 0x0010;
                var Direciton = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };
                var b = new Quaternion(Position.LocalAxisZ, Angle);//Zziku Angle分回転する回転量
                var c = Direciton * b;//Angle分回転した向きベクトル

                var Purpose = new Vector3 { x = 0.0f, y = 1.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

                


                if (
                    (Rot < 270 && Rot > 90)
                    ||
                    ((Rot > 270 && Rot <= 360) || (Rot >= 0 && Rot < 90))
                    )
                {
                    if (Cross.z >= 0)
                        Rot -= 3;
                    else
                        Rot += 3;

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 60);
                }
                else
                {
                    Rot = 270;
                }
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_LEFT) == 1
                || EightDirection == XGameController.StickDirection.LEFT)
            {
                dir |= 0x0100;
                var Direciton = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };
                var b = new Quaternion(Position.LocalAxisZ, Angle);//Zziku Angle分回転する回転量
                var c = Direciton * b;//Angle分回転した向きベクトル

                var Purpose = new Vector3 { x = -1.0f, y = 0.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

                

                if (
                    (Rot > 180 && Rot < 360) ||
                    (Rot > 0 && Rot < 180)
                    )
                {
                    if (Cross.z <= 0)
                        Rot -= 3;
                    else
                        Rot += 3;

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, -(float)Math.PI / 60);
                }
                else
                {
                    Rot = 180;
                }
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_RIGHT) == 1
                || EightDirection == XGameController.StickDirection.RIGHT)
            {
                dir |= 0x0001;

                var Direciton = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };
                var b = new Quaternion(Position.LocalAxisZ, Angle);//Zziku Angle分回転する回転量
                var c = Direciton * b;//Angle分回転した向きベクトル

                var Purpose = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

               

                if (
                    (Rot > 180 && Rot < 360) ||
                    (Rot > 0 && Rot < 180)
                    )
                {
                    if (Cross.z <= 0)
                        Rot -= 3;
                    else
                        Rot += 3;

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 60);
                }
                else
                {
                    Rot = 0;
                }

            }


            float theta;
            switch (dir)
            {
                case 0x0001: theta = 0.0f; break;
                case 0x0011: theta = (float)Math.PI * 0.25f; break;
                case 0x0010: theta = (float)Math.PI * 0.5f; break;
                case 0x0110: theta = (float)Math.PI * 0.75f; break;
                case 0x0100: theta = (float)Math.PI; break;
                case 0x1100: theta = (float)Math.PI * 1.25f; break;
                case 0x1000: theta = (float)Math.PI * 1.5f; break;
                case 0x1001: theta = (float)Math.PI * 1.75f; break;
                default: theta = -1.0f; break;
            }

            if (theta >= 0.0f)
            {
                Position.LocalPosition.x += (float)Math.Cos(theta) * Speed;
                Position.LocalPosition.y += -(float)Math.Sin(theta) * Speed;
            }

            Angle = Rot * -(float)Math.PI / 180;
        }


        /// <summary>
        /// @brief 切り離し
        /// </summary>
        public void Undock()
        {
            IsDock = false;
            Rot = 0;
            Position.Parent = Game.Instance.SceneController.CurrentScene.Root.Position;
        }
    }
}
