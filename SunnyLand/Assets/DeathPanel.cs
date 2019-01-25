using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanel : BasePanel
{
    public void OnClickRestartButton()
    {
        SceneManager.LoadScene(ctrl.model.levelIndex + 1);
    }

    public void OnClickReturnButton()
    {
        SceneManager.LoadScene(1);
    }
}
