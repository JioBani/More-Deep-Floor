using System.Collections;
using System.Collections.Generic;
using LNK.MoreDeepFloor.Data.Corps;
using LNK.MoreDeepFloor.Data.Schemas;
using UnityEngine;

namespace LNK.MoreDeepFloor.CorpsSelectScene
{
    public class EventManager : MonoBehaviour
    {
        public delegate void CorpsDataEventHandler(CorpsData corpsData);
        
        
        #region #. OnClickCorpsListTile
        
        private CorpsDataEventHandler OnClickCorpsListTileAction;
        public void AddOnClickCorpsListTileAction(CorpsDataEventHandler action)
        {
            OnClickCorpsListTileAction += action;
        }
        
        public void RemoveOnClickCorpsListTileAction(CorpsDataEventHandler action)
        {
            OnClickCorpsListTileAction -= action;
        }
        public void SetOnClickCorpsListTileAction(CorpsData corpsData)
        {
            OnClickCorpsListTileAction?.Invoke(corpsData);
        }
        
        #endregion

        #region #. OnClickPreviewItem
        
        private CorpsDataEventHandler OnClickPreviewItemAction;
        public void AddOnClickPreviewItemAction(CorpsDataEventHandler action)
        {
            OnClickCorpsListTileAction += action;
        }
        
        public void RemoveOnClickPreviewItemAction(CorpsDataEventHandler action)
        {
            OnClickCorpsListTileAction -= action;
        }
        public void SetOnClickPreviewItemAction(CorpsData corpsData)
        {
            OnClickCorpsListTileAction?.Invoke(corpsData);
        }
        
        #endregion
        
        #region #. OnClickAdapt

        public delegate void OnAdaptEventHandler(List<CorpsData> corpsDatas);

        private OnAdaptEventHandler OnAdaptAction;
        
        public void AddOnAdaptAction(OnAdaptEventHandler action)
        {
            OnAdaptAction += action;
        }
        public void RemoveOnAdaptAction(OnAdaptEventHandler action)
        {
            OnAdaptAction -= action;
        }
        public void SetOnAdaptAction(List<CorpsData> corpsDatas)
        {
            OnAdaptAction?.Invoke(corpsDatas);
        }
        
        #endregion
        
        #region #. OnClickDefender

        public delegate void  OnClickDefenderEventHandler(DefenderOriginalData defenderOriginalData);

        private OnClickDefenderEventHandler onClickDefenderAction;
        
        public void AddOnClickDefenderAction(OnClickDefenderEventHandler action)
        {
            onClickDefenderAction += action;
        }
        
        public void RemoveOOnClickDefenderAction(OnClickDefenderEventHandler action)
        {
            onClickDefenderAction -= action;
        }
        
        public void SetOnClickDefenderAction(DefenderOriginalData defenderOriginalData)
        {
            onClickDefenderAction?.Invoke(defenderOriginalData);
        }
        
        #endregion
        
    }
}


