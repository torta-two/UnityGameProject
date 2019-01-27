using System;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{
    [HideInInspector]
    public Model Model => FindObjectOfType<Model>();

    [HideInInspector]
    public View View => FindObjectOfType<View>();

    [HideInInspector]
    public AudioManager audioManager;

    [HideInInspector]
    public AudioSource audioSource;

    [HideInInspector]
    public PlayerControl player;

    [HideInInspector]
    public int loseHearts = 0;
   
    [HideInInspector]
    public event Action OnPlayerBeHurt_UI;

    [HideInInspector]
    public event Action OnPlayerPassLevel;

    [HideInInspector]
    public event Action OnPlayerBeDead;

    private UIManager UIManager => FindObjectOfType<GameRoot>().UIManager;
    
    private List<Enemy> enemies = new List<Enemy>();
    private List<Coin> coins = new List<Coin>();

    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        audioSource = GetComponent<AudioSource>();     

        Enemy[] emys = GetComponentsInChildren<Enemy>();
        Coin[] cons = GetComponentsInChildren<Coin>();
        
        enemies.CopyFrom(emys);
        coins.CopyFrom(cons);

        OnPlayerPassLevel += View.OnPlayerPassLevel;
        OnPlayerBeDead += View.OnPlayerBeDead;

        Transform PlayerTrans = transform.Find("PlayerTrans");
        Debug.Log("1");
        foreach (var item in GameRecord.playerPathList)
        {
            GameObject playerPrefab = Resources.Load(item) as GameObject;
            if(playerPrefab == null)
                Debug.Log(playerPrefab.name);
            if(playerPrefab.GetComponent<PlayerControl>().playerInfo.isSelect)
            {
                player = Instantiate(playerPrefab, PlayerTrans.position, Quaternion.identity, transform).GetComponent<PlayerControl>();
            }
        }
    }

    private void Start()
    {
        //Transform PlayerTrans = transform.Find("PlayerTrans");

        //foreach (var item in GameRecord.playerPrefabList)
        //{
        //    if (item.playerInfo.isSelect)
        //    {
        //        player = Instantiate(item, PlayerTrans.position, Quaternion.identity, transform);
        //    }
        //}
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
                OnPlayerPassLevel();
                Model.SaveScore();

                audioManager.Play(audioManager.passLevel, player.audioSource);
            }
        }

        //死亡
        if (player.isDead)
        {
            if (!UIManager.CheckPanelExist(UIPanelInfo.PanelType.EndingPanel))
            {
                OnPlayerBeDead();

                foreach (var item in enemies)
                    item.enabled = false;                                   
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
                    OnPlayerBeHurt_UI?.Invoke();
                }

                if (deadEnemy != null)
                {
                    break;
                }
            }

            if (deadEnemy != null)
            {
                Model.GetScore(deadEnemy.tag);
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
                Model.GetScore(deadReward.tag);
                deadReward.OnGetCoins();

                coins.Remove(deadReward);
            }

            #endregion
        }
    } 
}
