using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 位置情報や親子関係を管理するクラス
    /// </summary>
    public class Positioner
    {
        private Positioner parent;

        /// <summary>
        /// @brief ローカル座標
        /// </summary>
        public Vector3    LocalPosition { set; get; }

        /// <summary>
        /// @brief 行列
        /// </summary>
        public Matrix     Transform { set; get; }

        /// <summary>
        /// @brief 親の位置
        /// </summary>
        public Positioner Parent
        {
            set
            {
                parent = value;
            }
            get
            {
                return parent;
            }
        }
    }
}
