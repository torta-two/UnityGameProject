using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PausePanel : BasePanel
{
    public bool onAnimEnd = false;
    private Text maxScore;
    private Text currentScore;

    protected override void Awake()
    {
        base.Awake();
        currentScore = transform.Find("CurrentScorePanel/CurrentScore").GetComponent<Text>();
        maxScore = transform.Find("MaxScorePanel/MaxScore").GetComponent<Text>();

        ctrl.Model.OnScoreChange += OnScoreChange;
        ctrl.Model.OnMaxScoreChange += OnMaxScoreChange;
    }

    public override void OnEnter()
    {
        canvasGroup.blocksRaycasts = true;
        Tween tween = canvasGroup.DOFade(1, 0.2f);

        tween.OnComplete(() => { Time.timeScale = 0; });
    }

    #region button click event

    public void OnClickCloseButton()
    {
        Time.timeScale = 1;
    }

    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(GameRecord.Instance.levelIndex + 1);
    }

    public void OnClickReturnButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnClickShareButton()
    {
        System.Diagnostics.Process.Start("https://www.baidu.com/");
    }

    #endregion

    private void OnMaxScoreChange(int maxScore)
    {
        this.maxScore.text = maxScore.ToString();
    }

    private void OnScoreChange(int score)
    {
        currentScore.text = score.ToString();
    }
}
