using DxLibDLL;
using System;
using System.Runtime.InteropServices;


namespace Raysist
{
    class Player : GameComponent
    {
        private const float PlayerWidth = 80.0f;
        private const float PlayerHeight = 80.0f;

        //---------------------------------------------------
        //メンバ変数こっから

        /// <summary>
        /// @brief 体力
        /// </summary>
        public int Life
        {
            private set;
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
        /// @brief 回転量
        /// </summary>
        private int Rot
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 最大回転量
        /// </summary>
        private const int MaxRot = 45;

        //ここまで
        //----------------------------------------------------



        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Player(GameContainer container) : base(container)
        {
            Speed = 5.0f;
            Position.LocalRotation = new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f) * new Quaternion(Vector3.AxisY, (float)Math.PI);
            Position.LocalPosition = new Vector3 { x = 100.0f, y = 0.0f, z = 0.0f };
            Rot = 0;


            var bitmaker = new BitFactory(this, Bit.BitIndex.BIT_LEFT);
            var bitmaker2 = new BitFactory(this, Bit.BitIndex.BIT_RIGHT);

            bitmaker.Create();
            bitmaker2.Create();
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            int dir = 0x0000;

            // 移動処理
            if (DX.CheckHitKey(DX.KEY_INPUT_W) == 1)
            {
                dir |= 0x1000;
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_S) == 1)
            {
                dir |= 0x0010;
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_A) == 1)
            {
                dir |= 0x0100;

                Rot -= 3;
                if (Rot >= -MaxRot)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, -(float)Math.PI / 60);
                }
                else
                {
                    Rot = -MaxRot;
                }
            }
            else if (DX.CheckHitKey(DX.KEY_INPUT_D) == 1)
            {
                dir |= 0x0001;

                Rot += 3;
                if (Rot <= MaxRot)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 60);
                }
                else
                {
                    Rot = MaxRot;
                }
            }
            else
            {
                if (Rot != 0)
                {
                    Position.LocalRotation *= new Quaternion(Vector3.AxisZ, (float)Math.PI / 180 * (Rot < 0 ? 3 : -3));
                    Rot += Rot < 0 ? 3 : -3;
                }
            }

            float theta;
            switch (dir)
            {
                case 0x0001: theta = 0.0f; break;
                case 0x0011: theta = (float)Math.PI * 0.25f; break;
                case 0x0010: theta = (float)Math.PI * 0.5f;  break;
                case 0x0110: theta = (float)Math.PI * 0.75f; break;
                case 0x0100: theta = (float)Math.PI;         break;
                case 0x1100: theta = (float)Math.PI * 1.25f; break;
                case 0x1000: theta = (float)Math.PI * 1.5f;  break;
                case 0x1001: theta = (float)Math.PI * 1.75f; break;
                default: theta = -1.0f; break;
            }

            if (theta >= 0.0f)
            {
                Position.LocalPosition.x += (float)Math.Cos(theta) * Speed;
                Position.LocalPosition.y += -(float)Math.Sin(theta) * Speed;
            }
          
            var pos = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
            if (300.0f + PlayerWidth * 0.5f > pos.x)
            {
                pos.x = 300.0f + PlayerWidth * 0.5f;
                Position.LocalPosition.x = DX.ConvScreenPosToWorldPos(pos).x;
            }
            else if (1300.0f - PlayerWidth * 0.5f < pos.x)
            {
                pos.x = 1300.0f - PlayerWidth * 0.5f;
                Position.LocalPosition.x = DX.ConvScreenPosToWorldPos(pos).x;
            }

            if (PlayerHeight * 0.5f > pos.y)
            {
                pos.y = PlayerHeight * 0.5f;
                Position.LocalPosition.y = DX.ConvScreenPosToWorldPos(pos).y;
            }
            else if (800.0f - PlayerHeight * 0.5f < pos.y)
            {
                pos.y = 800.0f - PlayerHeight * 0.5f;
                Position.LocalPosition.y = DX.ConvScreenPosToWorldPos(pos).y;
            }
            DX.DrawBox(300, 0, 1300, 800,255,0);//有効距離

            // 左ビット射出
            if (DX.CheckHitKey(DX.KEY_INPUT_Z) == 1)
            {
                var bit = Position.FindChildren("BitLeft");
                foreach (var b in bit)
                {
                    b.Container.GetComponent<Bit>().Undock();
                }
            }
            // 右ビット射出
            if (DX.CheckHitKey(DX.KEY_INPUT_X) == 1)
            {
                var bit = Position.FindChildren("BitRight");
                foreach (var b in bit)
                {
                    b.Container.GetComponent<Bit>().Undock();
                }
            }

            if (DX.CheckHitKey(DX.KEY_INPUT_RETURN) == 1)
            {
                var dis = Container.GetComponent<DisablePlayer>();
                dis.Active = true;
            }
        }
    }

    
}
