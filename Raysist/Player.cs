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
                //Position.LocalRotation *= new Quaternion(Position.LocalAxisX, 0.1f) * new Quaternion(Position.LocalAxisZ, 0.1f);
                Position.LocalPosition.y += 1.0f; 
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
            {
                //Position.LocalRotation *= new Quaternion(Position.LocalAxisX, -0.1f) * new Quaternion(Position.LocalAxisZ, -0.1f);
                Position.LocalPosition.y -= 1.0f;
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                Position.LocalPosition.x -= 1.0f;

                --Rot;
                if (Rot >= -MaxRot)
                {
                    
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, -(float)Math.PI / 180);
                } 
                else
                {
                    Rot = -MaxRot;
                }
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
            {
                Position.LocalPosition.x += 1.0f;

                ++Rot;
                if (Rot <= MaxRot)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 180);
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
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 180 * (Rot < 0 ? 1 : -1));
                    Rot += Rot < 0 ? 1 : -1;
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

    class TestScene : Scene
    {
        public TestScene() : base()
        {
            
        }

        public override void LoadResource()
        {
        }

        public override void EnterScene()
        {
            CollisionManager.Initialize(8, -10.0f, -10.0f, 1610.0f, 910.0f);

            var cf = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Player(g));
                //g.AddComponent(new SpriteRenderer(g, "dummy.png"));
                g.AddComponent(new MeshRenderer(g, "fighter.x"));
                var col = new RectCollider(g, (Collider c) => {return;});
                col.Width = 10.0f;
                col.Height = 10.0f;
                g.AddComponent(col);
            });

            var cameraFactory = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Camera(g));
                
            });

            var gc = cf.Create(Root);
            var player = gc.GetComponent<Player>();

            var fact = new ContainerFactory((GameContainer g) =>
            {
                var a = new RectCollider(g, (Collider c) => { DX.DrawString(0, 0, "HIT", DX.GetColor(255, 255, 255)); });
                a.Width = 10.0f;
                a.Height = 10.0f;
                g.AddComponent(a);
            });
            fact.Create();


            var bitmaker = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Bit(g, new Vector3 { x = 40.0f, y = 0.0f, z = -40.0f }));

                var br = new BillboardRenderer(g, "");
                var animator = new Animator(g, br, "explosion.png", 4, 4, 1, 512, 512);
                br.Scale *= 25.0f;
                animator.UpdateFrame = 5;
                g.AddComponent(br);
                g.AddComponent(animator);

                var col = new RectCollider(g, (Collider c) => { return; });
                col.Width = 10.0f;
                col.Height = 10.0f;
                g.AddComponent(col);
            });

            var bit2maker = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Bit(g, new Vector3 { x = -40.0f, y = 0.0f, z = -40.0f }));

                var br = new BillboardRenderer(g, "");
                var animator = new Animator(g, br, "explosion.png", 4, 4, 1, 512, 512);
                br.Scale *= 25.0f;
                animator.UpdateFrame = 5;
                g.AddComponent(br);
                g.AddComponent(animator);
            });
            bitmaker.Create(gc);
            bit2maker.Create(gc);
            //player.Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f) * new Quaternion(Vector3.AxisZ, (float)Math.PI);

            var camera = cameraFactory.Create(Root).GetComponent<Camera>();
            camera.Position.LocalPosition = new Vector3 { x = 0.0f, y = 0.0f, z = -3000.0f };
            camera.FieldOfView = (float)Math.PI * 0.1f;
        }

        public override void LeaveScene()
        {
        }

        public override void UnloadResource()
        {
        }
    }
}
