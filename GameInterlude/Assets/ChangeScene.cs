using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public int loadSceneBuildIndex = 0;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnLoadScene);
    }

    private void OnLoadScene()
    {
        Interlude.Instance.LoadScene(loadSceneBuildIndex);
    }
}
