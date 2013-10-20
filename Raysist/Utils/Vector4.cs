using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Raysist
{
    public class Vector4
    {
        /// <summary>
        /// 要素
        /// </summary>
        private float[] elements = new float[4];

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
        /// @brief w座標
        /// </summary>
        public float w 
        { 
            set 
            { 
                elements[3] = value; 
            } 
            get
            { 
                return elements[3]; 
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
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4 { x = left.x + right.x, y = left.y + right.y, z = left.z + right.z, w = left.w + right.w };
        }

        /// <summary>
        /// @brief 減算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4 { x = left.x - right.x, y = left.y - right.y, z = left.z - right.z, w = left.w - right.w };
        }
    }
}
