using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    [HideInInspector]
    public Model model;

    [HideInInspector]
    public AudioManager audioManager;

    [HideInInspector]
    public AudioSource audioSource;

    [HideInInspector]
    public PlayerControl player;

    [HideInInspector]
    public int loseHearts = 0;

    [HideInInspector]
    public UIManager UIManager;

    [HideInInspector]
    public GameRecordInfo gameRecord;
   
    [HideInInspector]
    public event Action OnPlayerBeHurtForUI;

    private List<Enemy> enemies = new List<Enemy>();
    private List<Coin> coins = new List<Coin>();

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();     
        UIManager = GetComponent<GameRoot>().UIManager;
        gameRecord = GetComponent<GameRoot>().gameRecord;

        model = FindObjectOfType<Model>();

        Enemy[] emys = GetComponentsInChildren<Enemy>();
        Coin[] cons = GetComponentsInChildren<Coin>();
        
        enemies.CopyFrom(emys);
        coins.CopyFrom(cons);

        Transform PlayerTrans = transform.Find("PlayerTrans");

        foreach (var item in model.gameRecord.playerPrefabList)
        {
            if (item.playerInfo.isSelect)
            {
                player = Instantiate(item, PlayerTrans.position, Quaternion.identity, transform);
            }
        }
    }

    private void Update()
    {
        loseHearts = player.playerInfo.maxHP - player.HP;

        Enemy deadEnemy = null;
        Coin deadReward = null;

        //通关，保存记录
        if (player.isPassLevel)
        {
            if (!UIManager.CheckPanelExist(UIPanelInfo.PanelType.EndingPanel))
            {
                UIManager.PushPanel(UIPanelInfo.PanelType.EndingPanel);
                UIManager.PushPanel(UIPanelInfo.PanelType.BalancePanel);
                model.SaveScore();

                audioManager.Play(audioManager.passLevel, player.audioSource);
            }
        }

        //死亡
        if (player.isDead)
        {
            if (!UIManager.CheckPanelExist(UIPanelInfo.PanelType.EndingPanel))
            {
                UIManager.PushPanel(UIPanelInfo.PanelType.EndingPanel);

                foreach (var item in enemies)
                    item.enabled = false;

                StartCoroutine(DelayInvokePushPanel(UIPanelInfo.PanelType.DeathPanel, 3));                    
            }
        }
        else
        {
            #region check dead enemy and hurt player

            foreach (var enemy in enemies)
            {
                deadEnemy = enemy.CheckDeadEnemy();

                if (enemy.CheckPlayBeHurt(player))
                {
                    if (OnPlayerBeHurtForUI != null)
                        OnPlayerBeHurtForUI();
                }

                if (deadEnemy != null)
                {
                    break;
                }
            }

            if (deadEnemy != null)
            {
                model.GetScore(deadEnemy.tag);
                deadEnemy.OnEnemyDead();

                enemies.Remove(deadEnemy);
            }

            #endregion


            #region check reward

            foreach (var coin in coins)
            {
                if (coin.checkCoin)
                {
                    deadReward = coin;
                    if (deadReward != null)
                    {
                        break;
                    }
                }
            }

            if (deadReward != null)
            {
                audioManager.Play(audioManager.commonCoin, deadReward.GetComponent<AudioSource>());
                model.GetScore(deadReward.tag);
                deadReward.OnGetCoins();

                coins.Remove(deadReward);
            }

            #endregion
        }
    }


    private IEnumerator DelayInvokePushPanel(UIPanelInfo.PanelType type , int delayTime)
    {
        for (int i = 0; i < delayTime; i++)
            yield return new WaitForSeconds(1);

        UIManager.PushPanel(type);
        StopCoroutine(DelayInvokePushPanel(type , delayTime));
    }

}
