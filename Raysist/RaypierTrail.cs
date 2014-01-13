using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class RaypierTrail : GameComponent
    {
        private const float Speed = 50.0f;

        /// <summary>
        /// @brief 角度
        /// </summary>
        private float Angle
        {
            set;
            get;
        }

        public RaypierTrail(GameContainer container, float angle) : base(container)
        {
            Angle = angle;
        }

        public override void Update()
        {
            var pos = Position.LocalPosition;
            Position.LocalPosition = new Vector3 { x = pos.x + (float)Math.Cos(Angle) * Speed, y = pos.y - (float)Math.Sin(Angle) * Speed, z = 0.0f };
            
            var wp = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
            var ga = GameScene.GameArea;
            if (wp.x < ga.Left || wp.x > ga.Right ||
                wp.y < ga.Top || wp.y > ga.Bottom)
            {
                // TODO:外に出たら破棄する
                GameContainer.Destroy(Container);
            }
        }
    }

    class RaipierTrailFactory : ContainerFactory
    {
        /// <summary>
        /// @brief 親
        /// </summary>
        private GameContainer Parent
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ビットの場所
        /// </summary>
        private Bit Bit
        {
            set;
            get;
        }
        
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="parent">@param 親コンテナ</param>
        /// <param name="angle">@param 進行方向</param>
        public RaipierTrailFactory(GameContainer parent, Bit bit, Action<GameContainer> option) : base(option)
        {
            Parent = parent;
            Bit = bit;
        }


        public override GameContainer Create()
        {
            var gc = new GameContainer(Parent);

            gc.Position.LocalPosition = Bit.Position.WorldPosition;
            gc.Position.LocalPosition.z = 0.0f;
            gc.AddComponent(new RaypierTrail(gc, Bit.Angle));
            var br = new BillboardRenderer(gc, "dummy2.png");
            br.Scale = 64.0f;
            gc.AddComponent(br);

            var col = new RectCollider(gc, (Collider g) =>
            {
                DX.DrawString(0, 400, "hit", DX.GetColor(255, 255, 255));
            });

            col.Width = 10.0f;
            col.Height = 10.0f;

            gc.AddComponent(col);

            Option(gc);

            return gc;
        }
    }
}
