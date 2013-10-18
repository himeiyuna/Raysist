using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Raysist
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Vector4
    {
        /// <summary>
        /// 要素
        /// </summary>
        [FieldOffset(0)]
        private fixed float elements[4];

        [FieldOffset(0)]
        private float x;

        [FieldOffset(4)]
        private float y;

        [FieldOffset(8)]
        private float z;

        [FieldOffset(12)]
        private float w;

        /// <summary>
        /// X
        /// </summary>
        public float X { set { x = value; } get { return x; } }

        /// <summary>
        /// Y
        /// </summary>
        public float Y { set { y = value; } get { return y; } }

        /// <summary>
        /// Z
        /// </summary>
        public float Z { set { z = value; } get { return z; } }

        /// <summary>
        /// W
        /// </summary>
        public float W { set { w = value; } get { return z; } }

        /// <summary>
        /// @brief 配列
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>値</returns>
        public float this[int index]
        {
            set
            {
                fixed (float* p = elements)
                    p[index] = value;

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
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4 { X = left.X + right.X, Y = left.Y + right.Y, Z = left.Z + right.Z, W = left.W + right.W };
        }

        /// <summary>
        /// @brief 減算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4 { X = left.X - right.X, Y = left.Y - right.Y, Z = left.Z - right.Z, W = left.W - right.W };
        }
    }
}
