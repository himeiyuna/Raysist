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
        private Matrix     transform = Matrix.Identity;

        /// <summary>
        /// @brief ローカル座標を取得するプロパティ
        /// </summary>
        public  Vector3    LocalPosition 
        {
            set
            {
                transform[3, 0] = value.x;
                transform[3, 1] = value.y;
                transform[3, 2] = value.z;
            }
            get
            {
                return new Vector3 { x = transform[3, 0], y = transform[3, 1], z = transform[3, 2] };
            }
        }

        /// <summary>
        /// @brief ローカル変形行列を取得するプロパティ
        /// </summary>
        public Matrix      LocalTransform 
        {
            set
            {
                transform = value;
            }
            get
            {
                return transform;
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得するプロパティ
        /// </summary>
        public  Vector3    WorldPosition
        {
            get
            {
                var t = WorldTransform;
                return new Vector3 { x = t[3, 0], y = t[3, 1], z = t[3, 2] };
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得するプロパティ
        /// </summary>
        public  Matrix     WorldTransform
        {
            get
            {
                // 親がいなくなるまで再帰を続ける
                return parent == null ? LocalTransform : parent.WorldTransform * LocalTransform;
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
