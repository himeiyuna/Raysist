using System;
using System.Runtime.InteropServices;
using DxLibDLL;
using System.Collections;

namespace Raysist
{
    /// <summary>
    /// @brief 行列
    /// </summary>
    public class Matrix
    {
        /// <summary>
        /// @brief 要素
        /// </summary>
        private float[,] elements;

        /// <summary>
        /// @brief 行要素を取得する
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>行</returns>
        public Vector4 this[int index]
        {
            set
            {
                elements[index, 0] = value.x;
                elements[index, 1] = value.y;
                elements[index, 2] = value.z;
                elements[index, 3] = value.w;
            }
            get
            {
                return new Vector4 { x = elements[index, 0], y = elements[index, 1], z = elements[index, 2], w = elements[index, 3] };
            }
        }

        /// <summary>
        /// @brief 要素を取得する
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns>要素</returns>
        public float this[int row, int col]
        {
            set
            {
                elements[row, col] = value;
            }
            get
            {
                return elements[row, col];
            }
        }

        /// <summary>
        /// @brief 単位行列を取得する
        /// </summary>
        public static Matrix Identity
        {
            get
            {
                var ret = new Matrix();
                ret[0, 0] = 1.0f;
                ret[1, 1] = 1.0f;
                ret[2, 2] = 1.0f;
                ret[3, 3] = 1.0f;

                return ret;
            }
        }

        /// <summary>
        /// @brief DxLib用の構造体へ暗黙的変換を行う
        /// </summary>
        /// <param name="value">変換対象</param>
        /// <returns>変換結果</returns>
        public static implicit operator DX.MATRIX (Matrix value)
        {
            // 非常に効率が悪い
            // 共用体的なものを使用して改善するべき
            var ret = new DX.MATRIX();
            ret.m00 = value[0, 0];
            ret.m01 = value[0, 1];
            ret.m02 = value[0, 2];
            ret.m03 = value[0, 3];
            ret.m10 = value[1, 0];
            ret.m11 = value[1, 1];
            ret.m12 = value[1, 2];
            ret.m13 = value[1, 3];
            ret.m20 = value[2, 0];
            ret.m21 = value[2, 1];
            ret.m22 = value[2, 2];
            ret.m23 = value[2, 3];
            ret.m30 = value[3, 0];
            ret.m31 = value[3, 1];
            ret.m32 = value[3, 2];
            ret.m33 = value[3, 3];

            return ret;
        }

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">行列</param>
        /// <param name="right">ベクトル</param>
        /// <returns>計算結果ベクトル</returns>
        public static Vector3 operator *(Matrix left, Vector3 right)
        {
            return new Vector3 { x = left[0, 0] * right.x + left[0, 1] * right.y + left[0, 2] * right.z,
                                 y = left[1, 0] * right.x + left[1, 1] * right.y + left[1, 2] * right.z,
                                 z = left[2, 0] * right.x + left[2, 1] * right.y + left[2, 2] + right.z };
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            var ret = Matrix.Identity;
            ret[0, 0] = left[0, 0] * right[0, 0] + left[0, 1] * right[1, 0] + left[0, 2] * right[2, 0] + left[0, 3] * right[3, 0];
            ret[0, 1] = left[0, 0] * right[0, 1] + left[0, 1] * right[1, 1] + left[0, 2] * right[2, 1] + left[0, 3] * right[3, 1];
            ret[0, 2] = left[0, 0] * right[0, 2] + left[0, 1] * right[1, 2] + left[0, 2] * right[2, 2] + left[0, 3] * right[3, 2];
            ret[0, 3] = left[0, 0] * right[0, 3] + left[0, 1] * right[1, 3] + left[0, 2] * right[2, 3] + left[0, 3] * right[3, 3];
            ret[1, 0] = left[1, 0] * right[0, 0] + left[1, 1] * right[1, 0] + left[1, 2] * right[2, 0] + left[1, 3] * right[3, 0];
            ret[1, 1] = left[1, 0] * right[0, 1] + left[1, 1] * right[1, 1] + left[1, 2] * right[2, 1] + left[1, 3] * right[3, 1];
            ret[1, 2] = left[1, 0] * right[0, 2] + left[1, 1] * right[1, 2] + left[1, 2] * right[2, 2] + left[1, 3] * right[3, 2];
            ret[1, 3] = left[1, 0] * right[0, 3] + left[1, 1] * right[1, 3] + left[1, 2] * right[2, 3] + left[1, 3] * right[3, 3];
            ret[2, 0] = left[2, 0] * right[0, 0] + left[2, 1] * right[1, 0] + left[2, 2] * right[2, 0] + left[2, 3] * right[3, 0];
            ret[2, 1] = left[2, 0] * right[0, 1] + left[2, 1] * right[1, 1] + left[2, 2] * right[2, 1] + left[2, 3] * right[3, 1];
            ret[2, 2] = left[2, 0] * right[0, 2] + left[2, 1] * right[1, 2] + left[2, 2] * right[2, 2] + left[2, 3] * right[3, 2];
            ret[2, 3] = left[2, 0] * right[0, 3] + left[2, 1] * right[1, 3] + left[2, 2] * right[2, 3] + left[2, 3] * right[3, 3];
            ret[3, 0] = left[3, 0] * right[0, 0] + left[3, 1] * right[1, 0] + left[3, 2] * right[2, 0] + left[3, 3] * right[3, 0];
            ret[3, 1] = left[3, 0] * right[0, 1] + left[3, 1] * right[1, 1] + left[3, 2] * right[2, 1] + left[3, 3] * right[3, 1];
            ret[3, 2] = left[3, 0] * right[0, 2] + left[3, 1] * right[1, 2] + left[3, 2] * right[2, 2] + left[3, 3] * right[3, 2];
            ret[3, 3] = left[3, 0] * right[0, 3] + left[3, 1] * right[1, 3] + left[3, 2] * right[2, 3] + left[3, 3] * right[3, 3];
            return ret;
        }

        /// <summary>
        /// @brief デフォルトコンストラクタ
        /// </summary>
        public Matrix()
        {
            elements = new float[4, 4] { { 0.0f, 0.0f, 0.0f, 0.0f },
                                         { 0.0f, 0.0f, 0.0f, 0.0f },
                                         { 0.0f, 0.0f, 0.0f, 0.0f },
                                         { 0.0f, 0.0f, 0.0f, 0.0f } 
                                       };
        }
    }
}