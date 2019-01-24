using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StorePanel : BasePanel
{
    private RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        rectTransform = GetComponent<RectTransform>();   
    }

    public override void OnEnter()
    {
        base.OnEnter();
        rectTransform.DOMoveY(0, 0.2f);
    }

    public override void OnExit()
    {
        base.OnExit();
        rectTransform.DOLocalMoveY(-50, 0.2f);
    }

}
