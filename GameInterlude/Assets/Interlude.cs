using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interlude
{
    private static Interlude _instance;
    public static Interlude Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Interlude();
            return _instance;
        }
    }

    private Canvas canvas;
    private CanvasScaler canvasScaler;
    private GameObject interludePanel;

    private Interlude()
    {
        canvas = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler)).GetComponent<Canvas>();        
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999;

        canvasScaler = canvas.GetComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        //interludePanel = UnityEngine.Object.Instantiate(Resources.Load("interludePanel"), canvas.transform) as GameObject;
        //interludePanel.transform.SetAsLastSibling();
        //interludePanel.SetActive(false);

        //UnityEngine.Object.DontDestroyOnLoad(interludePanel);
        UnityEngine.Object.DontDestroyOnLoad(canvas.gameObject);
    }

    public void LoadScene(int sceneBuildIndex)
    {
        interludePanel.SetActive(true);
        interludePanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);

        //EventHandler temp = BlackSite;
        //if (temp != null)
        //{
        //    temp();
        //}
    }

    

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    

    //private void BlackSite(int sceneBuildIndex)
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        Vector4 tempColor = interludePanel.GetComponent<Image>().color;
    //        tempColor -= new Vector4(0.2f, 0.2f, 0.2f, -0.2f);
    //        interludePanel.GetComponent<Image>().color = tempColor;

    //        yield return new WaitForSeconds(0.2f);
    //    }

    //    SceneManager.LoadScene(sceneBuildIndex);
    //}
}
