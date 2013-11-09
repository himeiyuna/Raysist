using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief カメラクラス
    /// </summary>
    class Camera : GameComponent
    {
        /// <summary>
        /// @brief デフォルトの最近点
        /// </summary>
        public static readonly float DefaultNear = 1.0f;

        /// <summary>
        /// @brief デフォルトの最遠点
        /// </summary>
        public static readonly float DefaultFar  = 10000.0f;

        /// <summary>
        /// @brief デフォルトの視野角
        /// </summary>
        public static readonly float DefaultFOV  = (float)Math.PI * 0.25f;

        /// <summary>
        /// @brief 目線ベクトル
        /// </summary>
        public Vector3 Eye
        {
            get
            {
                return Position.LocalAxisZ;
            }
        }

        /// <summary>
        /// @brief 上ベクトル
        /// </summary>
        public Vector3 Up
        {
            get
            {
                return Position.LocalAxisY;
            }
        }

        /// <summary>
        /// @brief カメラに映り始める距離
        /// </summary>
        public float Near
        {
            set;
            get;
        }

        /// <summary>
        /// @brief カメラに映らなくなる距離
        /// </summary>
        public float Far
        {
            set;
            get;
        }

        /// <summary>
        /// @brief スクリーンの幅
        /// </summary>
        public float ScreenWidth
        {
            set;
            get;
        }

        /// <summary>
        /// @brief スクリーンの高さ
        /// </summary>
        public float ScreenHeight
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 視野角
        /// </summary>
        public float FieldOfView
        {
            set;
            get;
        }

        /// <summary>
        /// @brief ビュー変換行列を取得するプロパティ
        /// </summary>
        public virtual Matrix ViewTransform
        {
            get
            {
                var ret = new Matrix();
                var zaxis = Eye.Normalize;
                var xaxis = Up.Cross(zaxis).Normalize;
                var yaxis = zaxis.Cross(xaxis).Normalize;
                var pos = Position.WorldPosition;

                ret[0, 0] = xaxis.x;
                ret[0, 1] = yaxis.x;
                ret[0, 2] = zaxis.x;
                ret[0, 3] = 0.0f;
                ret[1, 0] = xaxis.y;
                ret[1, 1] = yaxis.y;
                ret[1, 2] = zaxis.y;
                ret[1, 3] = 0.0f;
                ret[2, 0] = xaxis.z;
                ret[2, 1] = yaxis.z;
                ret[2, 2] = zaxis.z;
                ret[2, 3] = 0.0f;
                ret[3, 0] = -xaxis.Dot(pos);
                ret[3, 1] = -yaxis.Dot(pos);
                ret[3, 2] = -zaxis.Dot(pos);
                ret[3, 3] = 1.0f;

                return ret;
            }
        }

        /// <summary>
        /// @brief 射影変換行列を取得するプロパティ
        /// </summary>
        public Matrix ProjectionTransform
        {
            get
            {
                var ret = Matrix.Identity;
                var cot = 1.0f / (float)Math.Tan(FieldOfView * 0.5f);
                var farnear = Far / (Far - Near);

                ret[0, 0] = cot / (ScreenWidth / ScreenHeight);
                ret[1, 1] = cot;
                ret[2, 2] = farnear;
                ret[2, 3] = 1.0f;
                ret[3, 2] = -farnear * Near;
                ret[3, 3] = 0.0f;

                return ret;
            }
        }

        /// <summary>
        /// @brief ビューポート変換行列を取得するプロパティ
        /// </summary>
        public Matrix ViewportTransform
        {
            get
            {
                var ret = Matrix.Identity;
                ret[0, 0] = ScreenWidth  * 0.5f;
                ret[0, 1] = ScreenHeight * 0.5f;
                ret[3, 0] = ScreenWidth  * 0.5f;
                ret[3, 1] = ScreenHeight * 0.5f;

                return ret;
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントを実装するコンテナ</param>
        public Camera(GameContainer container) : base(container)
        {
            Position.LocalPosition.z = -100.0f;
            Near = DefaultNear;
            Far  = DefaultFar;

            // 初期値はウィンドウの幅と高さに設定
            int width, height;
            DX.GetWindowSize(out width, out height);
            ScreenWidth  = (float)width;
            ScreenHeight = (float)height;

            FieldOfView = DefaultFOV;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            DX.SetupCamera_ProjectionMatrix(ProjectionTransform);
            DX.SetCameraViewMatrix(ViewTransform);
        }
    }
}
