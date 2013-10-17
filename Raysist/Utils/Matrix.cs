using System;
using System.Runtime.InteropServices;
using DxLibDLL;
using System.Collections;

namespace Raysist
{
    [System.Runtime.InteropServices.StructLayout(LayoutKind.Explicit)]
    public class Matrix
    {
        [System.Runtime.InteropServices.FieldOffset(0)]
        private float[][] elements;

        [System.Runtime.InteropServices.FieldOffset(0)]
        private Vector4[] row;

        [System.Runtime.InteropServices.FieldOffset(0)]
        private DX.MATRIX dxlib;
      
        /// <summary>
        /// @brief 行を取得するプロパティ
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
        /// @brief 要素を取得するプロパティ
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <returns>要素</returns>
        public float this[int row, int col]
        {
            set
            {
                elements[row][col] = value;
            }
            get
            {
                return elements[row][col];
            }
        }

        /// <summary>
        /// 単位行列を取得する関数
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
        /// DxLib用に変換する関数
        /// </summary>
        public DX.MATRIX ToDxLib { get { return dxlib; } }

        /// <summary>
        /// 乗算演算子
        /// </summary>
        /// <param name="left">行列</param>
        /// <param name="right">ベクトル</param>
        /// <returns>計算結果ベクトル</returns>
        public static Vector3 operator *(Matrix left, Vector3 right)
        {
            return new Vector3 { X = left[0, 0] * right.X + left[0, 1] * right.Y + left[0, 2] * right.Z,
                                 Y = left[1, 0] * right.X + left[1, 1] * right.Y + left[1, 2] * right.Z,
                                 Z = left[2, 0] * right.X + left[2, 1] * right.Y + left[2, 2] + right.Z };
        }

        public static Matrix operator *(Matrix left, Matrix right)
        {
            var ret = Matrix.Identity;
            ret.row[0] = new Vector4
            {
                X = left[0, 0] * right[0, 0] + left[0, 1] * right[1, 0] + left[0, 2] * right[2, 0] + left[0, 3] * right[3, 0],
                Y = left[0, 0] * right[0, 1] + left[0, 1] * right[1, 1] + left[0, 2] * right[2, 1] + left[0, 3] * right[3, 1],
                Z = left[0, 0] * right[0, 2] + left[0, 1] * right[1, 2] + left[0, 2] * right[2, 2] + left[0, 3] * right[3, 2],
                W = left[0, 0] * right[0, 3] + left[0, 1] * right[1, 3] + left[0, 2] * right[2, 3] + left[0, 3] * right[3, 3]
            };
            ret.row[1] = new Vector4
            {
                X = left[1, 0] * right[0, 0] + left[1, 1] * right[1, 0] + left[1, 2] * right[2, 0] + left[1, 3] * right[3, 0],
                Y = left[1, 0] * right[0, 1] + left[1, 1] * right[1, 1] + left[1, 2] * right[2, 1] + left[1, 3] * right[3, 1],
                Z = left[1, 0] * right[0, 2] + left[1, 1] * right[1, 2] + left[1, 2] * right[2, 2] + left[1, 3] * right[3, 2],
                W = left[1, 0] * right[0, 3] + left[1, 1] * right[1, 3] + left[1, 2] * right[2, 3] + left[1, 3] * right[3, 3]
            };
            ret.row[2] = new Vector4
            {
                X = left[2, 0] * right[0, 0] + left[2, 1] * right[1, 0] + left[2, 2] * right[2, 0] + left[2, 3] * right[3, 0],
                Y = left[2, 0] * right[0, 1] + left[2, 1] * right[1, 1] + left[2, 2] * right[2, 1] + left[2, 3] * right[3, 1],
                Z = left[2, 0] * right[0, 2] + left[2, 1] * right[1, 2] + left[2, 2] * right[2, 2] + left[2, 3] * right[3, 2],
                W = left[2, 0] * right[0, 3] + left[2, 1] * right[1, 3] + left[2, 2] * right[2, 3] + left[2, 3] * right[3, 3]
            };
            ret.row[3] = new Vector4
            {
                X = left[3, 0] * right[0, 0] + left[3, 1] * right[1, 0] + left[3, 2] * right[2, 0] + left[3, 3] * right[3, 0],
                Y = left[3, 0] * right[0, 1] + left[3, 1] * right[1, 1] + left[3, 2] * right[2, 1] + left[3, 3] * right[3, 1],
                Z = left[3, 0] * right[0, 2] + left[3, 1] * right[1, 2] + left[3, 2] * right[2, 2] + left[3, 3] * right[3, 2],
                W = left[3, 0] * right[0, 3] + left[3, 1] * right[1, 3] + left[3, 2] * right[2, 3] + left[3, 3] * right[3, 3]
            };
            return ret;
        }
    }
}