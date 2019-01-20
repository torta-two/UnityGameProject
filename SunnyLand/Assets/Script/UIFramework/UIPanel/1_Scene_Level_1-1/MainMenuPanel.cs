using UnityEngine;

public class MainMenuPanel : BasePanel
{
    public void OnClickSystemSettingButton()
    {
        uiManager.PushPanel(UIPanelInfo.PanelType.SystemSettingPanel);
    }

    public void OnClickPauseButton()
    {
        uiManager.PushPanel(UIPanelInfo.PanelType.PausePanel);
        Time.timeScale = 0;
    }
}
