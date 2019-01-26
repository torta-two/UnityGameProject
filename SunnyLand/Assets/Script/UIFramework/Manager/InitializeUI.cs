using UnityEngine;

public class InitializeUI : MonoBehaviour
{
    public UIPanelInfo.PanelType mainPanel;
    public UIPanelInfo panelInfo;
    public GameRecordInfo gameRecord;

    private UIManager _uiManager;
    [HideInInspector]
    public UIManager UIManager
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = new UIManager();               
            }

            return _uiManager;
        }
    }

    private void Awake()
    {
        UIManager.PushPanel(mainPanel);
    }


    private void OnApplicationQuit()
    {
        if (!UIManager.CheckPanelExist(UIPanelInfo.PanelType.ExitWarningPanel,true))
        {            
            Application.CancelQuit();
            UIManager.PushPanel(UIPanelInfo.PanelType.ExitWarningPanel);
        }                
    }
}
