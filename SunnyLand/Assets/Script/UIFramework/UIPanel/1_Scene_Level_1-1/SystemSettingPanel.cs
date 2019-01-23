using UnityEngine;

public class SystemSettingPanel : BasePanel
{
    public void OnClickExitButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.ExitWarningPanel);
    }
}
