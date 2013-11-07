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
        /// @brief x軸
        /// </summary>
        public static readonly Vector3 AxisX = new Vector3 { x = 1.0f, y = 0.0f, z = 0.0f };

        /// <summary>
        /// @brief y軸
        /// </summary>
        public static readonly Vector3 AxisY = new Vector3 { x = 0.0f, y = 1.0f, z = 0.0f };

        /// <summary>
        /// @brief z軸
        /// </summary>
        public static readonly Vector3 AxisZ = new Vector3 { x = 0.0f, y = 0.0f, z = 1.0f };

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
        /// @brief 平行移動行列を取得するプロパティ
        /// </summary>
        public Matrix TranslationMatrix
        {
            get
            {
                var ret = Matrix.Identity;
                ret[3, 0] = x;
                ret[3, 1] = y;
                ret[3, 2] = z;
                return ret;
            }
        }

        /// <summary>
        /// @brief 拡縮行列を取得するプロパティ
        /// </summary>
        public Matrix ScaleMatrix
        {
            get
            {
                var ret = new Matrix();
                ret[0, 0] = x;
                ret[0, 1] = y;
                ret[0, 2] = z;
                ret[0, 3] = 1.0f;
                return ret;
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
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="right">配列</param>
        public Vector3(float[] array)
        {
            elements = array;
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

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Vector3 operator *(Vector3 left, Quaternion right)
        {
            var v = new Quaternion();
            v.x = left.x;
            v.y = left.y;
            v.z = left.z;

            v = right * v * right.Conjugate;

            return new Vector3 { x = v.x, y = v.y, z = v.z };
        }

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Vector3 operator *(Quaternion left, Vector3 right)
        {
            var v = new Quaternion();
            v.x = right.x;
            v.y = right.y;
            v.z = right.z;

            v = left * v * left.Conjugate;

            return new Vector3 { x = v.x, y = v.y, z = v.z };
        }

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Vector3 operator *(float left, Vector3 right)
        {
            return new Vector3 { x = left * right.x, y = left * right.y, z = left * right.z };
        }

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Vector3 operator *(Vector3 left, float right)
        {
            return new Vector3 { x = left.x * right, y = left.y * right, z = left.z * right };
        }

        /// <summary>
        /// @brief 内積を取得する
        /// </summary>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public float Dot(Vector3 right)
        {
            return x * right.x + y * right.y + z * right.z;
        }

        /// <summary>
        /// @brief 外積を取得する
        /// </summary>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public Vector3 Cross(Vector3 right)
        {
            return new Vector3 { x = y * right.z - z * right.y, 
                                 y = z * right.x - x * right.z,
                                 z = x * right.y - y * right.x };
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