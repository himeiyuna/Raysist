using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using DxLibDLL;
using Microsoft.VisualBasic.FileIO;

namespace Raysist
{
    /// <summary>
    /// @brief ゲーム内時間管理クラス
    /// </summary>
    abstract class Timeline : GameComponent
    {
        /// <summary>
        /// @brief 時間軸
        /// </summary>
        public int Time
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief
        /// </summary>
        private IEnumerator<string[]> Iterator
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="worksheet">ワークシート名</param>
        public Timeline(GameContainer container, string path) : base(container)
        {
            Time = 0;
            Iterator = Iterate(path);
        }

        /// <summary>
        /// @brief 更新処理
        /// </summary>
        public sealed override void Update()
        {
            ++Time;

            if (Iterator.Current != null)
            {
                OnUpdateTimeline(Iterator.Current);
            }

            Iterator.MoveNext();
        }

        /// <summary>
        /// @brief タイムライン更新
        /// </summary>
        /// <returns>タイムラインが更新されたらレコード、そうでなければnull</returns>
        private IEnumerator<string[]> Iterate(string path)
        {
            using (TextFieldParser parser = new TextFieldParser("Resources\\" + path)) 
            {
                parser.SetDelimiters(",");
                parser.TextFieldType = FieldType.Delimited;

                var record = parser.ReadFields();
                while (!parser.EndOfData)
                {
                    // タイムラインを次に進める
                    if (int.Parse(record[0]) <= Time)
                    {
                        record = parser.ReadFields();
                        yield return record;
                    }
                    yield return null;
                }
            }
            while (true) yield return null;
        }

        /// <summary>
        /// @brief タイムラインが更新された時に呼び出される
        /// </summary>
        /// <param name="record">レコード</param>
        protected abstract void OnUpdateTimeline(string[] record);
    }
}
