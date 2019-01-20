using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectPanel : BasePanel
{
    public void OnClickStoreButton()
    {
        uiManager.PushPanel(UIPanelInfo.PanelType.StorePanel);
    }

    public void OnClickTaskButton()
    {

    }
}
