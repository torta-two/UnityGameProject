using UnityEngine;

public class GameRoot : MonoBehaviour
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
        gameRecord = GameRecordInfo.Instance;
        UIManager.PushPanel(mainPanel);
    }

}
