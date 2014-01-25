using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief レンダラ基底クラス
    /// </summary>
    public abstract class Renderer : GameComponent
    {
        public Renderer(GameContainer container)    
        :   base(container)
        {
     
        }
    }
}
