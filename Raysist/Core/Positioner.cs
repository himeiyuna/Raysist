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
    public sealed class Positioner
    {
        /// <summary>
        /// @brief 親
        /// </summary>
        private Positioner parent;

        /// <summary>
        /// @brief 子供
        /// </summary>
        private List<Positioner> Children
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 親
        /// </summary>
        public Positioner Parent
        {
            set
            {
                value.Children.Add(this);

                if (parent != null)
                {
                    parent.RemoveChild(this);

                    // ローカルパラメータをワールドパラメータに変換
                    LocalScale = WorldScale;

                    var ws = value.WorldScale;
                    LocalScale.x /= ws.x;
                    LocalScale.y /= ws.y;
                    LocalScale.z /= ws.z;

                    LocalPosition = WorldPosition - value.WorldPosition;
                    LocalRotation = value.WorldRotation * WorldRotation;
                }

                parent = value;
            }
            get
            {
                return parent;
            }
        }

        /// <summary>
        /// @brief 自身を所持しているコンテナ
        /// </summary>
        public GameContainer Container
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief ローカル座標を取得するプロパティ
        /// </summary>
        public Vector3 LocalPosition
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ローカルのスケールを取得するプロパティ
        /// </summary>
        public Vector3 LocalScale
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ローカルの回転量を取得するプロパティ
        /// </summary>
        public Quaternion LocalRotation
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ローカル変換行列を取得するプロパティ
        /// </summary>
        public Matrix LocalTransform
        {
            get
            {
                var ret = LocalRotation.RotationMatrix;
                ret[0] *= LocalScale.x;
                ret[1] *= LocalScale.y;
                ret[2] *= LocalScale.z;
                ret[3, 0] = LocalPosition.x;
                ret[3, 1] = LocalPosition.y;
                ret[3, 2] = LocalPosition.z;

                return ret;
            }
        }

        /// <summary>
        /// @brief ローカルのx軸
        /// </summary>
        public Vector3 LocalAxisX
        {
            get
            {
                return new Vector3(LocalRotation.RotationMatrix[0].ToArray());
            }
        }

        /// <summary>
        /// @brief ローカルのy軸
        /// </summary>
        public Vector3 LocalAxisY
        {
            get
            {
                return new Vector3(LocalRotation.RotationMatrix[1].ToArray());
            }
        }

        /// <summary>
        /// @brief ローカルのz軸
        /// </summary>
        public Vector3 LocalAxisZ
        {
            get
            {
                return new Vector3(LocalRotation.RotationMatrix[2].ToArray());
            }
        }

        /// <summary>
        /// @brief ワールドのスケール取得するプロパティ
        /// </summary>
        public Vector3 WorldScale
        {
            get
            {
                var mat = WorldTransform;
                return new Vector3 { x = WorldTransform[0].Length, y = WorldTransform[1].Length, z = WorldTransform[2].Length };
            }
        }

        /// <summary>
        /// @brief ワールドの回転量を取得するプロパティ
        /// </summary>
        public Quaternion WorldRotation
        {
            get
            {
                return Parent == null ? LocalRotation : Parent.WorldRotation * LocalRotation;
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得するプロパティ
        /// </summary>
        public Matrix WorldTransform
        {
            get
            {
                // 親がいなくなるまで再帰を続ける
                return Parent == null ? LocalTransform : LocalTransform * Parent.WorldTransform;
            }
        }

        /// <summary>
        /// @brief ローカルのx軸
        /// </summary>
        public Vector3 WorldAxisX
        {
            get
            {
                return new Vector3(WorldRotation.RotationMatrix[0].ToArray());
            }
        }

        /// <summary>
        /// @brief ローカルのy軸
        /// </summary>
        public Vector3 WorldAxisY
        {
            get
            {
                return new Vector3(WorldRotation.RotationMatrix[1].ToArray());
            }
        }

        /// <summary>
        /// @brief ローカルのz軸
        /// </summary>
        public Vector3 WorldAxisZ
        {
            get
            {
                return new Vector3(WorldRotation.RotationMatrix[2].ToArray());
            }
        }

        /// <summary>
        /// @brief ワールド変換行列を取得するプロパティ
        /// </summary>
        public Vector3 WorldPosition
        {
            get
            {
                return new Vector3(WorldTransform[3].ToArray());
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Positioner(GameContainer container)
        {
            Container = container;
            Children = new List<Positioner>();
            LocalPosition = new Vector3();
            LocalScale    = new Vector3 { x = 1.0f, y = 1.0f, z = 1.0f };
            LocalRotation = Quaternion.Identity;
        }

        /// <summary>
        /// @brief コンテナ名から子を検索する関数
        ///        同名のものがあった場合は先に見つかったものを返す
        /// </summary>
        /// <param name="name">コンテナ名</param>
        /// <returns>子</returns>
        public Positioner FindChild(string name)
        {
            foreach (var child in Children)
            {
                if (child.Container.Name == name) 
                {
                    return child;
                }
            }
            return null;
        }

        /// <summary>
        /// @brief コンテナ名から子を検索する関数
        ///        FindChildの複数要素検索版
        /// </summary>
        /// <param name="name">コンテナ名</param>
        /// <returns>子の配列</returns>
        public List<Positioner> FindChildren(string name)
        {
            return (from child in Children where child.Container.Name == name select child).ToList();
        }

        /// <summary>
        /// @brief 子を削除する
        /// </summary>
        /// <param name="p"></param>
        public void RemoveChild(Positioner p)
        {
            Children.Remove(p);
        }

        /// <summary>
        /// @brief イテレータを返す
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Positioner> GetIterator()
        {
            return Children;
        }
    }
}
