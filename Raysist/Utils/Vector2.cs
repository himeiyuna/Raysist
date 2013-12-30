using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    class Vector2
    {
        private float[] element;


        public float x
        {
            set
            {
                element[0] = value;
            }
            get
            {
                return element[0];
            }
        }

        public float y
        {
            set
            {
                element[1] = value;
            }
            get
            {
                return element[1];
            }
        }

        public float this[int index]
        {
            set
            {
                element[index] = value;
            }
            get
            {
                return element[index];
            }
        }

        /// <summary>
        /// @brief 長さ
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt((double)Length2);
            }
        }

        /// <summary>
        /// @brief 長さの二乗
        /// </summary>
        public float Length2
        {
            get
            {
                return x * x + y * y;
            }
        }
        
        public Vector2()
        {
            element = new float[2];
        }

        public float Dot(Vector2 right)
        {
            return x * right.x + y * right.y;
        }

        public float Cross(Vector2 right)
        {
            return x * right.y - right.x * y;    
        }

        public float Angle(Vector2 right)
        {
            return (float)Math.Acos(Dot(right) / Length);
        }
    }
}
