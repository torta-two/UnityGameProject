using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : BasePanel
{
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(GameRecord.Instance.currentLevelIndex + 1);
    }

    public void OnClickReturnButton()
    {
        SceneManager.LoadScene(1);
    }
}
