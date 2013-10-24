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
        /// <summary>
        /// @brief 親
        /// </summary>
        private Positioner parent;

        /// <summary>
        /// @brief ワールド変換行列
        /// </summary>
        private Matrix     worldMatrix = Matrix.Identity;

        /// <summary>
        /// @brief ローカル座標を取得するプロパティ
        /// </summary>
        public  Vector3    LocalPosition 
        {
            set
            {
                worldMatrix[3, 0] = value.x;
                worldMatrix[3, 1] = value.y;
                worldMatrix[3, 2] = value.z;
            }
            get
            {
                return new Vector3 { x = worldMatrix[3, 0], y = worldMatrix[3, 1], z = worldMatrix[3, 2] };
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得するプロパティ
        /// </summary>
        public  Vector3    WorldPosition
        {
            get
            {
                var t = Transform * ;
                return new Vector3 { x = t[3, 0], y = t[3, 1], z = t[3, 2] };
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得するプロパティ
        /// </summary>
        public  Matrix     Transform 
        {
            get
            {
                // 親がいなくなるまでループを続ける
                return parent == null ? worldMatrix : parent.Transform * worldMatrix;
            }
        }

        /// <summary>
        /// @brief 親の位置を取得するプロパティ
        /// </summary>
        public  Positioner Parent
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
