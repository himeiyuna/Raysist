using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class ResourceController : IDisposable
    {
        private static ResourceController instance; 
        public static ResourceController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResourceController();
                }
                return instance;
            }
        }

        /// <summary>
        /// @brief ハンドルの配列(キーはパス)
        /// </summary>
        public Dictionary<string, int> Handles
        {
            private set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        private ResourceController()
        {
            Handles = new Dictionary<string, int>();
        }

        /// <summary>
        /// @brief 解放処理
        /// </summary>
        public void Dispose()
        {
            instance = null;
        }

        /// <summary>
        /// @brief 画像読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>success:handle, failed:-1</returns>
        public int LoadGraphic(string path)
        {
            if (Handles.ContainsKey(path))
            {
                return Handles[path];
            }

            var h = DX.LoadGraph("Resources\\" + path);
            if (h == -1)
            {
                return -1;
            }

            Handles.Add(path, h);

            return h;
        }

        /// <summary>
        /// @brief 画像読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="num">総数</param>
        /// <param name="col">横</param>
        /// <param name="row">縦</param>
        /// <param name="width">横幅</param>
        /// <param name="height">縦幅</param>
        /// <returns>success:handles, failed:null</returns>
        public int[] LoadDivideGraphic(string path, int num, int width, int height)
        {
            int h = LoadGraphic(path);
            if (h == -1)
            {
                return null;
            }

            int[] buf = new int[num];
            for (var i = 0; i < num; ++i)
            {
                buf[i] = DX.DerivationGraph(i * width, 0, width, height, h);
            }

            return buf;
        }

        /// <summary>
        /// @brief 画像読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>success:handle, failed:-1</returns>
        public int LoadModel(string path)
        {
            return -1;
        }

        /// <summary>
        /// @brief 画像読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>success:handle, failed:-1</returns>
        public int LoadSound(string path)
        {
            return -1;
        }
    }
}
