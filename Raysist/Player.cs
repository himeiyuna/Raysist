using DxLibDLL;
using System;
using System.Runtime.InteropServices;

namespace Raysist
{
    class Player : GameComponent
    {
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
            Speed = 2.0f;
            Position.LocalRotation = new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f) * new Quaternion(Vector3.AxisY, (float)Math.PI);
            Position.LocalPosition = new Vector3 { x = 100.0f, y = 0.0f, z = 50.0f };
            Rot = 0;
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            

            // 移動処理
            if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
            {
                Position.LocalPosition.y += 1.0f;
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
            {

                Position.LocalPosition.y -= 1.0f;
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                Position.LocalPosition.x -= 1.0f;

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
                Position.LocalPosition.x += 1.0f;

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

            if (DX.CheckHitKey(DX.KEY_INPUT_SPACE) == 1)
            {
                var bit = Position.FindChildren("Bit");
                foreach (var b in bit)
                {
                    b.Parent = Game.Instance.SceneController.CurrentScene.Root.Position;
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
        public Bit(GameContainer container, Vector3 position) : base(container)
        {
            container.Name = "Bit";
            Position.LocalPosition = position;
        }

        public override void Update()
        {
            /*var c = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new ShotComponent(g, (float)Math.PI / 2.0f, Position.WorldPosition));
                g.AddComponent(new MeshRenderer(g, "fighter.x"));
            });
            //c.Create();*/
        }
    }
}
