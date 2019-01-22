using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{



    public void OnClickSystemSettingButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.SystemSettingPanel);
    }

    public void OnClickPauseButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.PausePanel);
    }

    public void OnGetCoins()
    {

    }
}
