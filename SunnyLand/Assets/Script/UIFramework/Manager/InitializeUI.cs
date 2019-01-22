using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUI : MonoBehaviour
{
    public UIPanelInfo.PanelType mainPanel;
    public UIPanelInfo panelInfo;   

    private UIManager _uiManager;
    [HideInInspector]
    public UIManager UIManager
    {
        get
        {
            if (_uiManager == null)
            {
                _uiManager = new UIManager();               
            }

            return _uiManager;
        }
    }

    private void Awake()
    {
        UIManager.PushPanel(mainPanel);
    }
}
