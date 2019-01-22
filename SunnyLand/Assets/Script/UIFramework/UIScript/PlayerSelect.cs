using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator player;
    private Toggle toggle;
    private GameObject introduction;

	void Start ()
    {
        toggle = GetComponent<Toggle>();
        player = GetComponentInChildren<Animator>();
        introduction = transform.Find("Introduction").gameObject;

        introduction.SetActive(false);
	}
	

	void Update ()
    {
        player.SetBool("Play", toggle.isOn);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ((IPointerExitHandler)toggle).OnPointerExit(eventData);

        introduction.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ((IPointerEnterHandler)toggle).OnPointerEnter(eventData);

        introduction.SetActive(true);
    }
}
