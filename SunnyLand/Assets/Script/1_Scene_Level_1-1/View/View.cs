using System.Collections;
using UnityEngine;

public class View : MonoBehaviour
{
    private UIManager UIManager => FindObjectOfType<GameRoot>().UIManager;


    public void OnPlayerPassLevel()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.EndingPanel);
        UIManager.PushPanel(UIPanelInfo.PanelType.BalancePanel);
    }   


    public void OnPlayerBeDead()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.EndingPanel);
        StartCoroutine(DelayInvokePushPanel(UIPanelInfo.PanelType.DeathPanel, 3));
    }


    private IEnumerator DelayInvokePushPanel(UIPanelInfo.PanelType type, int delayTime)
    {
        for (int i = 0; i < delayTime; i++)
            yield return new WaitForSeconds(1);

        UIManager.PushPanel(type);
        StopCoroutine(DelayInvokePushPanel(type, delayTime));
    }


    private void OnApplicationQuit()
    {
        if (!UIManager.CheckPanelExist(UIPanelInfo.PanelType.ExitWarningPanel, true))
        {
            Application.CancelQuit();
            UIManager.PushPanel(UIPanelInfo.PanelType.ExitWarningPanel);
        }
    }
}
