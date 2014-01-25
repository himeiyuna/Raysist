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
        /// @brief グラフィックハンドルの配列(キーはパス)
        /// </summary>
        private Dictionary<string, int> GraphicHandles
        {
            set;
            get;
        }

        /// <summary>
        /// @brief モデルハンドルの配列
        /// </summary>
        private Dictionary<string, int> ModelHandles
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        private ResourceController()
        {
            GraphicHandles = new Dictionary<string, int>();
            ModelHandles = new Dictionary<string, int>();
        }

        /// <summary>
        /// @brief 解放処理
        /// </summary>
        public void Dispose()
        {
            instance = null;

            foreach (var handle in GraphicHandles)
            {
                DX.DeleteGraph(handle.Value);
            }

            foreach (var handle in ModelHandles)
            {
                DX.MV1DeleteModel(handle.Value);
            }
        }

        /// <summary>
        /// @brief 画像読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>success:handle, failed:-1</returns>
        public int LoadGraphic(string path)
        {
            if (GraphicHandles.ContainsKey(path))
            {
                return GraphicHandles[path];
            }

            var h = DX.LoadGraph("Resources\\" + path);
            if (h == -1)
            {
                return -1;
            }

            GraphicHandles.Add(path, h);

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
        /// @brief モデル読み込み
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>success:handle, failed:-1</returns>
        public int LoadModel(string path)
        {
            if (ModelHandles.ContainsKey(path))
            {
                return DX.MV1DuplicateModel(ModelHandles[path]);
            }

            var h = DX.MV1LoadModel("Resources\\" + path);
            if (h == -1)
            {
                return -1;
            }

            ModelHandles.Add(path, h);

            return h;
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
