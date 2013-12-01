using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class GameScene : Scene
    {
        public GameScene()
            : base()
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
                g.AddComponent(new DisablePlayer(g));
                //g.AddComponent(new SpriteRenderer(g, "dummy.png"));
                g.AddComponent(new MeshRenderer(g, "fighter.x"));
                var col = new RectCollider(g, (Collider c) => { return; });
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
