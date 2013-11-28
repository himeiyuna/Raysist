﻿using System;
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
                    LocalRotation = value.WorldRotation.Inverse * WorldRotation;
                    LocalPosition = WorldPosition - value.WorldPosition;
                    LocalScale = WorldScale - value.WorldScale;
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
                return new Vector3 { x = WorldTransform[0].Length, y = WorldTransform[1].Length, z = WorldTransform[3].Length };
            }
        }

        /// <summary>
        /// @brief ワールドの回転量を取得するプロパティ
        /// </summary>
        public Quaternion WorldRotation
        {
            get
            {
                return Parent == null ? LocalRotation : LocalRotation * Parent.WorldRotation;
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
        /// @brief 子を検索する関数
        /// </summary>
        /// <param name="key">子の参照</param>
        public List<Positioner> Find(String key)
        {
            return (from child in Children where child.Container.Name == key select child).ToList<Positioner>();
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
