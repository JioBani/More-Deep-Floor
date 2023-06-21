using LNK.MoreDeepFloor.InGame.Entitys;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using LNK.MoreDeepFloor.InGame.Ui.Market;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class MarketButtonManager : MonoBehaviour
    {
        private TileManager tileManager;
        private MarketManager marketManager;
        private ObjectPooler defenderPooler;
        public MarketBoard marketBoard;

        private void Awake()
        {
            tileManager = ReferenceManager.instance.tileManager;
            marketManager = ReferenceManager.instance.marketManager;
            defenderPooler = ReferenceManager.instance.objectPoolingManager.defenderPooler;
        }

        public void OnClickReRoll()
        {
            marketManager.TryReRoll();
        }

        public void OnClickExpUp()
        {
            marketManager.TryBuyExp();
        }

        public void OnClickClose()
        {
            marketBoard.Close();
        }
        
        public void OnClickOpen()
        {
            marketBoard.Open();
        }
    }
}


