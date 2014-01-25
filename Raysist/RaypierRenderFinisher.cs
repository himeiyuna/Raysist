using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class RaypierRenderFinisher : GameComponent
    {
        public RaypierRenderFinisher(GameContainer container) : base(container)
        {
        }

        public override void Update()
        {
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
        }
    }
}
