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
            }
            else
            {
                Position.LocalPosition = new Vector3 { x = 10.0f, y = -5.0f, z = 0.0f };
                container.Name = "BitRight";
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
                        g.AddComponent(new Shot(g, (float)Math.PI * -0.5f, 5.0f , Position.WorldPosition));

                        var b = new BillboardRenderer(g, "dummy.png");
                        b.Scale = 5.0f;
                        g.AddComponent(b);

                        var col = new RectCollider(g, (Collider c) =>
                        {
                            var target = c.Container.GetComponent<EnemyInformation>();
                            if (target != null)
                            {
                                target.Damage(10);
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
            int dir = 0x0000;
            var Direciton = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };//右向きの基本ベクトル
            float Clockwise = (float)Math.PI / 60;//曲げる分；
            var b = new Quaternion(Position.LocalAxisZ, Angle);//Zziku Angle分回転する回転量
            var c = Direciton * b;//Angle分回転した向きベクトル
            var EightDirection = XGameController.StickDirection.NONE;

            // コントローラーが接続されていない場合は判定しない
            if (Game.Instance.InputController.XController != null) 
            {
                EightDirection = Game.Instance.InputController.XController.GetStick8Direction(false, 10000);
            }
            
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
                var Purpose = new Vector3 { x = 0.0f, y = -1.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

                if (((Rot < 270 && Rot > 90) || ((Rot > 270 && Rot < 360) || (Rot >= 0 && Rot < 90))) ||
                    Rot == 270)
                {
                    if (Cross.z >= 0)
                    {
                        Rot -= 3;
                        Clockwise = (float)Math.PI / 60;
                    }
                    else
                    {
                        Rot += 3;
                        Clockwise = -(float)Math.PI / 60;
                    }
                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, Clockwise);
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
                var Purpose = new Vector3 { x = 0.0f, y = 1.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

                if (((Rot < 270 && Rot > 90) || ((Rot > 270 && Rot < 360) || (Rot >= 0 && Rot < 90))) ||
                    Rot == 90)
                {
                    if (Cross.z >= 0)
                    {
                        Rot -= 3;
                        Clockwise = (float)Math.PI / 60;
                    }
                    else
                    {
                        Rot += 3;
                        Clockwise = -(float)Math.PI / 60;
                    }

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, Clockwise);
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
                var Purpose = new Vector3 { x = -1.0f, y = 0.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);

                if (((Rot > 180 && Rot < 360) || (Rot > 0 && Rot < 180)) || Rot == 0)
                {
                    if (Cross.z <= 0)
                    {
                        Rot -= 3;
                        Clockwise = (float)Math.PI / 60;
                    }
                    else
                    {
                        Rot += 3;
                        Clockwise = -(float)Math.PI / 60;
                    }

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, Clockwise);
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
                var Purpose = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };
                var Cross = c.Cross(Purpose);



                if (((Rot > 180 && Rot < 360) || (Rot > 0 && Rot < 180)) || Rot == 180)
                {
                    if (Cross.z <= 0)
                    {
                        Rot -= 3;
                        Clockwise = (float)Math.PI / 60;
                    }
                    else
                    {
                        Rot += 3;
                        Clockwise = -(float)Math.PI / 60;
                    }

                    if (Rot >= 360)//360度を越えたら
                    {
                        Rot -= 360;
                    }
                    if (Rot < 0)//-になったら
                    {
                        Rot += 360;
                    }
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, Clockwise);
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
            Rot = 90;
            Position.Parent = Game.Instance.SceneController.CurrentScene.Root.Position;
        }
    }

    class BitFactory : ContainerFactory
    {
        /// <summary>
        /// @brief プレイヤー
        /// </summary>
        private Player Player
        {
            set;
            get;
        }

        private Bit.BitIndex Index
        {
            set;
            get;
        }

        public BitFactory(Player player, Bit.BitIndex index, Action<GameContainer> option = null)
            : base(option)
        {
            Player = player;
            Index = index;
        }

        /// <summary>
        /// @brief コンテナ生成関数
        /// </summary>
        /// <returns></returns>
        public override GameContainer Create()
        {
            var g = new GameContainer(Player.Container);

            var raypierEnd = new GameContainer();
            raypierEnd.Active = false;
            var raypier = new GameContainer();

            var bit = new Bit(g, Player, Index, raypier);
            g.AddComponent(bit);

            var r = new Raypier(raypier, raypierEnd, bit);
            r.Active = false;
            raypier.AddComponent(r);


            g.AddComponent(new MeshRenderer(g, "bit.x"));

            g.Position.LocalRotation *= new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f);
            g.Position.LocalScale *= 3.0f;

            raypierEnd.AddComponent(new RaypierRenderFinisher(raypierEnd));

            return g;
        }
    }
}
