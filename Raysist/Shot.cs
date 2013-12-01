using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class ShotComponent : GameComponent
    {
        //---------------------------------------------------
        //メンバ変数こっから

        /// <summary>
        /// @brief 体力
        /// </summary>
        public float Angle
        {
            private set;
            get;
        }


        //ここまで
        //----------------------------------------------------

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public ShotComponent(GameContainer container, float angle, Vector3 position)
            : base(container)
        {
            Position.LocalRotation *= new Quaternion(Vector3.AxisY, (float)Math.PI);
            Position.LocalPosition = position;
            Angle = angle;
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            // 移動処理
            Position.LocalPosition.x += (float)Math.Cos(Angle) * 10.0f;
            Position.LocalPosition.z += (float)Math.Sin(Angle) * 10.0f;
        }


    }
}
