﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief 後方から出現する敵コンポーネント
    /// </summary>
    class BehindAppearEnemy : GameComponent
    {
        internal const int MaxLife = 100;
        private const int AnimationFrame = 90;

        /// <summary>
        /// @brief 開始地点
        /// </summary>
        private Vector3 From
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 終了地点
        /// </summary>
        private Vector3 To
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 差分
        /// </summary>
        private Vector3 Diff
        {
            set;
            get;
        }

        /// <summary>
        /// @brief フレーム数
        /// </summary>
        private int Frame
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="to">目的地</param>
        public BehindAppearEnemy(GameContainer container, Vector3 from, Vector3 to) : base(container)
        {
            From = from;
            To = to;
            Diff = to - from;

            Position.LocalPosition = from;
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            ++Frame;
            if (Frame > AnimationFrame)
            {
                Active = false;
                return;
            }


            float r = Frame / (float)AnimationFrame;

            Position.LocalPosition = new Vector3() { x = From.x + Diff.x * r, y = Position.LocalPosition.y + (To.y - Position.LocalPosition.y) * 0.05f, z = From.z + Diff.z * r };
        }

        /// <summary>
        /// @brief 非アクティブになったときに呼び出される
        /// </summary>
        public override void OnDisable()
        {
            base.OnDisable();
            Container.GetComponent<RetreatEnemy>().Active = true;
            Container.GetComponent<RectCollider>().Active = true;
        }
    }

    class BehindAppearEnemyFactory : ContainerFactory
    {
        public enum Direction
        {
            LEFT,
            RIGHT
        }

        /// <summary>
        /// @brief 開始地点
        /// </summary>
        private Vector3 From
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 終了地点
        /// </summary>
        private Vector3 To
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 移動速度
        /// </summary>
        private float Speed
        {
            set;
            get;
        }
        
        /// <summary>
        /// @brief プレイヤーの位置
        /// </summary>
        public Player PlayerPosition
        {
            set;
            get;
        }

        /// <summary>
        /// @breif スコアへのポインタ
        /// </summary>
        public ScoreComponent SC
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public BehindAppearEnemyFactory(Vector3 to, Direction dir, float speed, Player pos, ScoreComponent sc)
            : base()
        {
            From = new Vector3 { x = to.x + 300.0f * (dir == Direction.LEFT ? -1 : 1), y = -500.0f, z = 0.0f };
            To = to;
            Speed = speed;
            PlayerPosition = pos;
            SC = sc;
        }

        public override GameContainer Create()
        {
            var gc = new GameContainer();
            gc.AddComponent(new EnemyInformation(gc, BehindAppearEnemy.MaxLife, SC));
            gc.AddComponent(new BehindAppearEnemy(gc, From, To));

            var re = new RetreatEnemy(gc, Speed);
            re.Active = false;
            gc.AddComponent(re);
            
            var rc = new RectCollider(gc, (Collider c) =>
            {
                DX.DrawString(0, 150, "Enemy Hit", DX.GetColor(255, 255, 255));
            });
            rc.Active = false;
            rc.Width = 50.0f;
            rc.Height = 50.0f;
            gc.AddComponent(rc);

            gc.AddComponent(new MeshRenderer(gc, "fighter.x"));

            gc.Position.LocalRotation = new Quaternion(Vector3.AxisX, (float)Math.PI * 0.5f);

            //var vs = new FanShot(gc, (float)Math.PI * 0.5f, From, PlayerPosition);
            //vs.Magazine = 7;
            //var vs = new VerticalShot(gc, 0.5f, From, PlayerPosition);
            

            var vs = new CircleShot(gc, 0.5f, From, PlayerPosition);
            vs.Magazine = 36;



            vs.Speed = 5.0f;
            vs.Count = 60;
            vs.SpaceanInterval = 60;
            vs.AimFlag = false;


            gc.AddComponent(vs);
            return gc;
        }
    }
}
