using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanel : BasePanel
{
    public void OnClickStoreButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.StorePanel);
    }

    public void OnClickTaskButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.TaskPanel);
    }
}
