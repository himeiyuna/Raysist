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
        private Bit Parent
        {
            set;
            get;
        }

        private GameContainer Ender
        {
            set;
            get;
        }

        public Raypier(GameContainer container, GameContainer re, Bit parent) : base(container, false)
        {
            Ender = re;
            Parent = parent;
        }

        public override void Update()
        {
            // 加算描画開始
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD, 255);

            var ang = Parent.Angle;
            var i = 0;
            var rtf = new RaipierTrailFactory(Container, Parent, (GameContainer g) => 
            {
                var pos = g.Position.LocalPosition;
                g.Position.LocalPosition = new Vector3 { x = pos.x + (float)Math.Cos(ang) * 17.0f * i, y = pos.y - (float)Math.Sin(ang) * 17.0f * i, z = 0.0f };
                ++i;
            });

            rtf.Create();
            rtf.Create();
            rtf.Create();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            Ender.Active = false;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            Ender.Active = true;
        }
    }
}
