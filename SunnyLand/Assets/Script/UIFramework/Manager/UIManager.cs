using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private Transform _canvasTransform;
    public Transform CanvasTransform
    {
        get
        {
            if (_canvasTransform == null)
                _canvasTransform = GameObject.Find("Canvas").transform;
            return _canvasTransform;
        }
    }

    private InitializeUI _uiRoot;
    public InitializeUI UIRoot
    {
        get
        {
            if (_uiRoot == null)
                _uiRoot = UnityEngine.Object.FindObjectOfType<InitializeUI>();
            return _uiRoot;
        }
    }

    private Stack<BasePanel> panelStack;
    /// <summary>
    /// instPanelDict记录以及实例化在场景里的UIPanel
    /// </summary>
    private Dictionary<UIPanelInfo.PanelType, BasePanel> instPanelDict;

    public void PushPanel(UIPanelInfo.PanelType type)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count > 0)
            panelStack.Peek().OnPause();

        BasePanel instPanel = instPanelDict.TryGetValue(type);

        if (instPanel == null)
        {
            instPanel = InstantiatePanel(type);
            instPanelDict.Add(type, instPanel);
        }

        panelStack.Push(instPanel);
        instPanel.OnEnter();
    }

    public void PopPanel()
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count <= 0) return;

        BasePanel topPanel = panelStack.Pop();
        topPanel.OnExit();

        if (panelStack.Count <= 0) return;

        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }

    private BasePanel InstantiatePanel(UIPanelInfo.PanelType type)
    {
        GameObject prefab = UIRoot.panelInfo.panelDict.TryGetValue(type);

        if (prefab == null)
            throw new Exception("Can't find the prefab of " + type.ToString());

        BasePanel instPanel = UnityEngine.Object.Instantiate(prefab).GetComponent<BasePanel>();
        instPanel.gameObject.name = prefab.name;

        instPanel.transform.SetParent(CanvasTransform, false);

        return instPanel;
    }

    public bool CheckUIPanelInScene(UIPanelInfo.PanelType type)
    {
        bool uiPanelInScene = false;

        BasePanel panel = instPanelDict.TryGetValue(type);

        if (panel != null)
            uiPanelInScene = true;

        return uiPanelInScene;
    }


    /// <summary>
    /// 预先把需要获取实例的面板实例化
    /// </summary>
    /// <param name="panels">需要获取实例的面板(不包括mainPanel)</param>
    /// <param name="mainPanels">实例化且无需出栈的面板(一般为主UI面板)</param>
    public void InitializePanel(UIPanelInfo.PanelType mainPanels, UIPanelInfo.PanelType[] panels)
    {
        if (instPanelDict == null)
            instPanelDict = new Dictionary<UIPanelInfo.PanelType, BasePanel>();

        PushPanel(mainPanels);

        foreach (var item in panels)
        {
            if(item != mainPanels)
            {
                PushPanel(item);
                PopPanel();
            }            
        }
    }

}
