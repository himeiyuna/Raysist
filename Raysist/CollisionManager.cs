using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    class Collider : GameComponent
    {
        public Collider(GameContainer container) : base(container)
        {
        }

        public override void Update()
        {
 	        
        }
    }

    class CollisionManager
    {
        /// <summary>
        /// @brief 空間を表すクラス
        /// </summary>
        private class Cell
        {
            /// <summary>
            /// @brief 最初の要素
            /// </summary>
            public Element First
            {
                set;
                get;
            }

            /// <summary>
            /// @brief コンストラクタ
            /// </summary>
            public Cell()
            {
            } 
        }

        /// <summary>
        /// @brief 要素を表すクラス
        /// </summary>
        private class Element
        {
            /// <summary>
            /// @brief 所属空間
            /// </summary>
            public Cell Parent
            {
                set;
                get;
            }

            /// <summary>
            /// @brief コライダ
            /// </summary>
            public Collider Object
            {
                set;
                get;
            }

            /// <summary>
            /// @brief 前の要素
            /// </summary>
            public Element Prev
            {
                set;
                get;
            }

            /// <summary>
            /// @brief 次の要素
            /// </summary>
            public Element Next
            {
                set;
                get;
            }

            /// <summary>
            /// @brief コンストラクタ
            /// </summary>
            /// <param name="collider">コライダ</param>
            public Element(Collider collider)
            {
                Object = collider;
            }

            /// <summary>
            /// @brief 自らリストから外れる関数
            /// </summary>
            /// <returns>成功すればtrue</returns>
            public bool Remove()
            {
                return true;
            }

        }

        /// <summary>
        /// @brief 最大分割数
        /// </summary>
        private const int MaxLevel = 8;


        /// <summary>
        /// @brief シングルトンインスタンス
        /// </summary>
        private static CollisionManager instance;
        public static CollisionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CollisionManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        private CollisionManager()
        {
        }
    }
}
