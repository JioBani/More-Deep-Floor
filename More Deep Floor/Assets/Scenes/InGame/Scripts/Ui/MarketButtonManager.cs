using LNK.MoreDeepFloor.InGame.Entity;
using LNK.MoreDeepFloor.InGame.MarketSystem;
using LNK.MoreDeepFloor.InGame.Tiles;
using UnityEngine;

namespace LNK.MoreDeepFloor.InGame.Ui
{
    public class MarketButtonManager : MonoBehaviour
    {
        private TileManager tileManager;
        private MarketManager marketManager;
        private ObjectPooler defenderPooler;

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
    }
}


