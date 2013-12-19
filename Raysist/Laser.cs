using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief レーザークラス
    /// </summary>
    class Laser : GameComponent
    {
        private const int NumDivideLaser = 10;

        //---------------------------------------------------
        //メンバ変数こっから

        /// <summary>
        /// @brief レーザーの軌跡
        /// </summary>
        private Queue<Vector3> Trail
        {
            set;
            get;
        }

        /// <summary>
        /// @brief レーザーの4端点
        /// </summary>
        private Vector3[] DrawCorner
        {
            set;
            get;
        }

        public Vector3 mPos
        {
            set;
            get;
        }
        /// <summary>
        /// @brief レーザーの方向
        /// </summary>
        private float Direction
        {
            set;
            get;

        }
        /// <summary>
        /// @brief レーザーのスピード
        /// </summary>
        private float Speed
        {
            set;
            get;

        }
        /// <summary>
        /// @brief レーザー画像を分割した配列
        /// </summary>
        private int[] LaserPiece
        {
            set;
            get;
        }

        /// <summary>
        /// @brief レーザー画像の高さ
        /// </summary>
        private int Height
        {
            set;
            get;
        }
        /// <summary>
        /// @brief レーザー画像の幅
        /// </summary>
        private int Width
        {
            set;
            get;
        }
        /// <summary>
        /// @brief レーザーの状態
        /// </summary>
        private int State
        {
            set;
            get;
        }

        private int GraphicHandle
        {
            set;
            get;
        }

        //ここまで
        //----------------------------------------------------


        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public Laser(GameContainer container, Vector3 pos, float direction, float speed)
            : base(container)
        {
            Position.LocalPosition = pos;
            Direction = direction;
            Speed = speed;
            State = 0;
            Trail = new Queue<Vector3>();
            var s_pos = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
            DrawCorner = new Vector3[4] { Vector3.ToVector3(ref s_pos), Vector3.ToVector3(ref s_pos), Vector3.ToVector3(ref s_pos), Vector3.ToVector3(ref s_pos) };

            GraphicHandle = DX.LoadGraph("Resources\\dummy.png");
            int w = 0, h = 0;
            DX.GetGraphSize(GraphicHandle, out w, out h);
            Height = h;
            Width = w;


            //画像の高さから分割数を計算
            int nPiece = Height / NumDivideLaser;

            //分割数の分だけ配列を確保し、分割したレーザー画像ハンドルを格納
            LaserPiece = new int[nPiece];
            for (int i = 0; i < nPiece; i++)
            {
                LaserPiece[i] = DX.DerivationGraph(0, i * NumDivideLaser, Width, NumDivideLaser, GraphicHandle);
            }
        }

        /// <summary>
        /// @brief 更新
        /// </summary>
        public override void Update()
        {
            Vector3 memPoint = Position.LocalPosition;
            //Trail.Enqueue(memPoint);
            //Trail.Enqueue(memPoint);
            switch (State)
            {
                case 0:
                    State = 1;
                    break;
                case 1:

                    Position.LocalPosition.x += (float)Math.Cos(Direction) * Speed;
                    Position.LocalPosition.y -= (float)Math.Sin(Direction) * Speed;

                   // if (Trail.Count != 0)

                    if ((int)(Trail.Count * NumDivideLaser) > Height)
                        {
                            Trail.Dequeue();
                        }

                        //ゲーム領域から出ると軌跡を消していく
                        var pos = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
                        if (300.0f > pos.x)
                        {
                            // TODO:外に出たら破棄する
                            GameContainer.Destroy(Container);
                        }
                        else if (1300.0f < pos.x)
                        {
                            // TODO:外に出たら破棄する
                            GameContainer.Destroy(Container);
                        }

                        if (0.0f > pos.y)
                        {
                            // TODO:外に出たら破棄する
                            GameContainer.Destroy(Container);
                        }
                        else if (800.0f < pos.y)
                        {
                            // TODO:外に出たら破棄する
                            GameContainer.Destroy(Container);
                        }
                        else
                        {
                            Trail.Enqueue(new Vector3 { x = pos.x, y = pos.y, z = pos.z });
                        }
                    

                    //軌跡がなくなったら実体を消す
                    if (Trail.Count == 0)
                    {
                        State = 2;
                    }

                    break;
                case 2:
                    // TODO:外に出たら破棄する
                    GameContainer.Destroy(Container);
                    break;
            }

            int index = 0;
            int trailSize = Trail.Count;				//軌跡の数(軌跡の分だけループを回す)
            int nPiece = Height / 50;	//分割数

            //加算ブレンドモードに変更
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ADD, 255);

            int j = 0;
            var a = Trail.ToList();
            var e2 = Trail.GetEnumerator();
            if (e2.MoveNext())
            {
                var e = Trail.GetEnumerator();
                e.MoveNext();

                //軌跡を全て参照したら終わる
                while (e2.MoveNext())
                {
                    //二つの軌跡間の角度を求める
                    float drawAngle = (float)(GetAngle(e.Current, e2.Current) + Math.PI * 0.5f);

                    //軌跡の参照が一度目なら他の端点と同じように計算し代入する
                    //違うなら前の軌跡に合わせる
                    if (e.Current == Trail.First())
                    {
                        DrawCorner[0] = new Vector3 { x = DrawCorner[3].x, y = DrawCorner[3].y, z = DrawCorner[3].z };
                        DrawCorner[1] = new Vector3 { x = DrawCorner[2].x, y = DrawCorner[2].y, z = DrawCorner[2].z };
                    }
                    else
                    {
                        DrawCorner[0].x = (float)(e.Current.x - Math.Cos(drawAngle) * Width * 0.5f);
                        DrawCorner[0].y = (float)(e.Current.y + Math.Sin(drawAngle) * Width * 0.5f);
                        DrawCorner[1].x = (float)(e.Current.x + Math.Cos(drawAngle) * Width * 0.5f);
                        DrawCorner[1].y = (float)(e.Current.y - Math.Sin(drawAngle) * Width * 0.5f);
                    }

                    DrawCorner[2].x = (float)(e2.Current.x - Math.Cos(drawAngle) * Width * 0.5f);
                    DrawCorner[2].y = (float)(e2.Current.y - Math.Sin(drawAngle) * Width * 0.5f);
                    DrawCorner[3].x = (float)(e2.Current.x + Math.Cos(drawAngle) * Width * 0.5f);
                    DrawCorner[3].y = (float)(e2.Current.y + Math.Sin(drawAngle) * Width * 0.5f);

                    index = (int)((nPiece - 1) * j / trailSize);

                    DX.DrawModiGraphF(
                        DrawCorner[0].x, DrawCorner[0].y,
                        DrawCorner[1].x, DrawCorner[1].y,
                        DrawCorner[2].x, DrawCorner[2].y,
                        DrawCorner[3].x, DrawCorner[3].y,
                        LaserPiece[index], 0);
                    if (Trail.Count == ++j)
                        break;
                    e.MoveNext();
                }
            }

            //ブレンドモードをデフォルトに戻す
            DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 255);
        }


        public float GetAngle(Vector3 from, Vector3 to)
        {
            return (float)Math.Atan2(to.y - from.y, to.x - from.x);
        }






    }
}
/* var te = new ContainerFactory((GameContainer g) =>
                    {
                        g.AddComponent(new Laser(g,Position.WorldPosition, 10.0f, 1.0f));

                    });

                   te.Create(Game.Instance.SceneController.CurrentScene.Root);*/