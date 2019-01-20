using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : BasePanel
{
    private Ctrl ctrl;

    private void Start()
    {
        ctrl = FindObjectOfType<Ctrl>();
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
