using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUI : MonoBehaviour
{
    public UIPanelInfo.PanelType mainPanel;
    public UIPanelInfo.PanelType[] panels;

    public UIPanelInfo panelInfo;

    [HideInInspector]
    public UIManager uiRoot;
    

    private void Awake()
    {
        uiRoot = new UIManager();
        uiRoot.InitializePanel(mainPanel,panels);        
    }

}
