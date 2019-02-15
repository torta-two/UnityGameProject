using UnityEngine;
using UnityEngine.UI;

public class GameRoot : MonoBehaviour
{
    public UIPanelInfo.PanelType mainPanel;
    public UIPanelInfo panelInfo;
    public GameRecord gameRecord;
    public static string GameRecordJsonSavePath =>
        Application.persistentDataPath + "/json_GameRecord.json";

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

    //private Canvas interludeCanvas;
    //private CanvasScaler interludeCanvasScaler;

    private void Awake()
    {        
        GameRecord.Load(GameRecordJsonSavePath, gameRecord);
        UIManager.PushPanel(mainPanel);

        //GameObject interlude = GameObject.Find("InterludeCanvas");

        //if (interlude == null)
        //{
        //    interlude = new GameObject("InterludeCanvas", typeof(Canvas), typeof(CanvasScaler));
        //    DontDestroyOnLoad(interlude);
        //}

        //interludeCanvas = interlude.GetComponent<Canvas>();
        //interludeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        ////interludeCanvas.sortingOrder = 999;

        //interludeCanvasScaler = interlude.GetComponent<CanvasScaler>();
        //interludeCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

}
