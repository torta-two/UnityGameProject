using UnityEngine;

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
            if (item.buttonLevelIndex < GameRecord.Instance.beingPassedLevel)
            {
                int specialCoin = GameRecord.Instance.specialCoin[item.buttonLevelIndex - 1];
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


    #region button onclick event

    public void OnClickStoreButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.StorePanel);
    }

    public void OnClickTaskButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.TaskPanel);
    }

    #endregion



    private void OnApplicationQuit()
    {
        GameRecord.Instance.Save(GameRoot.GameRecordJsonSavePath);
    }
}
