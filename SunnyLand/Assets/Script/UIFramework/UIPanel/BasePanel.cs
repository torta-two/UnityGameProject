using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Button btn;
    protected UIManager uiManager;

    private void Awake()
    {
        btn = FindCloseButton("CloseButton");
        canvasGroup = GetComponent<CanvasGroup>();
        uiManager = FindObjectOfType<InitializeUI>().uiRoot;

        if (btn != null)
        {
            btn.onClick.AddListener(uiManager.PopPanel);
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

    public void OnEnter()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPause()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnResume()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }       
}
