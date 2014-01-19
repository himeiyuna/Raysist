using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 前方から出現する敵コンポーネント
    /// </summary>
    class InfrontAppearEnemy : GameComponent
    {
        internal const int MaxLife = 100;
        private const int AnimationFrame = 90;

        /// <summary>
        /// @brief 開始地点
        /// </summary>
        private Vector3 From
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 終了地点
        /// </summary>
        private Vector3 To
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 差分
        /// </summary>
        private Vector3 Diff
        {
            set;
            get;
        }

        /// <summary>
        /// @brief フレーム数
        /// </summary>
        private int Frame
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="time"></param>
        public InfrontAppearEnemy(GameContainer container, Vector3 to, Vector3 from) : base(container)
        {
            Position.LocalPosition = from;
            From = from;
            To = to;
            Diff = to - from;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            ++Frame;
            if (Frame > AnimationFrame)
            {
                Active = false;
                return;
            }

            float r = Frame / (float)AnimationFrame;

            Position.LocalPosition = new Vector3() { x = From.x + Diff.x * r, y = Position.LocalPosition.y + (To.y - Position.LocalPosition.y) * 0.05f, z = From.z + Diff.z * r };
        }

        /// <summary>
        /// @brief 非アクティブになったときに呼び出される
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();

            Container.GetComponent<StayEnemy>().Active = true;
            Container.GetComponent<RectCollider>().Active = true;
        }
    }

    class InfrontAppearEnemyFactory : ContainerFactory
    {
        public enum Direction
        {
            LEFT,
            RIGHT
        }

        private Direction Dir
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 目的地
        /// </summary>
        private Vector3 To
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 滞在時間
        /// </summary>
        private int StayTime
        {
            set;
            get;
        }

        public InfrontAppearEnemyFactory(Vector3 to, Direction dir, int stayTime)
        {
            To = to;
            Dir = dir;
            StayTime = stayTime;
        }

        public override GameContainer Create()
        {
            var gc = new GameContainer();
            var iae = new InfrontAppearEnemy(gc, To, new Vector3 { x = To.x + 500.0f * (Dir == Direction.LEFT ? -1 : 1), y = 500.0f, z = 0.0f });
            var se = new StayEnemy(gc, StayTime);
            var de = new DisappearEnemy(gc, new Vector3 { x = To.x + 500.0f * (Dir == Direction.LEFT ? 1 : -1), y = 500.0f, z = 0.0f }, 0.3f);
            gc.AddComponent(iae);
            gc.AddComponent(se);
            gc.AddComponent(de);
            gc.AddComponent(new MeshRenderer(gc, "fighter.x"));

            var col = new RectCollider(gc, (Collider c) =>
            {

            });
            col.Width = 50.0f;
            col.Height = 50.0f;
            gc.AddComponent(col);

            gc.Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f);
            return gc;
        }
    }
}
