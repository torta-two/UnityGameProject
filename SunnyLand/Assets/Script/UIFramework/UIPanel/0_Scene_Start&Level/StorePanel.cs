using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StorePanel : BasePanel
{
    public AudioClip clip;

    [HideInInspector]
    public Text moneyText;

    [HideInInspector]
    public AudioSource source;

    [HideInInspector]
    private SelectPlayer[] selectPlayers;
    private RectTransform rectTransform;

    protected override void Awake()
    {
        base.Awake();
        
        rectTransform = GetComponent<RectTransform>();
        moneyText = transform.Find("MoneyPanel/Money").GetComponent<Text>();
        moneyText.text = gameRecord.money.ToString();

        source = GetComponent<AudioSource>();
        source.clip = clip;
        source.loop = false;

        selectPlayers = GetComponentsInChildren<SelectPlayer>();
        foreach (var item in selectPlayers)
            item.OnBuyPlayer += OnBuyPlayer;
    }

    private bool OnBuyPlayer(int playerPrice)
    {        
        if(playerPrice <= gameRecord.money)
        {
            source.Play();
            StartCoroutine(MoneyAnim(playerPrice));
            return true;
        }
        return false;
    }

    private IEnumerator MoneyAnim(int lossMoney)
    {
        for (int i = 0; i < lossMoney / 10; i++)
        {
            gameRecord.money -= 10;
            moneyText.text = gameRecord.money.ToString();

            yield return new WaitForSeconds(0.01f);
        }

        StopCoroutine(MoneyAnim(lossMoney));
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
