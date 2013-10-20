using System;
using System.Runtime.InteropServices;
using DxLibDLL;
using System.Collections;

namespace Raysist
{
    public class Matrix
    {
        private Vector4[] row = new Vector4[4];

        /// <summary>
        /// @brief 行要素を取得する
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <returns>行</returns>
        public Vector4 this[int index]
        {
            set
            {
                row[index] = value;
            }
            get
            {
                return row[index];
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
                this.row[row][col] = value;
            }
            get
            {
                return this.row[row][col];
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
        /// @brief コンストラクタ
        /// </summary>
        public Matrix()
        {
            row[0] = new Vector4 { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
            row[1] = new Vector4 { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
            row[2] = new Vector4 { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
            row[3] = new Vector4 { x = 0.0f, y = 0.0f, z = 0.0f, w = 0.0f };
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
            ret.row[0] = new Vector4
            {
                x = left[0, 0] * right[0, 0] + left[0, 1] * right[1, 0] + left[0, 2] * right[2, 0] + left[0, 3] * right[3, 0],
                y = left[0, 0] * right[0, 1] + left[0, 1] * right[1, 1] + left[0, 2] * right[2, 1] + left[0, 3] * right[3, 1],
                z = left[0, 0] * right[0, 2] + left[0, 1] * right[1, 2] + left[0, 2] * right[2, 2] + left[0, 3] * right[3, 2],
                w = left[0, 0] * right[0, 3] + left[0, 1] * right[1, 3] + left[0, 2] * right[2, 3] + left[0, 3] * right[3, 3]
            };
            ret.row[1] = new Vector4
            {
                x = left[1, 0] * right[0, 0] + left[1, 1] * right[1, 0] + left[1, 2] * right[2, 0] + left[1, 3] * right[3, 0],
                y = left[1, 0] * right[0, 1] + left[1, 1] * right[1, 1] + left[1, 2] * right[2, 1] + left[1, 3] * right[3, 1],
                z = left[1, 0] * right[0, 2] + left[1, 1] * right[1, 2] + left[1, 2] * right[2, 2] + left[1, 3] * right[3, 2],
                w = left[1, 0] * right[0, 3] + left[1, 1] * right[1, 3] + left[1, 2] * right[2, 3] + left[1, 3] * right[3, 3]
            };
            ret.row[2] = new Vector4
            {
                x = left[2, 0] * right[0, 0] + left[2, 1] * right[1, 0] + left[2, 2] * right[2, 0] + left[2, 3] * right[3, 0],
                y = left[2, 0] * right[0, 1] + left[2, 1] * right[1, 1] + left[2, 2] * right[2, 1] + left[2, 3] * right[3, 1],
                z = left[2, 0] * right[0, 2] + left[2, 1] * right[1, 2] + left[2, 2] * right[2, 2] + left[2, 3] * right[3, 2],
                w = left[2, 0] * right[0, 3] + left[2, 1] * right[1, 3] + left[2, 2] * right[2, 3] + left[2, 3] * right[3, 3]
            };
            ret.row[3] = new Vector4
            {
                x = left[3, 0] * right[0, 0] + left[3, 1] * right[1, 0] + left[3, 2] * right[2, 0] + left[3, 3] * right[3, 0],
                y = left[3, 0] * right[0, 1] + left[3, 1] * right[1, 1] + left[3, 2] * right[2, 1] + left[3, 3] * right[3, 1],
                z = left[3, 0] * right[0, 2] + left[3, 1] * right[1, 2] + left[3, 2] * right[2, 2] + left[3, 3] * right[3, 2],
                w = left[3, 0] * right[0, 3] + left[3, 1] * right[1, 3] + left[3, 2] * right[2, 3] + left[3, 3] * right[3, 3]
            };
            return ret;
        }
    }
}