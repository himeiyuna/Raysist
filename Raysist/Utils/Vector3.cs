using DxLibDLL;
using System.Runtime.InteropServices;

namespace Raysist
{
    /// <summary>
    /// 3次元ベクトルクラス
    /// </summary>
    public class Vector3
    {
        /// <summary>
        /// @brief 配列式
        /// </summary>
        private float[] elements = new float[3];

        /// <summary>
        /// @brief x座標
        /// </summary>
        public float x
        {
            set
            {
                elements[0] = value;
            }
            get
            {
                return elements[0];
            }
        }

        /// <summary>
        /// @brief y座標
        /// </summary>
        public float y
        {
            set
            {
                elements[1] = value;
            }
            get
            {
                return elements[1];
            }
        }

        /// <summary>
        /// @brief z座標
        /// </summary>
        public float z
        {
            set
            {
                elements[2] = value;
            }
            get
            {
                return elements[2];
            }
        }

        /// <summary>
        /// @brief 配列
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>値</returns>
        public float this[int index]
        {
            set
            {
                elements[index] = value;
            }
            get
            {
                return elements[index];
            }
        }

        /// <summary>
        /// @brief 加算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3 { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z };
        }

        /// <summary>
        /// @brief 減算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3 { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z };
        }
    }
}