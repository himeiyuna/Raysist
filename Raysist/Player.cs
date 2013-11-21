using DxLibDLL;
using System;
using System.Runtime.InteropServices;

namespace Raysist
{
    class Player : GameComponent
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Player(GameContainer container) : base(container)
        {
            Position.LocalRotation *= new Quaternion(Vector3.AxisY, (float)Math.PI);
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
                Position.LocalPosition.z += 1.0f; 
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
            {
                //Position.LocalRotation *= new Quaternion(Position.LocalAxisX, -0.1f) * new Quaternion(Position.LocalAxisZ, -0.1f);
                Position.LocalPosition.z -= 1.0f;
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                Position.LocalPosition.x -= 1.0f;
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
            {
                Position.LocalPosition.x += 1.0f;
            }
        }
    }

    class Bit : GameComponent
    {
        public Bit(GameContainer container) : base(container)
        {
            Position.LocalPosition = new Vector3 { x = 40.0f, y = 0.0f, z = -40.0f };
        }

        public override void Update()
        {
            
        }
    }

    class TestScene : Scene
    {
        public TestScene()
        {
            
        }

        public override void LoadResource()
        {
        }

        public override void EnterScene()
        {
            var cf = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Player(g));
                //g.AddComponent(new SpriteRenderer(g, "dummy.png"));
                g.AddComponent(new MeshRenderer(g, "fighter.x"));
            });

            var cameraFactory = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Camera(g));
            });

            var gc = cf.Create(Root);
            var player = gc.GetComponent<Player>();

            var bitmaker = new ContainerFactory((GameContainer g) =>
            {
                g.AddComponent(new Bit(g));
                g.AddComponent(new BillboardRenderer(g, "dummy.png"));
            });

            bitmaker.Create(gc);
            //player.Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f) * new Quaternion(Vector3.AxisZ, (float)Math.PI);

            var camera = cameraFactory.Create(Root).GetComponent<Camera>();
            camera.Position.LocalPosition = new Vector3 { x = 0.0f, y = 1000.0f, z = 0.0f };
            camera.Position.LocalRotation *= new Quaternion(Vector3.AxisX, (float)Math.PI / 2.0f);
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
