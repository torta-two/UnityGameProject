using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    private Text maxScore;
    private Text currentScore;

    protected override void Awake()
    {
        base.Awake();
        currentScore = transform.Find("CurrentScore").GetComponent<Text>();
        maxScore = transform.Find("MaxScore").GetComponent<Text>();

        ctrl.model.OnScoreChange += OnScoreChange;
        ctrl.model.OnMaxScoreChange += OnMaxScoreChange;
    }


    public void OnClickResumeButton()
    {
        Time.timeScale = 1;
    }
  
    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ctrl.model.gameRecord.levelIndex + 1);
    }

    public void OnClickReturnMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    private void OnMaxScoreChange(int maxScore)
    {
        this.maxScore.text = maxScore.ToString();
    }

    private void OnScoreChange(int score)
    {
        currentScore.text = score.ToString();
    }
}
