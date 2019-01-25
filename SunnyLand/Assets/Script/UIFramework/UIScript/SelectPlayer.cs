using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SelectPlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerInfo player;
    public AudioClip audioClip;

    [HideInInspector]
    public event Func<int,bool> OnBuyPlayer;
    
    private Animator playerImage;
    private Toggle toggle;
    private Transform toggleButton;
    private Transform buyButton;
    private Transform introduction;

    private void Awake()
    {
        playerImage = GetComponentInChildren<Animator>();
        toggle = GetComponent<Toggle>();
        toggle.isOn = player.isSelect;

        toggleButton = transform.Find("toggleButton");
        buyButton = transform.Find("buyButton");
        introduction = transform.Find("Introduction");
    }

    private void Start ()
    {                    
        Text price = buyButton.GetComponentInChildren<Text>();
        price.text = player.price.ToString();

        IsPurchase(player.isPurchase);
        introduction.gameObject.SetActive(false);
	}

    private void IsPurchase(bool isPurchase)
    {
        if (isPurchase)
        {
            toggle.enabled = true;
            toggleButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            player.isPurchase = true;
        }
        else
        {
            toggle.enabled = false;
            toggleButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            player.isPurchase = false;
        }
    }   

    void Update ()
    {
        if(player.isPurchase)
        {
            if(toggle.isOn)
            {
                playerImage.SetBool("Play", true);
                player.isSelect = true;
            }
            else
            {
                playerImage.SetBool("Play", false);
                player.isSelect = false;
            }            
        }        
    }

    #region introduction

    public void OnPointerExit(PointerEventData eventData)
    {
        ((IPointerExitHandler)toggle).OnPointerExit(eventData);

        introduction.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ((IPointerEnterHandler)toggle).OnPointerEnter(eventData);

        introduction.gameObject.SetActive(true);
    }

    #endregion

    #region buybutton click event

    public void OnClickBuyButton()
    {
        IsPurchase(OnBuyPlayer(player.price));
    } 

    #endregion
}
