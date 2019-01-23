using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BasePanel : MonoBehaviour
{
    protected Ctrl ctrl;

    protected CanvasGroup canvasGroup;
    protected Button btn;
    protected UIManager UIManager;

    protected virtual void Awake()
    {
        btn = FindCloseButton("CloseButton");
        canvasGroup = GetComponent<CanvasGroup>();
        ctrl = FindObjectOfType<Ctrl>();
        UIManager = FindObjectOfType<InitializeUI>().UIManager;

        if (btn != null)
        {
            btn.onClick.AddListener(UIManager.PopPanel);
        }

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        
    }

    private Button FindCloseButton(string childName)
    {
        Button closeButton = null;
        foreach (var item in GetComponentsInChildren<Button>())
        {
            if (item.name == childName)
                closeButton = item.GetComponent<Button>();
        }
        return closeButton;
    }

    public virtual void OnEnter()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 0.2f);       
    }

    public virtual void OnPause()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnExit()
    {
        canvasGroup.DOFade(0, 0.2f);
        canvasGroup.blocksRaycasts = false;
    }       
}
