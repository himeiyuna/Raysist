using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief クォータニオン
    /// </summary>
    public class Quaternion
    {
        /// <summary>
        /// @brief 要素
        /// </summary>
        private float[] elements;
        
        /// <summary>
        /// @brief x要素を取得するプロパティ
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
        /// @brief y要素を取得するプロパティ
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
        /// @brief z要素を取得するプロパティ
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
        /// @brief w要素を取得するプロパティ
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
        /// @brief インデクサ
        /// </summary>
        /// <param name="index">添字</param>
        /// <returns></returns>
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
        /// @brief 単位クォータニオン
        /// </summary>
        public static Quaternion Identity
        {
            get
            {
                return new Quaternion { x = 0.0f, y = 0.0f, z = 0.0f, w = 1.0f };
            }
        }

        /// <summary>
        /// @brief 共役四元数を取得するプロパティ
        /// </summary>
        public Quaternion Conjugate
        {
            get
            {
                return new Quaternion { x = -x, y = -y, z = -z, w = w };
            }
        }

        /// <summary>
        /// @brief 逆クォータニオンを取得する
        /// </summary>
        public Quaternion Inverse
        {
            get
            {
                var length = Length2;
                var conj = Conjugate;
                conj.w /= length;
                conj.x /= length;
                conj.y /= length;
                conj.z /= length;
                return conj;
            }
        }

        /// <summary>
        /// @brief 長さを取得するプロパティ
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(Length2);
            }
        }

        /// <summary>
        /// @brief 長さの２乗を取得するプロパティ
        /// </summary>
        public float Length2 
        { 
            get
            {
                return x * x + y * y + z * z + w * w; 
            }
        }

        /// <summary>
        /// @brief 回転行列
        /// </summary>
        public Matrix RotationMatrix
        {
            get
            {
                var ret = new Matrix();
                ret[0, 0] = 1.0f - 2.0f * y * y - 2.0f * z * z;
                ret[0, 1] = 2.0f * x * y + 2.0f * w * z;
                ret[0, 2] = 2.0f * x * z - 2.0f * w * y;
                ret[0, 3] = 0.0f;

                ret[1, 0] = 2.0f * x * y - 2.0f * w * z;
                ret[1, 1] = 1.0f - 2.0f * x * x - 2.0f * z * z;
                ret[1, 2] = 2.0f * y * z + 2.0f * w * x;
                ret[1, 3] = 0.0f;

                ret[2, 0] = 2.0f * x * z + 2.0f * w * y;
                ret[2, 1] = 2.0f * y * z - 2.0f * w * x;
                ret[2, 2] = 1.0f - 2.0f * x * x - 2.0f * y * y;

                ret[3, 0] = 0.0f;
                ret[3, 1] = 0.0f;
                ret[3, 2] = 0.0f;
                ret[3, 3] = 1.0f;

                return ret;
            }
        }

        /// <summary>
        /// @brief デフォルトコンストラクタ
        /// </summary>
        public Quaternion()
        {
            elements = new float[4] { 0.0f, 0.0f, 0.0f, 0.0f };
        }

   
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="axis">軸ベクトル</param>
        /// <param name="radian">回転量</param>
        public Quaternion(Vector3 axis, float radian) : this()
        {
            var norm = axis.Normalize;

            float sin_t2 = (float)Math.Sin(0.5f * radian);
            float cos_t2 = (float)Math.Cos(0.5f * radian);

            w = cos_t2;
            x = norm.x * sin_t2;
            y = norm.y * sin_t2;
            z = norm.z * sin_t2;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="rotation">回転行列</param>
        public Quaternion(Matrix rotation) : this()
        {
            // 最大成分を検索
            float[] elem = new float[4];
            elem[0] =  rotation[0, 0] - rotation[1, 1] - rotation[2, 2] + 1.0f;
            elem[1] = -rotation[0, 0] + rotation[1, 1] - rotation[2, 2] + 1.0f;
            elem[2] = -rotation[0, 0] - rotation[1, 1] + rotation[2, 2] + 1.0f;
            elem[3] =  rotation[0, 0] + rotation[1, 1] + rotation[2, 2] + 1.0f;

            int biggestIndex = 0;
            for (var i = 0; i < elem.Length; ++i)
            {
                if (elem[i] > elem[biggestIndex])
                    biggestIndex = i;
            }

            if (elem[biggestIndex] < 1.0f)
                return;

            float v = (float)Math.Sqrt(elem[biggestIndex]) * 0.5f;
            this[biggestIndex] = v;
            float mult = 0.25f / v;

            switch (biggestIndex)
            {
                case 0:
                    this[1] = (rotation[0][1] + rotation[1][0] ) * mult;
		            this[2] = (rotation[2][0] + rotation[0][2] ) * mult;
		            this[3] = (rotation[1][2] - rotation[2][1] ) * mult;
                    break;
                case 1:
                    this[0] = (rotation[0][1] + rotation[1][0] ) * mult;
		            this[2] = (rotation[1][2] + rotation[2][1] ) * mult;
		            this[3] = (rotation[2][0] - rotation[0][2] ) * mult;
                    break;
                case 2:
                    this[0] = (rotation[2][0] + rotation[0][2] ) * mult;
		            this[1] = (rotation[1][2] + rotation[2][1] ) * mult;
		            this[3] = (rotation[0][1] - rotation[1][0] ) * mult;
                    break;
                case 3:
                    this[0] = (rotation[1][2] - rotation[2][1] ) * mult;
		            this[1] = (rotation[2][0] - rotation[0][2] ) * mult;
		            this[2] = (rotation[0][1] - rotation[1][0] ) * mult;
                    break;
            }
        }

        /// <summary>
        /// @brief 乗算演算子
        /// </summary>
        /// <param name="left">左辺</param>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return new Quaternion
            {
                x = left.w * right.x + left.x * right.w + left.y * right.z - left.z * right.y, //right.y * left.z - right.z * left.y + right.w * left.x + left.w * right.x,
                y = left.w * right.y - left.x * right.z + left.y * right.w + left.z * right.x, //right.z * left.x - right.x * left.z + right.w * left.y + left.w * right.y,
                z = left.w * right.z + left.x * right.y - left.y * right.x + left.z * right.w, //right.x * left.y - right.y * left.x + right.w * left.z + left.w * right.z,
                w = left.w * right.w - left.x * right.x - left.y * right.y - left.z * right.z
            };
        }

        /// <summary>
        /// @brief 内積を取得する
        /// </summary>
        /// <param name="right">右辺</param>
        /// <returns>計算結果</returns>
        public float Dot(Quaternion right)
        {
            return x * right.x + y + right.y + z * right.z + w * right.w;
        }

        /// <summary>
        /// @brief 正規化する
        /// </summary>
        /// <returns></returns>
        public Quaternion Normalize()
        {
            var length = Length;
            return new Quaternion { x = this.x / length, y = this.y / length, z = this.z / length, w = this.w / length };
        }

        /// <summary>
        /// @brief 球面線形補完する
        /// </summary>
        /// <param name="end">最終地点</param>
        /// <param name="t">補完の早さ</param>
        /// <returns></returns>
        public Quaternion Slerp(Quaternion end, float t)
        {
            Quaternion left = Normalize();
            Quaternion right = end.Normalize();
            float len1 = left.Length;
            float len2 = right.Length;

            if (len1 == 0.0f || len2 == 0.0f)
            {
                return Quaternion.Identity;
            }

            float cos = left.Dot(right) / (len1 * len2);
            if (cos >= 1.0f)
            {
                return this;
            }

            Quaternion ret = new Quaternion();
            float w           = (float)Math.Acos(cos);
            float sin_w       = (float)Math.Sin(w);
            float sin_t_w     = (float)Math.Sin(t * w);
            float sin_inv_t_w = (float)Math.Sin((1.0f - t) * w);
            float mult_q1     = sin_inv_t_w / sin_w;
            float mult_q2     = sin_t_w / sin_w;

            ret.x = mult_q1 * left.x + mult_q2 * right.x;
            ret.y = mult_q1 * left.y + mult_q2 * right.y;
            ret.z = mult_q1 * left.z + mult_q2 * right.z;
            ret.w = mult_q1 * left.w + mult_q2 * right.w;

            return ret;
        }
    }
}
