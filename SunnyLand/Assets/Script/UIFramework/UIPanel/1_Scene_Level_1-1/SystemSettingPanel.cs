using UnityEngine;

public class SystemSettingPanel : BasePanel
{
    public void OnClickExitButton()
    {
        ctrl.model.gameRecord.Save();

        Application.Quit();
    }
}
