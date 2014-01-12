using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief 後方から出現する敵コンポーネント
    /// </summary>
    class BehindAppearEnemy : GameComponent
    {
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
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="to">目的地</param>
        public BehindAppearEnemy(GameContainer container, Vector3 from, Vector3 to) : base(container)
        {
            From = from;
            To = to;
            Diff = to - from;

            Position.LocalPosition = from;
        }

        public override void Update()
        {
            ++Frame;
            if (Frame > AnimationFrame)
            {
                Active = false;
                return;
            }

            float r = Frame / (float)AnimationFrame;

            Position.LocalPosition = new Vector3() { x = From.x + Diff.x * r, y = Position.LocalPosition.y + (To.y - Position.LocalPosition.y) * 0.1f, z = From.z + Diff.z * r };
        }

        /// <summary>
        /// @brief 非アクティブになったときに呼び出される
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();

            Container.GetComponent<RetreatEnemy>().Active = true;
            Container.GetComponent<RectCollider>().Active = true;
        }
    }

    class BehindAppearEnemyFactory : ContainerFactory
    {
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
        /// @brief 移動速度
        /// </summary>
        private float Speed
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public BehindAppearEnemyFactory(Vector3 from, Vector3 to, float speed) : base()
        {
            From = from;
            To = to;
            Speed = speed;
        }

        public override GameContainer Create()
        {
            var gc = new GameContainer();
            gc.AddComponent(new BehindAppearEnemy(gc, From, To));

            var re = new RetreatEnemy(gc, Speed);
            re.Active = false;
            gc.AddComponent(re);

            var rc = new RectCollider(gc, (Collider c) =>
            {
                DX.DrawString(0, 150, "Enemy Hit", DX.GetColor(255, 255, 255));
            });
            rc.Active = false;
            rc.Width = 50.0f;
            rc.Height = 50.0f;
            gc.AddComponent(rc);

            gc.AddComponent(new MeshRenderer(gc, "fighter.x"));

            gc.Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f); ;

            return gc;
        }
    }
}
