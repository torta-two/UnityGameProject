using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class BasePanel : MonoBehaviour
{
    protected Ctrl ctrl;

    [HideInInspector]
    public CanvasGroup canvasGroup;
    protected Button closeButton;   
    protected UIManager UIManager;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        closeButton = FindCloseButton("CloseButton");        
        ctrl = FindObjectOfType<Ctrl>();
        UIManager = FindObjectOfType<InitializeUI>().UIManager;

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(UIManager.PopPanel);
        }

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
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
