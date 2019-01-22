using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ctrl : MonoBehaviour
{
    [HideInInspector]
    public float volume = 1;

    [HideInInspector]
    public int loseHearts = 0;

    [HideInInspector]
    public PlayerControl player;
    
    [HideInInspector]
    public Model model;

    [HideInInspector]
    public AudioManager audioManager;

    [HideInInspector]
    public UIManager UIManager;

    [HideInInspector]
    public event Action OnPlayerBeHurt;

    private List<Enemy> enemies = new List<Enemy>();
    private List<Coin> coins = new List<Coin>();

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();       
        player = GetComponentInChildren<PlayerControl>();
        UIManager = GetComponent<InitializeUI>().UIManager;

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
            if (!UIManager.CheckPanelExist(UIPanelInfo.PanelType.EndingPanel))
            {
                player.GetComponent<PlayerControl>().OnPassLevel();
                model.SaveScore();

                UIManager.PushPanel(UIPanelInfo.PanelType.EndingPanel);
                audioManager.Play(audioManager.passLevel, player.audioSource);
            }
        }

        #region check dead enemy and hurt player

        foreach (var enemy in enemies)
        {
            deadEnemy = enemy.CheckDeadEnemy();

            if(enemy.CheckPlayBeHurt(player))
            {
                if(OnPlayerBeHurt != null)
                    OnPlayerBeHurt();
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
            audioManager.Play(audioManager.commonReward, deadReward.GetComponent<AudioSource>());
            model.GetScore(deadReward.tag);
            deadReward.OnGetCoins();

            coins.Remove(deadReward);
        }

        #endregion

    }
}
