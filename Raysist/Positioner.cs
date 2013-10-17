using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 位置情報や親子関係を管理するクラス
    /// </summary>
    public class Positioner
    {
        private Positioner parent;

        private Matrix     worldMatrix = Matrix.Identity;

        /// <summary>
        /// @brief ローカル座標
        /// </summary>
        public Vector3    LocalPosition { set; get; }

        public Vector3    WorldPosition
        {
            get
            {
                var t = Transform;
                return new Vector3 { X = t[3, 0], Y = t[3, 1], Z = t[3, 2] };
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得する
        /// </summary>
        public Matrix     Transform 
        {
            get
            {
                // 親がいなくなるまでループを続ける
                return parent == null ? worldMatrix : parent.Transform * worldMatrix;
            }
        }

        /// <summary>
        /// @brief 親の位置
        /// </summary>
        public Positioner Parent
        {
            set
            {
                parent = value;
            }
            get
            {
                return parent;
            }
        }
    }
}
