using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TaskPanel : BasePanel
{
    public GameRecordInfo gameRecord;

    [HideInInspector]
    public Transform taskGroup; 

    private RectTransform rectTransform;
   
    protected override void Awake()
    {
        base.Awake();
        taskGroup = transform.Find("TaskScroll/TaskGroup");
       
        rectTransform = GetComponent<RectTransform>();
    }

    public override void OnEnter()
    {
        taskGroup.localPosition = new Vector3(0, -270, 0);
        base.OnEnter();
        rectTransform.DOMoveY(0, 0.2f);
    }

    public override void OnExit()
    {
        base.OnExit();
        rectTransform.DOLocalMoveY(-50, 0.2f);        
    }
}
