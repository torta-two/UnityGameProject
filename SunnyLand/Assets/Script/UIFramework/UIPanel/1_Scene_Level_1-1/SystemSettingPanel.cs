using UnityEngine;

public class SystemSettingPanel : BasePanel
{
    public void OnClickExitButton()
    {
        ctrl.gameRecordInfo.Save();

        Application.Quit();
    }
}
