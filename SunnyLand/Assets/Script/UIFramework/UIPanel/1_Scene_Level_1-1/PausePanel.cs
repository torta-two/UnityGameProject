using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    public override void OnEnter()
    {       
        base.OnEnter();
    }

    public void OnClickResumeButton()
    {
        Time.timeScale = 1;
    }
  
    public void OnClickRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ctrl.gameRecordInfo.thisLevel + 1);
    }

    public void OnClickGoLevelButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
