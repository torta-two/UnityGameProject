using UnityEngine;

public class ExitWarningPanel : BasePanel
{
    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickCancelButton()
    {
        UIManager.PopPanel();
    }
}
