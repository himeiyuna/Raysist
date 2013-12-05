using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    abstract class Collider : GameComponent
    {
        /// <summary>
        /// @brief 境界ボックス
        /// </summary>
        public class AABB
        {
            public float Left { set; get; }
            public float Top { set; get; }
            public float Right { set; get; }
            public float Bottom { set; get; }
        }

        /// <summary>
        /// @brief 境界範囲
        /// </summary>
        public abstract AABB BoundingBox
        {
            get;
        }

        /// <summary>
        /// @brief 衝突時に呼び出すメソッド
        /// </summary>
        public Action<Collider> OnCollision
        {
            set;
            internal get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">コンテナ</param>
        public Collider(GameContainer container, Action<Collider> onCollision) : base(container)
        {
            OnCollision = onCollision;
        }

        /// <summary>
        /// @brief 衝突処理
        /// </summary>
        /// <param name="target">衝突処理対象</param>
        /// <returns>衝突していればtrue</returns>
        internal abstract bool IsCollision(Collider target);

        /// <summary>
        /// @brief 衝突処理
        /// </summary>
        /// <param name="target">衝突処理対象</param>
        /// <returns>衝突していればtrue</returns>
        internal abstract bool IsCollision(RectCollider target);

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            // 空間に登録
            Game.Instance.SceneController.CurrentScene.CollisionManager.Regist(this);
        }
    }

    /// <summary>
    /// @brief 平面の箱のコライダ
    /// </summary>
    class RectCollider : Collider
    {
        /// <summary>
        /// @brief 境界範囲
        /// </summary>
        public override Collider.AABB BoundingBox
        {
            get
            {
                var pos = DX.ConvWorldPosToScreenPos(Position.WorldPosition.ToDxLib);
                var scale = Container.Position.WorldScale;
                var ret = new Collider.AABB();

                ret.Left   = pos.x - Width * scale.x * 0.5f;
                ret.Right  = pos.x + Width * scale.x * 0.5f;
                ret.Top    = pos.y - Height * scale.y * 0.5f;
                ret.Bottom = pos.y + Height * scale.y * 0.5f;

                return ret;
            }
            
        }

        /// <summary>
        /// @brief 幅
        /// </summary>
        public float Width
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 高さ
        /// </summary>
        public float Height
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を所持するコンテナ</param>
        public RectCollider(GameContainer container, Action<Collider> onCollision)
            : base(container, onCollision)
        {
            Width = 1.0f;
            Height = 1.0f;
        }

        /// <summary>
        /// @brief 衝突処理
        /// </summary>
        /// <param name="target">衝突処理対象</param>
        /// <returns>衝突していればtrue</returns>
        internal override bool IsCollision(Collider target)
        {
            return target.IsCollision(this);
        }

        /// <summary>
        /// @brief 衝突処理
        /// </summary>
        /// <param name="target">衝突処理対象</param>
        /// <returns>衝突していればtrue</returns>
        internal override bool IsCollision(RectCollider target)
        {
            var s = BoundingBox;
            var t = target.BoundingBox;
            return s.Left < t.Right && s.Right > t.Left && s.Top < t.Bottom && s.Bottom > t.Top;
        }


        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public override void Update()
        {
            DX.DrawBox(1, 800, 10, 810, DX.GetColor(255,255,255), DX.TRUE);
            DX.DrawBox((int)BoundingBox.Left, (int)BoundingBox.Top, (int)BoundingBox.Right, (int)BoundingBox.Bottom, DX.GetColor(255,255,255), DX.FALSE);

            base.Update();
        }
    }

    /// <summary>
    /// @brief 衝突処理管理クラス
    /// </summary>
    class CollisionManager
    {
        /// <summary>
        /// @brief 衝突判定する組み合わせ
        /// </summary>
        private class CollisionPair
        {
            /// <summary>
            /// @brief 衝突の組み合わせ
            /// </summary>
            public Collider[] Pair
            {
                set;
                get;
            }

            /// <summary>
            /// @brief コンストラクタ
            /// </summary>
            /// <param name="left"></param>
            /// <param name="right"></param>
            public CollisionPair(Collider left, Collider right)
            {
                Pair = new Collider[2];
                Pair[0] = left;
                Pair[1] = right;
            }
        }

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

            /// <summary>
            /// @brief リンクをすべてリセットする関数
            /// </summary>
            /// <param name="elem">要素</param>
            public void ResetLink(Element elem)
            {
                if (elem.Next != null)
                {
                    ResetLink(elem.Next);
                }
                elem = null;
            }

            /// <summary>
            /// @brief オブジェクトをプッシュする関数
            /// </summary>
            /// <param name="elem">要素</param>
            /// <returns>成功すればtrue</returns>
            public bool Push(Element elem)
            {
                if (elem == null) return false;
                if (elem.Parent == this) return false;
                if (First == null)
                {
                    First = elem;
                }
                else
                {
                    elem.Next  = First;
                    First.Prev = elem;
                    First      = elem;
                }

                elem.Parent = this; // 空間を登録

                return true;
            }

            /// <summary>
            /// @brief 削除されるオブジェクトをチェック
            /// </summary>
            /// <param name="elem">削除される要素</param>
            /// <returns>成功すればtrue</returns>
            public bool OnRemove(Element elem)
            {
                // 離脱するオブジェクトが先頭オブジェクトなら
                if (elem == First)
                {
                    if (First != null)
                    {
                        // 先頭オブジェクト変更する
                        First = elem.Next;
                    }
                }

                return true;
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
                // すでに離脱しているときは処理終了
                if (Parent == null) return false;

                // 自分を登録している空間に自分離脱を通知
                if (!Parent.OnRemove(this)) return false;

                // 離脱処理
                // 前後のオブジェクト連結
                if (Prev != null)
                {
                    Prev.Next = Next;
                }
                if (Next != null)
                {
                    Next.Prev = Prev;
                }

                Prev = Next = null;
                Parent = null;

                return true;
            }

        }

        /// <summary>
        /// @brief 最大分割数
        /// </summary>
        private const int MaxLevel = 9;

        /// <summary>
        /// @brief 左上のx座標
        /// </summary>
        private float Left
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 左上のy座標
        /// </summary>
        private float Top
        {
            set;
            get;
        }

        
        /// <summary>
        /// @brief 右下のx座標
        /// </summary>
        private float Width
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 右下のy座標
        /// </summary>
        private float Height
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 1レベル分の幅
        /// </summary>
        private float UnitWidth
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 1レベル分の高さ
        /// </summary>
        private float UnitHeight
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 分割レベル
        /// </summary>
        private uint Level
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 階乗
        /// </summary>
        private uint[] Power
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 空間の数
        /// </summary>
        private ulong CellNum
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 空間の配列
        /// </summary>
        private Cell[] CellArray
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 要素の配列
        /// </summary>
        private List<Element> ElementList
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 衝突リスト
        /// </summary>
        private List<CollisionPair> CollisionList
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        public CollisionManager()
        {
            ElementList = new List<Element>();
            CollisionList = new List<CollisionPair>();

            Power = new uint[MaxLevel + 1];

            Power[0] = 1;
            for (var i = 1; i < MaxLevel + 1; ++i)
            {
                Power[i] = Power[i - 1] * 4;
            }

            // 空間の配列を作成
            CellNum = (Power[8 + 1] - 1) / 3;
            CellArray = new Cell[CellNum];

            // 領域を計算
            Left = -10.0f;
            Top = -10.0f;
            Width = 1610.0f - Left;
            Height = 910.0f - Top;
            UnitWidth = Width / (1 << MaxLevel);
            UnitHeight = Height / (1 << MaxLevel);

            Level = 8;
        }

        /// <summary>
        /// @brief 初期化処理
        /// </summary>
        /// <param name="left">当たり判定領域の左</param>
        /// <param name="top">当たり判定領域の上</param>
        /// <param name="right">当たり判定領域の右</param>
        /// <param name="bottom">当たり判定領域の下</param>
        public void Initialize(float left, float top, float right, float bottom)
        {
           
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public void Update()
        {
            // 空間に登録するのは各コライダのUpdateで行う
            // このメソッドの最後で空間の掃除を行う

            // 衝突リストの作成
            var list = GetAllCollisionList();
            foreach (var pair in list)
            {
                // 衝突していれば
                if (pair.Pair[0].IsCollision(pair.Pair[1]))
                {
                    // 衝突時の処理
                    pair.Pair[0].OnCollision(pair.Pair[1]);
                    pair.Pair[1].OnCollision(pair.Pair[0]);
                }
            }

            // 空間から要素を削除する
            foreach (var elem in ElementList)
            {
                elem.Remove();
            }
            ElementList.Clear();
        }

        /// <summary>
        /// @brief 衝突リストを作成する
        /// </summary>
        /// <returns></returns>
        private List<CollisionPair> GetAllCollisionList()
        {
            // リストの初期化
            CollisionList.Clear();

            // ルート空間の存在をチェック
            if (CellArray[0] == null) return new List<CollisionPair>();

            // ルート空間を処理
            Stack<Collider> stac = new Stack<Collider>();
            GetCollisionList(0, ref stac);

            return CollisionList;
        }

        /// <summary>
        /// @brief 
        /// </summary>
        /// <param name="mortonNum"></param>
        /// <param name="colStac"></param>
        /// <returns></returns>
        private bool GetCollisionList(ulong mortonNum, ref Stack<Collider> colStac)
        {
            // 同空間内のオブジェクト同士の衝突リストの作成
            Element elem1 = CellArray[mortonNum].First;
            while (elem1 != null)
            {
                Element elem2 = elem1.Next;
                while (elem2 != null)
                {
                    CollisionList.Add(new CollisionPair(elem1.Object,elem2.Object));
                    elem2 = elem2.Next; // 次の要素へ
                }

                // 衝突スタック(親空間に所属するオブジェクトのリスト)との衝突リスト作成
                foreach (var it in colStac)
                {
                    CollisionList.Add(new CollisionPair(elem1.Object, it));
                }
                elem1 = elem1.Next; // 次の要素へ
            }

            bool isChildRegist = false;

            // 子空間に移動
            ulong objNum = 0, nextMortonNum;
            for (ulong i = 0; i < 4; ++i)
            {
                nextMortonNum = mortonNum * 4 + 1 + i;
                if (nextMortonNum < CellNum && CellArray[mortonNum * 4 + 1 + i] != null)
                {
                    // 要素がスタックに登録済みでなければ
                    if (!isChildRegist)
                    {
                        // 登録オブジェクトをスタックに追加
                        elem1 = CellArray[mortonNum].First;
                        while (elem1 != null)
                        {
                            colStac.Push(elem1.Object);
                            ++objNum;
                            elem1 = elem1.Next; // 次の要素へ
                        }
                    }
                    isChildRegist = true;
                    GetCollisionList(mortonNum * 4 + 1 + i, ref colStac); // 子空間へ
                }
            }

            // スタックからオブジェクトをはずす
            if (isChildRegist)
            {
                for (ulong i = 0; i < objNum; ++i)
                {
                    colStac.Pop();
                }
            }

            return true;
        }

        /// <summary>
        /// @brief 要素を登録する
        /// </summary>
        /// <param name="elem">登録するコライダ</param>
        /// <returns>成功すればtrue</returns>
        internal bool Regist(Collider elem)
        {
            Collider.AABB aabb = elem.BoundingBox;
            // オブジェクトの境界範囲からモートン番号を算出
            ulong mortonNum = GetMortonNumber(ref aabb);

            // モートン番号が領域をはみ出していなければ
            if (mortonNum < CellNum)
            {
                // 空間が存在していなければ作成する
                if (CellArray[mortonNum] == null)
                {
                    CreateNewCell(mortonNum);
                }
                var e = new Element(elem);

                // 衝突リスト作成後に空間から削除する
                ElementList.Add(e);
                return CellArray[mortonNum].Push(e);
            }
            return false;
        }

        /// <summary>
        /// @brief 空間を作成する
        /// </summary>
        /// <param name="mortonNum">モートン番号</param>
        /// <returns></returns>
        bool CreateNewCell(ulong mortonNum)
        {
	        while (CellArray[mortonNum] == null)
	        {
		        // 指定の要素番号に空間を新規作成
		        CellArray[mortonNum] = new Cell();

        		// 親空間にジャンプ
	        	mortonNum = (mortonNum - 1) >> 2;
		        if (mortonNum >= CellNum) break;
	        }
	        return true;
        }

        /// <summary>
        /// @brief モートン番号を取得する
        /// </summary>
        /// <param name="aabb">座標</param>
        /// <returns></returns>
        private ulong GetMortonNumber(ref Collider.AABB aabb)
        {
            ulong lt = GetPointElem(aabb.Left, aabb.Top);
            ulong rb = GetPointElem(aabb.Right, aabb.Bottom);

            // 空間番号の排他的論理輪から所属レベルを算出
            ulong def = rb ^ lt;
            int hiLevel = 0;
            for (var i = 0; i < Level; ++i)
            {
                ulong check = (def >> (i * 2)) & 0x3;
                if (check != 0)
                {
                    hiLevel = i + 1;
                }
            }
            ulong spaceNum = rb >> (hiLevel * 2);
            ulong addNum = (Power[Level - hiLevel] - 1) / 3;
            spaceNum += addNum;

            if (spaceNum > CellNum) return 0xffffffff;

            return spaceNum;
        }

        /// <summary>
        /// @brief 32ビット変数を分割する
        /// </summary>
        /// <param name="n">分割する数</param>
        /// <returns>結果</returns>
        private ulong BitSeparate32(ulong n)
        {
            n = (n | (n << 8)) & 0x00ff00ff;
            n = (n | (n << 4)) & 0x0f0f0f0f;
            n = (n | (n << 2)) & 0x33333333;
            return (n | (n << 1)) & 0x55555555;
        }

        /// <summary>
        /// @brief 2D座標を2Dの空間番号に変換する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private ushort Get2DMortonNumber(ushort x, ushort y)
        {
            return (ushort)(BitSeparate32(x) | (BitSeparate32(y) << 1));
        }

        /// <summary>
        /// @brief 座標を空間番号に変更する
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private ulong GetPointElem(float x, float y)
        {
            return Get2DMortonNumber((ushort)((x - Left) / UnitWidth), (ushort)((y - Top) / UnitHeight));
        }
    }
}
