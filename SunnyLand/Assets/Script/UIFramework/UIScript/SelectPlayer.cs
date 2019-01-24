using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SelectPlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PlayerInfo player;    

    private Text moneyText;
    private int money;
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
        moneyText = transform.parent.parent.Find("MoneyPanel").Find("Money").GetComponent<Text>();
        money = Convert.ToInt32(moneyText.text);
                
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
        if (money >= player.price)
        {
            StartCoroutine(MoneyAnim(player.price));
        }
    }

    private IEnumerator MoneyAnim(int lossMoney)
    {
        for (int i = 0; i < lossMoney/10; i++)
        {
            money -= 10;
            moneyText.text = money.ToString();
            yield return new WaitForSeconds(0.001f);
        }
        IsPurchase(true);

        StopCoroutine(MoneyAnim(lossMoney));
    }

    #endregion
}
