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
        public RaypierTrail(GameContainer container) : base(container)
        {

        }

        public override void Update()
        {
            Position.LocalPosition.y += 15.0f;
            
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
}
