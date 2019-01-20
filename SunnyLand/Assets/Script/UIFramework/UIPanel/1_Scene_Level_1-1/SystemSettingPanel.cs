using UnityEngine;

public class SystemSettingPanel : BasePanel
{
    private Ctrl ctrl;

    private void Start()
    {
        ctrl = FindObjectOfType<Ctrl>();
    }

    public void OnClickExitButton()
    {
        ctrl.gameRecordInfo.Save();

        Application.Quit();
    }
}
