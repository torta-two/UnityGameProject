using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel
{
    private Image[] hearts;
    private Text score;

    protected override void Awake()
    {
        base.Awake();

        #region get hearts UI
        hearts = new Image[ctrl.player.playerInfo.maxHP];
        Image[] allHearts = transform.Find("HeartsPanel").GetComponentsInChildren<Image>();
        for (int i = 0; i < allHearts.Length; i++)
        {
            if (i < hearts.Length)
                hearts[i] = allHearts[i];
            else
                allHearts[i].gameObject.SetActive(false);
        }
        #endregion

        score = transform.Find("ScorePanel/Score").GetComponent<Text>();
        score.text = 0.ToString();

        ctrl.OnPlayerBeHurt += OnPlayerBehurt;
        ctrl.model.OnScoreChange += OnScoreChange;
    }




    public void OnClickSystemSettingButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.SystemSettingPanel);
    }

    public void OnClickPauseButton()
    {
        UIManager.PushPanel(UIPanelInfo.PanelType.PausePanel);
    }

    private void OnScoreChange(int score)
    {
        this.score.text = score.ToString();
    }

    private void OnPlayerBehurt()
    {
        if (ctrl.loseHearts >= 0 && ctrl.loseHearts < hearts.Length - 1)
        {
            hearts[ctrl.loseHearts].gameObject.SetActive(false);
        }
    }
}
