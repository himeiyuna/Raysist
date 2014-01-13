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
        /// <summary>
        /// @brief ゲーム画面範囲
        /// </summary>
        public static Collider.AABB GameArea
        {
            get
            {
                var ret = new Collider.AABB();
                ret.Left = 300.0f;
                ret.Right = 1300.0f;
                ret.Top = 0.0f;
                ret.Bottom = 800.0f;
                return ret;
            }
        }

        public GameScene()
            : base()
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
                g.AddComponent(new DisablePlayer(g));
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

            var gc = cf.Create();
            var player = gc.GetComponent<Player>();

            var fact = new ContainerFactory((GameContainer g) =>
            {
                var a = new RectCollider(g, (Collider c) => { DX.DrawString(0, 0, "HIT", DX.GetColor(255, 255, 255)); });
                a.Width = 10.0f;
                a.Height = 10.0f;
                g.AddComponent(a);
                g.Position.LocalPosition = new Vector3 { x = 50.0f, y = 50.0f, z = 0.0f };
            });
            fact.Create();

            var ec = new GameContainer();
            ec.AddComponent(new EnemyController(ec));

            /*var ef = new BehindAppearEnemyFactory(new Vector3 { x = -400.0f, y = -1000.0f, z = 0.0f }, new Vector3 { x = 0.0f, y = 400.0f, z = 0.0f }, 1.0f);
            ef.Create();

            ef = new BehindAppearEnemyFactory(new Vector3 { x = -400.0f, y = -1000.0f, z = 0.0f }, new Vector3 { x = 50.0f, y = 400.0f, z = 0.0f }, 2.0f);
            ef.Create();

            ef = new BehindAppearEnemyFactory(new Vector3 { x = -400.0f, y = -1000.0f, z = 0.0f }, new Vector3 { x = 100.0f, y = 400.0f, z = 0.0f }, 3.0f);
            ef.Create();*/

            /*var enemy = new GameContainer();
            enemy.AddComponent(new Enemy(enemy, new Vector3 { x = 0.0f, y = 800.0f, z = 0.0f }));

            enemy = new GameContainer();
            enemy.AddComponent(new Enemy(enemy, new Vector3 { x = 200.0f, y = 500.0f, z = 0.0f }));

            enemy = new GameContainer();
            enemy.AddComponent(new Enemy(enemy, new Vector3 { x = -200.0f, y = 300.0f, z = 0.0f }));*/

            //player.Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f) * new Quaternion(Vector3.AxisZ, (float)Math.PI);

            var camera = cameraFactory.Create().GetComponent<Camera>();
            camera.Position.LocalPosition = new Vector3 { x = 0.0f, y = 0.0f, z = -1000.0f };
            camera.FieldOfView = (float)Math.PI * 0.1f;

            var sf = new ContainerFactory((GameContainer g) =>
            {
                var gcom = new Stage(g);
                g.AddComponent(gcom);
                g.AddComponent(new StageTimeline(g, gcom));
                g.AddComponent(new MeshRenderer(g, "hogeStage.x"));
            });
            
            sf.Create();
        }

        public override void LeaveScene()
        {
        }

        public override void UnloadResource()
        {
        }
    }
}
