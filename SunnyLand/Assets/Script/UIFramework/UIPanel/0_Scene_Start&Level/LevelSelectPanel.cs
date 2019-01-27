
public class LevelSelectPanel : BasePanel
{
    private LevelSelectButton[] levelButtons;

    protected override void Awake()
    {
        base.Awake();

        levelButtons = GetComponentsInChildren<LevelSelectButton>();    
    }

    private void Start()
    {
        foreach (var item in levelButtons)
        {
            if (item.buttonLevelIndex < gameRecord.beingPassedLevel)
            {
                int specialCoin = gameRecord.specialCoin[item.buttonLevelIndex - 1];
                for (int i = 0; i < 3; i++)
                {
                    if (i < specialCoin)
                    {
                        item.specialCoin[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        item.specialCoin[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void OnClickStoreButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.StorePanel);
    }

    public void OnClickTaskButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.TaskPanel);
    }

    private void OnApplicationQuit()
    {
        gameRecord.Save();
    }
}
