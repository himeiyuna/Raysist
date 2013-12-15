using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using DxLibDLL;

namespace Raysist
{
    /// <summary>
    /// @brief ゲーム内時間管理クラス
    /// </summary>
    abstract class Timeline : GameComponent
    {
        private string Path
        {
            set;
            get;
        }

        private string WorksheetName
        {
            set;
            get;
        }

        /// <summary>
        /// @brief 時間軸
        /// </summary>
        public int Time
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief レコードの長さ
        /// </summary>
        private int Length
        {
            set;
            get;
        }

        /// <summary>
        /// @brief
        /// </summary>
        private IEnumerator<List<Excel.Range>> Iterator
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="worksheet">ワークシート名</param>
        public Timeline(GameContainer container, string path, int length, string worksheet = null) : base(container)
        {
            Time = 0;
            Length = length;
            Iterator = Iterate(path, worksheet);
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
        private IEnumerator<List<Excel.Range>> Iterate(string path, string worksheetname)
        {
            using (ExcelController ec = new ExcelController(path, worksheetname))
            {
                int counter = 0;
                var record = ec.GetRecord(counter, Length);
                while (record != null)
                {
                    // タイムラインを次に進める
                    if (record[0].Value <= Time)
                    {
                        ++counter;
                        record = ec.GetRecord(counter, Length);
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
        protected abstract void OnUpdateTimeline(List<Excel.Range> record);
    }
}
