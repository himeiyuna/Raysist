using DxLibDLL;
using System;
using System.Runtime.InteropServices;


namespace Raysist
{
    class Player : GameComponent
    {
        private const float PlayerWidth = 80.0f;
        private const float PlayerHeight = 80.0f;

        //---------------------------------------------------
        //メンバ変数こっから

        /// <summary>
        /// @brief 体力
        /// </summary>
        public int Life
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
        /// @brief 回転量
        /// </summary>
        private int Rot
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 最大回転量
        /// </summary>
        private const int MaxRot = 45;

        //ここまで
        //----------------------------------------------------



        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Player(GameContainer container) : base(container)
        {
            Speed = 0.5f;
            Position.LocalRotation = new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f) * new Quaternion(Vector3.AxisY, (float)Math.PI);
            Position.LocalPosition = new Vector3 { x = 100.0f, y = 0.0f, z = 0.0f };
            Position.LocalScale *= 0.1f;
            Rot = 0;

            //Collider.AABB a = new Collider.AABB();



            var bitmaker = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Bit(g, this, Bit.BitIndex.BIT_LEFT));


                g.AddComponent(new MeshRenderer(g, "bit.x"));

                g.Position.LocalRotation *= new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f);
                g.Position.LocalScale *= 3.0f;
            });

            var bitmaker2 = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Bit(g, this, Bit.BitIndex.BIT_RIGHT));
                g.AddComponent(new MeshRenderer(g, "bit.x"));

                g.Position.LocalRotation *= new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f);
                g.Position.LocalScale *= 3.0f;
            });

            bitmaker.Create(Container);
            bitmaker2.Create(Container);
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            int dir = 0x0000;

            // 移動処理
            if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
            {
                dir |= 0x1000;
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
            {
                dir |= 0x0010;
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                dir |= 0x0100;

                Rot -= 3;
                if (Rot >= -MaxRot)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, -(float)Math.PI / 60);
                }
                else
                {
                    Rot = -MaxRot;
                }
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
            {
                dir |= 0x0001;

                Rot += 3;
                if (Rot <= MaxRot)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 60);
                }
                else
                {
                    Rot = MaxRot;
                }
            }
            else
            {
                if (Rot != 0)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 180 * (Rot < 0 ? 3 : -3));
                    Rot += Rot < 0 ? 3 : -3;
                }
            }

            float theta;
            switch (dir)
            {
                case 0x0001: theta = 0.0f; break;
                case 0x0011: theta = (float)Math.PI * 0.25f; break;
                case 0x0010: theta = (float)Math.PI * 0.5f;  break;
                case 0x0110: theta = (float)Math.PI * 0.75f; break;
                case 0x0100: theta = (float)Math.PI;         break;
                case 0x1100: theta = (float)Math.PI * 1.25f; break;
                case 0x1000: theta = (float)Math.PI * 1.5f;  break;
                case 0x1001: theta = (float)Math.PI * 1.75f; break;
                default: theta = -1.0f; break;
            }

            if (theta >= 0.0f)
            {
                Position.LocalPosition.x += (float)Math.Cos(theta) * Speed;
                Position.LocalPosition.y += -(float)Math.Sin(theta) * Speed;
            }
          
            var pos = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
            if (300.0f + PlayerWidth * 0.5f > pos.x)
            {
                pos.x = 300.0f + PlayerWidth * 0.5f;
                Position.LocalPosition.x = DX.ConvScreenPosToWorldPos(pos).x;
            }
            else if (1300.0f - PlayerWidth * 0.5f < pos.x)
            {
                pos.x = 1300.0f - PlayerWidth * 0.5f;
                Position.LocalPosition.x = DX.ConvScreenPosToWorldPos(pos).x;
            }

            if (PlayerHeight * 0.5f > pos.y)
            {
                pos.y = PlayerHeight * 0.5f;
                Position.LocalPosition.y = DX.ConvScreenPosToWorldPos(pos).y;
            }
            else if (800.0f - PlayerHeight * 0.5f < pos.y)
            {
                pos.y = 800.0f - PlayerHeight * 0.5f;
                Position.LocalPosition.y = DX.ConvScreenPosToWorldPos(pos).y;
            }
            DX.DrawBox(300, 0, 1300, 800,255,0);//有効距離

            // ビット射出
            if (DX.CheckHitKey(DX.KEY_INPUT_SPACE) == 1)
            {
                var bit = Position.FindChildren("Bit");
                foreach (var b in bit)
                {
                    b.Container.GetComponent<Bit>().Undock();
                }
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1)
            {
                var dis = Container.GetComponent<DisablePlayer>();
                dis.Active = true;
            }
        }
    }

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
        public Bit(GameContainer container, Player player, BitIndex index) : base(container)
        {
            Energy = MaxEnergy;      // 2秒分
            Player = player;
            Index = index;
            ShotCounter = 0;

            IsDock = true;

            container.Name = "Bit";

            if (index == BitIndex.BIT_LEFT)
            {
                Position.LocalPosition = new Vector3 { x = -10.0f, y = -10.0f, z = 0.0f };
            }
            else
            {
                Position.LocalPosition = new Vector3 { x = 10.0f, y = -10.0f, z = 0.0f };
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
                        g.AddComponent(b);

                        var col = new RectCollider(g, (Collider c) => { return; });
                        col.Width = 10.0f;
                        col.Height = 10.0f;

                        g.AddComponent(col);
                    });

                    sf.Create(Game.Instance.SceneController.CurrentScene.Root);
                }
            }
            else
            {
                --Energy;

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

            // 自機に戻る
            Position.Parent = Player.Position;

            // 位置のリセット
            if (Index == BitIndex.BIT_LEFT)
            {
                Position.LocalPosition = new Vector3 { x = -10.0f, y = -10.0f, z = 0.0f };
            }
            else
            {
                Position.LocalPosition = new Vector3 { x = 10.0f, y = -10.0f, z = 0.0f };
            }

            // 回転のリセット
            Position.LocalRotation = new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f);

            IsDock = true;
        }

        /// <summary>
        /// @brief 切り離し
        /// </summary>
        public void Undock()
        {
            IsDock = false;
            Position.Parent = Game.Instance.SceneController.CurrentScene.Root.Position;
        }
    }
}
