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
        /// @brief 要素
        /// </summary>
        private float[] elements;

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
        /// @brief 
        /// </summary>
        public Vector4 Normalize
        {
            get
            {
                var l = Length;
                return new Vector4 { x = x / l, y = y / l, z = z / l, w = w / l };
            }
        }

        /// <summary>
        /// @brief 長さを取得する
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(Length2);
            }
        }

        /// <summary>
        /// @brief 長さの２乗を取得する 
        /// </summary>
        public float Length2
        {
            get
            {
                return x * x + y * y + z * z + w * w;
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

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Vector4 operator *(float left, Vector4 right)
        {
            return new Vector4 { x = left * right.x, y = left * right.y, z = left * right.z, w = left * right.w };
        }

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Vector4 operator *(Vector4 left, float right)
        {
            return new Vector4 { x = left.x * right, y = left.y * right, z = left.z * right, w = left.w * right };
        }

        /// <summary>
        /// @brief デフォルトコンストラクタ
        /// </summary>
        public Vector4()
        {
            elements = new float[4]{ 0.0f, 0.0f, 0.0f, 0.0f };
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="right">サイズ4の配列</param>
        public Vector4(float[] array)
        {
            elements = array;
        }

        /// <summary>
        /// @brief 配列を返す
        /// </summary>
        /// <returns>配列</returns>
        public float[] ToArray()
        {
            return elements;
        }
    }
}
