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
        if (instPanelDict == null)
            instPanelDict = new Dictionary<UIPanelInfo.PanelType, BasePanel>();

        if (panelStack == null)
            panelStack = new Stack<BasePanel>();

        if (panelStack.Count > 0)
            panelStack.Peek().OnPause();

        BasePanel panel = instPanelDict.TryGetValue(type);

        if (panel == null)
            panel = InstantiatePanel(type);

        panelStack.Push(panel);
        panel.OnEnter();
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
        instPanelDict.Add(type, instPanel);

        return instPanel;
    }

    //public void Initialize(UIPanelInfo.PanelType mainPanel)
    //{
    //    PushPanel(mainPanel);
    //}

    public bool CheckPanelExist(UIPanelInfo.PanelType type)
    {
        bool uiPanelInScene = false;

        BasePanel panel = instPanelDict.TryGetValue(type);

        if (panel != null)
            uiPanelInScene = true;

        return uiPanelInScene;
    }
}
