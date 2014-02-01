using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace Raysist
{
    class MusicPlayer : GameComponent
    {
        /// <summary>
        /// @brief ミュージックハンドル
        /// </summary>
        private int MusicHandle
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        /// <param name="path">ファイルパス</param>
        public MusicPlayer(GameContainer container, string path) : base(container)
        {
            MusicHandle = ResourceController.Instance.LoadMusic(path);
            DX.PlayMusicMem(MusicHandle, 2);
        }

        /// <summary>
        /// @brief デストラクタ
        /// </summary>
        ~MusicPlayer()
        {
            DX.StopMusicMem(MusicHandle);
        }

        public override void Update()
        {
            
        }
    }
}
