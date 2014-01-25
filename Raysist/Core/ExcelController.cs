//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.IO;
//using DxLibDLL;

//namespace Raysist
//{
//    /// <summary>
//    /// @brief Excelファイルを管理するクラス
//    /// </summary>
//    class ExcelController : IDisposable
//    {

//        /// <summary>
//        /// @brief Excelオブジェクト
//        /// </summary>
//        private Excel.Application ExcelObject
//        {
//            set;
//            get;
//        }

//        /// <summary>
//        /// @brief Excelワークブック
//        /// </summary>
//        private Excel.Workbook ExcelWorkbook
//        {
//            set;
//            get;
//        }

//        /// <summary>
//        /// @brief Excelワークシート
//        /// </summary>
//        private Excel.Worksheet ExcelWorksheet
//        {
//            set;
//            get;
//        }

//        /// <summary>
//        /// @brief コンストラクタ
//        /// </summary>
//        /// <param name="path">ファイルパス</param>
//        /// <param name="worksheet">ワークシート名</param>
//        public ExcelController(string path, string worksheet)
//        {
//            ExcelObject = new Excel.Application();

//            try
//            {
//                ExcelWorkbook = ExcelObject.Workbooks.Open(System.IO.Path.GetFullPath("Resources\\" + path));
//            }
//            catch (System.Runtime.InteropServices.COMException e)
//            {
//                ExcelObject.Quit();
//                throw e;
//            }
//            ExcelWorksheet = ExcelWorkbook.Sheets[GetSheetIndex(worksheet, ExcelWorkbook.Sheets)] as Excel.Worksheet;
//        }

//        /// <summary>
//        /// @breif デストラクタ
//        /// </summary>
//        ~ExcelController()
//        {
//            Dispose();
//        }

//        /// <summary>
//        /// @brief 指定したセルとの値が同じであればtrueを返す
//        /// </summary>
//        /// <param name="col">列</param>
//        /// <param name="row">行</param>
//        /// <param name="value">値</param>
//        /// <returns></returns>
//        public bool Equals(int col, int row, int value)
//        {
//            return (ExcelWorksheet.Cells[row + 1, col + 1] as Excel.Range).Value == value;
//        }

//        /// <summary>
//        /// @brief 指定した行のレコードを取得する
//        /// </summary>
//        /// <param name="row">行</param>
//        /// <param name="length">レコードの長さ</param>
//        /// <returns>レコード</returns>
//        public List<Excel.Range> GetRecord(int row, int length)
//        {
//            if ((ExcelWorksheet.Cells[row + 1, 1] as Excel.Range).Value == null)
//            {
//                return null;
//            }

//            var ret = new List<Excel.Range>(length);
//            for (var i = 0; i < length; ++i)
//            {
//                ret.Add(ExcelWorksheet.Cells[row + 1, i + 1] as Excel.Range);
//            }
//            return ret;
//        }

//        /// <summary>
//        /// @brief 解放処理
//        /// </summary>
//        public void Dispose()
//        {
//            if (ExcelObject == null)
//            {
//                ExcelObject.Workbooks.Close();
//                ExcelObject.Quit();
//                ExcelObject = null;
//                ExcelWorkbook = null;
//            }
//        }

//        /// <summary>
//        /// @brief 指定されたワークシートのインデックスを取得する
//        /// </summary>
//        /// <param name="worksheet">ワークシート名</param>
//        /// <param name="sheets">ワークシートの配列</param>
//        /// <returns>ワークシートのインデックス番号(0なら存在しない)</returns>
//        private int GetSheetIndex(string worksheet, Excel.Sheets sheets)
//        {
//            // 何も指定されていなければ最初のワークシートを返す
//            if (worksheet == null)
//            {
//                return 1;
//            }

//            int i = 0;
//            foreach (Excel.Worksheet sh in sheets)
//            {
//                if (worksheet == sh.Name)
//                {
//                    return i + 1;
//                }
//                ++i;
//            }
//            return 0;
//        }
//    }
//}
