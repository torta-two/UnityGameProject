using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public UIPanelInfo.PanelType mainPanel;
    public UIPanelInfo panelInfo;
    public GameRecord gameRecord;
    public static string GameRecordJsonSavePath => Application.dataPath + "/Json/json_GameRecord";

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
        GameRecord.Load(GameRecordJsonSavePath, gameRecord);
        gameRecord = GameRecord.Instance;

        UIManager.PushPanel(mainPanel);
    }

}
