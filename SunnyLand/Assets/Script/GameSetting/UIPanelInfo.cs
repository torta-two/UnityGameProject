using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/UIPanelInfo")]
[Serializable]
public class UIPanelInfo : ScriptableObject
{
    public enum PanelType
    {
        StartPanel,
        LevelSelectPanel,
        StorePanel,
        TaskPanel,
        MainMenuPanel,
        PausePanel,
        SystemSettingPanel,        
        EndingPanel,
        BalancePanel,
        ExitWarningPanel,
        ReturnWarningPanel,
        DeathPanel
    }

    public List<GameObject> prefabList = new List<GameObject>();
    public List<PanelType> typeList = new List<PanelType>();
    public Dictionary<PanelType, GameObject> panelDict = new Dictionary<PanelType, GameObject>();

    private void OnEnable()
    {       
        for (int i = 0; i < (prefabList.Count < typeList.Count ? prefabList.Count : typeList.Count); i++)
        {
            panelDict.Add(typeList[i], prefabList[i]);
        }        
    }
}
