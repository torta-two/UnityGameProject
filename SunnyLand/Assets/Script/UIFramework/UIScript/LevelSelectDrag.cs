using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelectDrag : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    public float dragSpeed = 5.0f;
    public Toggle[] toggles = new Toggle[2];
    public Toggle[] bgs = new Toggle[2];
    public AudioClip audioClip;

    private ScrollRect scrollRect;
    private AudioSource audioSource;
    private float[] pageArray = { 0.01f, 1 };//, 0.3333f, 0.6666f    
    private float targetHorizental = 0;
    private bool isDraging = false;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.clip = audioClip;
    }

    void Update()
    {
        if (!isDraging)
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetHorizental, Time.deltaTime * dragSpeed);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
        float posX = scrollRect.horizontalNormalizedPosition;
        int index = 0;
        float offest = Mathf.Abs(posX - pageArray[index]);
        for (int i = 1; i < pageArray.Length; i++)
        {
            float offestTemp = Mathf.Abs(posX - pageArray[i]);
            if (offest > offestTemp)
            {
                index = i;
                offest = offestTemp;
            }
        }
        targetHorizental = pageArray[index];
        toggles[index].isOn = true;
        bgs[index].isOn = true;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
        audioSource.Play();
    }


    public void MoveToPage1(bool isOn)
    {
        if (isOn)
        {
            targetHorizental = pageArray[0];
            bgs[0].isOn = true;
        }
    }

    public void MoveToPage2(bool isOn)
    {
        if (isOn)
        {
            targetHorizental = pageArray[1];
            bgs[1].isOn = true;
        }
    }
}

