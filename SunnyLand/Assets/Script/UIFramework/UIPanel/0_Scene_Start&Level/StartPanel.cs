using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanel : BasePanel
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(1);       
    }
	
}
