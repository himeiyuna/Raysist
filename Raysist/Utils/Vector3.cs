using DxLibDLL;
using System;
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
        /// @brief 要素を取得する
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
        /// @brief 長さを取得する
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y + z * z);
            }
        }

        /// <summary>
        /// @brief 長さの２乗を取得する
        /// </summary>
        public float Length2
        {
            get
            {
                return x * x + y * y + z * z;
            }
        }

        /// <summary>
        /// @brief 単位ベクトル
        /// </summary>
        public Vector3 Normalize
        {
            get
            {
                var length = Length;
                return new Vector3 { x = x / length, y = y / length, z = z / length };
            }
        }

        /// <summary>
        /// @brief デフォルトコンストラクタ
        /// </summary>
        public Vector3()
        {
            elements = new float[3] { 0.0f, 0.0f, 0.0f };
        }

        /// <summary>
        /// @brief 加算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
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