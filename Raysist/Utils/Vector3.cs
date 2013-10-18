using DxLibDLL;
using System.Runtime.InteropServices;

namespace Raysist
{
    /// <summary>
    /// 3次元ベクトルクラス
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Vector3
    {
        /// <summary>
        /// @brief 配列式
        /// </summary>
        [FieldOffset(0)]
        private fixed float elements[3];

        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        [FieldOffset(8)]
        private float z;

        /// <summary>
        /// @brief X座標
        /// </summary>
        public float X { set { x = value; } get { return x; } }

        /// <summary>
        /// @brief Y座標
        /// </summary>
        public float Y { set { y = value; } get { return y; } }

        /// <summary>
        /// @brief Z座標
        /// </summary>       
        public float Z { set { z = value; } get { return z; } } 

        /// <summary>
        /// @brief 配列
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>値</returns>
        public float this[int index]
        {
            set
            {
                fixed (float* e = elements)
                    e[index] = value;
            }
            get
            {
                fixed (float* ret = elements)
                    return ret[index];
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
            return new Vector3 { X = left.X + right.X, Y = left.Y + right.Y, Z = left.Z + right.Z };
        }

        /// <summary>
        /// @brief 減算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3 { X = left.X - right.X, Y = left.Y - right.Y, Z = left.Z - right.Z };
        }
    }
}