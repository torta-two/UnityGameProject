using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl : MonoBehaviour
{
    public GameRecordInfo gameRecordInfo;
    public float volume = 1;

    public int score = 0;
    public int maxScore = 0;
    public int specialRewards = 0;

    public int loseHearts = 0;

    [HideInInspector]
    public PlayerControl player;

    private List<Enemy> enemies = new List<Enemy>();
    private List<Coin> coins = new List<Coin>();

    [HideInInspector]
    public Model model;

    [HideInInspector]
    public AudioManager audioManager;

    [HideInInspector]
    public UIManager UIRoot;

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();       
        player = GetComponentInChildren<PlayerControl>();
        UIRoot = GetComponent<InitializeUI>().UIManager;

        model = FindObjectOfType<Model>();

        Enemy[] emys = GetComponentsInChildren<Enemy>();
        Coin[] cons = GetComponentsInChildren<Coin>();
        
        enemies.CopyFrom(emys);
        coins.CopyFrom(cons);
    }

    private void Update()
    {
        loseHearts = player.playerInfo.maxHP - player.HP;

        Enemy deadEnemy = null;
        Coin deadReward = null;

        if (player.HP == 0)
        {

        }

        if (player.isEnding)
        {
            if (!UIRoot.CheckPanelExist(UIPanelInfo.PanelType.EndingPanel))
            {
                player.GetComponent<PlayerControl>().OnPassLevel();

                UIRoot.PushPanel(UIPanelInfo.PanelType.EndingPanel);
                audioManager.Play(audioManager.passLevel, player.audioSource);
            }
        }

        #region check dead enemy and hurt player

        foreach (var enemy in enemies)
        {
            deadEnemy = enemy.CheckDeadEnemy();

            enemy.CheckPlayBeAttacked(player);

            if (deadEnemy != null)
            {                
                break;
            }
        }

        if (deadEnemy != null)
        {
            model.OnEnemyDead(deadEnemy.tag);
            deadEnemy.OnEnemyDead();
            enemies.Remove(deadEnemy);
        }

        #endregion


        #region check reward

        foreach (var coin in coins)
        {
            if (coin.checkCoin)
            {
                coin.OnGetCoins();
                deadReward = coin;
                model.OnGetCoins(coin.tag);
                if (deadReward != null)
                {
                    audioManager.Play(audioManager.commonReward, deadReward.GetComponent<AudioSource>());
                    break;
                }
            }
        }

        if (deadReward != null)
            coins.Remove(deadReward);

        #endregion

    }
}
