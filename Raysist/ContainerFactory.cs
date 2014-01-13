using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief コンテナを生成するファクトリクラス
    /// </summary>
    class ContainerFactory
    {
        /// <summary>
        /// @brief コンテナ初期化の追加処理関数
        /// </summary>
        protected Action<GameContainer> Option
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="createFunction"></param>
        public ContainerFactory(Action<GameContainer> option = null)
        {
            Option = option;
        }

        /// <summary>
        /// @brief コンテナを生成する
        /// </summary>
        /// <returns>生成されたコンテナ</returns>
        public virtual GameContainer Create()
        {
            var ret = new GameContainer();
            if (Option != null)
            {
                Option(ret);
            }
            return ret;
        }
    }

    class BitFactory : ContainerFactory
    {
        /// <summary>
        /// @brief プレイヤー
        /// </summary>
        private Player Player
        {
            set;
            get;
        }

        private Bit.BitIndex Index
        {
            set;
            get;
        }

        public BitFactory(Player player, Bit.BitIndex index, Action<GameContainer> option = null) : base(option)
        {
            Player = player;
            Index = index;
        }

        /// <summary>
        /// @brief コンテナ生成関数
        /// </summary>
        /// <returns></returns>
        public override GameContainer Create()
        {
            var g = new GameContainer(Player.Container);

            var raypier = new GameContainer();
            var raypierEnd = new GameContainer();

            var bit = new Bit(g, Player, Index, raypier);
            g.AddComponent(bit);

            var r = new Raypier(raypier, raypierEnd, bit);
            r.Active = false;
            raypier.AddComponent(r);

            
            g.AddComponent(new MeshRenderer(g, "bit.x"));

            g.Position.LocalRotation *= new Quaternion(Vector3.AxisX, -(float)Math.PI * 0.5f);
            g.Position.LocalScale *= 3.0f;

            raypierEnd.AddComponent(new RaypierRenderFinisher(raypierEnd));

            return null;
        }
    }
}
