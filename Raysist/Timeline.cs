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
            ExcelController ec = new ExcelController(path, worksheetname);
            int counter = 0;
            var record = ec.GetRecord(counter, Length);
            while ( record != null )
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
            ec.Dispose();
            while (true) yield return null;
        }

        /// <summary>
        /// @brief タイムラインが更新された時に呼び出される
        /// </summary>
        /// <param name="record">レコード</param>
        protected abstract void OnUpdateTimeline(List<Excel.Range> record);
    }

    /// <summary>
    /// @brief Excelファイルを管理するクラス
    /// </summary>
    class ExcelController : IDisposable
    {

        /// <summary>
        /// @brief Excelオブジェクト
        /// </summary>
        private Excel.Application ExcelObject
        {
            set;
            get;
        }

        /// <summary>
        /// @brief Excelワークブック
        /// </summary>
        private Excel.Workbook ExcelWorkbook
        {
            set;
            get;
        }

        /// <summary>
        /// @brief Excelワークシート
        /// </summary>
        private Excel.Worksheet ExcelWorksheet
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="worksheet">ワークシート名</param>
        public ExcelController(string path, string worksheet)
        {
            ExcelObject = new Excel.Application();

            try
            {
                ExcelWorkbook = ExcelObject.Workbooks.Open(System.IO.Path.GetFullPath("Resources\\" + path));
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                ExcelObject.Quit();
                throw e;
            }
            ExcelWorksheet = ExcelWorkbook.Sheets[GetSheetIndex(worksheet, ExcelWorkbook.Sheets)] as Excel.Worksheet;
        }

        /// <summary>
        /// @brief デストラクタ
        /// </summary>
        ~ExcelController()
        {
            Dispose();
        }

        /// <summary>
        /// @brief 指定したセルとの値が同じであればtrueを返す
        /// </summary>
        /// <param name="col">列</param>
        /// <param name="row">行</param>
        /// <param name="value">値</param>
        /// <returns></returns>
        public bool Equals(int col, int row, int value)
        {
            return (ExcelWorksheet.Cells[row + 1, col + 1] as Excel.Range).Value == value;
        }

        /// <summary>
        /// @brief 指定した行のレコードを取得する
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="length">レコードの長さ</param>
        /// <returns>レコード</returns>
        public List<Excel.Range> GetRecord(int row, int length)
        {
            if ((ExcelWorksheet.Cells[row + 1, 1] as Excel.Range).Value == null)
            {
                return null;
            }

            var ret = new List<Excel.Range>(length);
            for (var i = 0; i < length; ++i)
            {
                
                ret.Add(ExcelWorksheet.Cells[row + 1, i + 1] as Excel.Range);
            }
            return ret;
        }
        
        /// <summary>
        /// @brief 解放処理
        /// </summary>
        public void Dispose()
        {
            ExcelWorkbook.Close();
            ExcelObject.Quit();
        }

        /// <summary>
        /// @brief 指定されたワークシートのインデックスを取得する
        /// </summary>
        /// <param name="worksheet">ワークシート名</param>
        /// <param name="sheets">ワークシートの配列</param>
        /// <returns>ワークシートのインデックス番号(0なら存在しない)</returns>
        private int GetSheetIndex(string worksheet, Excel.Sheets sheets)
        {
            // 何も指定されていなければ最初のワークシートを返す
            if (worksheet == null)
            {
                return 1;
            }

            int i = 0;
            foreach (Excel.Worksheet sh in sheets)
            {
                if (worksheet == sh.Name)
                {
                    return i + 1;
                }
                ++i;
            }
            return 0;
        }
    }
}
