using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief ゲームパッド入力処理管理クラス
    /// </summary>
    class InputController
    {
        public XGameController XController
        {
            private set;
            get;
        }


        public InputController()
        {
            DX.XINPUT_STATE buf;
            if (DX.GetJoypadXInputState(DX.DX_INPUT_PAD1, out buf) == 0)
            {
                XController = new XGameController();
            }
        }

        public void Update()
        {
            if (XController != null)
            {
                XController.Update();
            }
        }
    }

    class XGameController
    {
        private Vector2 RIGHT
        {
            set;
            get;
        }

        private Vector2 RIGHTDOWN
        {
            set;
            get;
        }

        private Vector2 DOWN
        {
            set;
            get;
        }

        private Vector2 LEFTDOWN
        {
            set;
            get;
        }

        private Vector2 LEFT
        {
            set;
            get;
        }

        private Vector2 LEFTUP
        {
            set;
            get;
        }

        private Vector2 UP
        {
            set;
            get;
        }

        private Vector2 RIGHTUP
        {
            set;
            get;
        }

        /// <summary>
        /// @brief トリガー
        /// </summary>
        public enum Trigger : int
        {
            LEFT,
            RIGHT
        }

        /// <summary>
        /// @brief ボタン
        /// </summary>
        public enum Button : int
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            START,
            BACK,
            LEFT_STICK_PUSH,
            RIGHT_STICK_PUSH,
            LEFT_SHOULDER,
            RIGHT_SHOULDER,
            A,
            B,
            X,
            Y,
            BUTTON_NUM
        }

        public enum StickDirection : int
        {
            NONE = -1,
            RIGHT,
            RIGHTDOWN,
            DOWN,
            LEFTDOWN,
            LEFT,
            LEFTUP,
            UP,
            RIGHTUP
        }

        /// <summary>
        /// @brief 入力状態
        /// </summary>
        private DX.XINPUT_STATE InputState
        {
            set;
            get;
        }

        /// <summary>
        /// @brief パッドボタンの押下フレーム数の配列
        /// </summary>
        private int[] ButtonInputBuffer
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 左スティックのx軸入力状態(-32768~32767)
        /// </summary>
        public short InputLeftStickX
        {
            get
            {
                return InputState.ThumbLX;
            }
        }

        /// <summary>
        /// @brief 左スティックのy軸入力状態(-32768~32767)
        /// </summary>
        public short InputLeftStickY
        {
            get
            {
                return InputState.ThumbLY;
            }
        }

        /// <summary>
        /// @brief 右スティックのx軸入力状態(-32768~32767)
        /// </summary>
        public short InputRightStickX
        {
            get
            {
                return InputState.ThumbRX;
            }
        }

        /// <summary>
        /// @brief 右スティックのy軸入力状態(-32768~32767)
        /// </summary>
        public short InputRightStickY
        {
            get
            {
                return InputState.ThumbRY;
            }
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        internal XGameController()
        {
            ButtonInputBuffer = new int[(int)Button.BUTTON_NUM];

            RIGHT = new Vector2 { x = 1.0f, y = 0.0f };
            RIGHTDOWN = new Vector2 { x = 0.70710678118f, y = 0.70710678118f };
            DOWN = new Vector2 { x = 0.0f, y = 1.0f };
            LEFTDOWN = new Vector2 { x = -0.70710678118f, y = 0.70710678118f };
            LEFT = new Vector2 { x = -1.0f, y = 0.0f };
            LEFTUP = new Vector2 { x = -0.70710678118f, y = -0.70710678118f };
            UP = new Vector2 { x = 0.0f, y = -1.0f };
            RIGHTUP = new Vector2 { x = 0.70710678118f, y = -0.70710678118f };
        }
        
        /// <summary>
        /// @brief トリガー判定
        /// </summary>
        /// <param name="type">左トリガーか右トリガーか</param>
        /// <param name="value">押し込み具合(0~255)</param>
        /// <returns></returns>
        public bool IsPullTrigger(Trigger type, byte value)
        {
            if (type == Trigger.LEFT)
            {
                return InputState.LeftTrigger >= value;
            }
            else
            {
                return InputState.RightTrigger >= value;
            }
        }

        /// <summary>
        /// @brief ボタン判定(押した瞬間のみ)
        /// </summary>
        /// <param name="button">ボタンの種類</param>
        /// <returns>押されていたらtrue</returns>
        public bool IsPushButtonOnce(Button button)
        {
            return ButtonInputBuffer[(int)button] == 1;
        }

        /// <summary>
        /// @brief ボタン判定(押した瞬間と指定したフレーム間隔)
        /// </summary>
        /// <param name="button"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        public bool IsPushButton(Button button, int interval = 0)
        {
            return IsPushButtonOnce(button) ? true : (ButtonInputBuffer[(int)button] - 1) % interval == 0;
        }

        /// <summary>
        /// @brief スティックの8方向判定
        /// </summary>
        /// <param name="isLeft"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public StickDirection GetStick8Direction(bool isLeft, uint value)
        {
            Vector2 dir;

            if (isLeft)
            {
                dir = new Vector2 { x = InputLeftStickX, y = -InputLeftStickY };
            }
            else
            {
                dir = new Vector2 { x = InputRightStickX, y = -InputRightStickY };
            }

            if (dir.Length < value)
            {
                return StickDirection.NONE;
            }

            dir = dir.Normalize();

            float r_cross = dir.Cross(RIGHT);
            float rd_cross = dir.Cross(RIGHTDOWN);
            float d_cross = dir.Cross(DOWN);
            float ld_cross = dir.Cross(LEFTDOWN);

            bool r = false;
            bool rd = false;
            bool d = false;
            bool ld = false;

            // 最低の外積値が一番近い方向
            float min = float.MaxValue;
            if (min > Math.Abs(r_cross))
            {
                r = true;
                min = Math.Abs(r_cross);
            }

            if (min > Math.Abs(rd_cross))
            {
                r = false;
                rd = true;
                min = Math.Abs(rd_cross);
            }

            if (min > Math.Abs(d_cross))
            {
                r = false;
                rd = false;
                d = true;
                min = Math.Abs(d_cross);
            }

            if (min > Math.Abs(ld_cross))
            {
                r = false;
                rd = false;
                d = false;
                ld = true;
                min = Math.Abs(ld_cross);
            }

            if (r)
            {
                return dir.Dot(RIGHT) > 0 ? StickDirection.RIGHT : StickDirection.LEFT;
            }
            else if (rd)
            {
                return dir.Dot(RIGHTDOWN) > 0 ? StickDirection.RIGHTDOWN : StickDirection.LEFTUP;
            }
            else if (d)
            {
                return dir.Dot(DOWN) > 0 ? StickDirection.DOWN : StickDirection.UP;
            }
            else
            {
                return dir.Dot(LEFTDOWN) > 0 ? StickDirection.LEFTDOWN : StickDirection.RIGHTUP;
            }
        }


        /// <summary>
        /// @brief 更新処理
        /// </summary>
        internal void Update()
        {
            DX.XINPUT_STATE buf;
            DX.GetJoypadXInputState(DX.DX_INPUT_PAD1, out buf);

            InputState = buf;

            // UP
            if (buf.Buttons0 != 0)
            {
                ++ButtonInputBuffer[0];
            }
            else
            {
                ButtonInputBuffer[0] = 0;
            }

            // DOWN
            if (buf.Buttons1 != 0)
            {
                ++ButtonInputBuffer[1];
            }
            else
            {
                ButtonInputBuffer[1] = 0;
            }

            // LEFT
            if (buf.Buttons2 != 0)
            {
                ++ButtonInputBuffer[2];
            }
            else
            {
                ButtonInputBuffer[2] = 0;
            }

            // RIGHT
            if (buf.Buttons3 != 0)
            {
                ++ButtonInputBuffer[3];
            }
            else
            {
                ButtonInputBuffer[3] = 0;
            }

            // START
            if (buf.Buttons4 != 0)
            {
                ++ButtonInputBuffer[4];
            }
            else
            {
                ButtonInputBuffer[4] = 0;
            }

            // BACK
            if (buf.Buttons5 != 0)
            {
                ++ButtonInputBuffer[5];
            }
            else
            {
                ButtonInputBuffer[5] = 0;
            }

            // LEFT_THUMB
            if (buf.Buttons6 != 0)
            {
                ++ButtonInputBuffer[6];
            }
            else
            {
                ButtonInputBuffer[6] = 0;
            }

            // RIGHT_THUMB
            if (buf.Buttons7 != 0)
            {
                ++ButtonInputBuffer[7];
            }
            else
            {
                ButtonInputBuffer[7] = 0;
            }

            // LEFT_SHOULDER
            if (buf.Buttons8 != 0)
            {
                ++ButtonInputBuffer[8];
            }
            else
            {
                ButtonInputBuffer[8] = 0;
            }

            // RIGHT_SHOULDER
            if (buf.Buttons9 != 0)
            {
                ++ButtonInputBuffer[9];
            }
            else
            {
                ButtonInputBuffer[9] = 0;
            }

            // A
            if (buf.Buttons10 != 0)
            {
                ++ButtonInputBuffer[10];
            }
            else
            {
                ButtonInputBuffer[10] = 0;
            }

            // B
            if (buf.Buttons11 != 0)
            {
                ++ButtonInputBuffer[11];
            }
            else
            {
                ButtonInputBuffer[11] = 0;
            }

            // X
            if (buf.Buttons12 != 0)
            {
                ++ButtonInputBuffer[12];
            }
            else
            {
                ButtonInputBuffer[12] = 0;
            }

            // Y
            if (buf.Buttons13 != 0)
            {
                ++ButtonInputBuffer[13];
            }
            else
            {
                ButtonInputBuffer[13] = 0;
            }
        }
    }
}
