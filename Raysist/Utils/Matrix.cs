using System;
using System.Runtime.InteropServices;
using DxLibDLL;

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
        private static Matrix Identity
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
        private DX.MATRIX ToDxLib { get { return dxlib; } }
    }
}