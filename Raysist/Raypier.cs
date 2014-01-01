using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class Raypier : GameComponent
    {
        private Positioner BitPosition
        {
            set;
            get;
        }

        private GameContainer Ender
        {
            set;
            get;
        }

        public Raypier(GameContainer container, GameContainer re, Positioner bitPosition) : base(container)
        {
            Ender = re;
            BitPosition = bitPosition;
            Active = false;
        }

        public override void Update()
        {
            // 加算描画開始
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD, 255);

            var trail = new GameContainer(Container);
            trail.Position.LocalPosition = BitPosition.WorldPosition;
            trail.Position.LocalPosition.z = 0.0f;
            trail.AddComponent(new RaypierTrail(trail));
            var br = new BillboardRenderer(trail, "dummy2.png");
            br.Scale = 64.0f;
            trail.AddComponent(br);

            var col = new RectCollider(trail, (Collider g) =>
            {
                DX.DrawString(0, 400, "hit", DX.GetColor(255, 255, 255));
            });

            col.Width = 10.0f;
            col.Height = 10.0f;

            trail.AddComponent(col);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Ender.Active = false;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Ender.Active = false;
        }
    }
}
