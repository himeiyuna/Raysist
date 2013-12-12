using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raysist
{
    /// <summary>
    /// @brief 背景モデル管理を担う
    /// </summary>
    class Stage : GameComponent
    {
        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container">自身を組み込むコンテナ</param>
        public Stage(GameContainer container) : base(container)
        {
            Position.LocalScale *= 1000.0f;
            Position.LocalPosition = new Vector3 { x = 0.0f, y = 0.0f, z = 100.0f }; 
        }

        public override void Update()
        {
            //Position.LocalPosition.y -= 1.0f;
            // TODO: タイムラインに沿って回転と移動を行う
        }
 	
    }

    class StageTimeline : Timeline
    {
        /// <summary>
        /// @brief 参照する親
        /// </summary>
        private Stage Parent
        {
            set;
            get;
        }

        /// <summary>
        /// @brief コンストラクタ
        /// </summary>
        /// <param name="container"></param>
        /// <param name="s"></param>
        public StageTimeline(GameContainer container, Stage s) : base(container, "stage.xlsx", 4)
        {
            Parent = s;
        }


        /// <summary>
        /// @brief タイムラインが更新されたときに呼び出される
        /// </summary>
        /// <param name="record">レコード</param>
        protected override void OnUpdateTimeline(List<Microsoft.Office.Interop.Excel.Range> record)
        {
            Parent.Position.LocalPosition = new Vector3 { x = (float)record[1].Value, y = (float)-record[2].Value, z = (float)record[3].Value };
        }
    }
}
