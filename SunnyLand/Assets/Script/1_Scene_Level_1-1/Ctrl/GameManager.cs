using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public PlayerControl player;

    private Ctrl ctrl;    
    private List<EnemyMove> enemies = new List<EnemyMove>();
    private List<RewardCheck> rewards = new List<RewardCheck>();

    private void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        player = FindObjectOfType<PlayerControl>();

        EnemyMove[] emys = FindObjectsOfType<EnemyMove>();
        RewardCheck[] rwds = FindObjectsOfType<RewardCheck>();

        enemies.CopyFrom(emys);
        rewards.CopyFrom(rwds);
    }


    void Update()
    {
        EnemyMove deadEnemy = null;
        RewardCheck deadReward = null;

        if (player.HP == 0)
        {
            
        }

        if(player.isEnding)
        {
            if(!ctrl.uiRoot.CheckUIPanelInScene(UIPanelInfo.PanelType.EndingPanel))
            {
                player.GetComponent<PlayerControl>().OnPassLevel();

                ctrl.uiRoot.PushPanel(UIPanelInfo.PanelType.EndingPanel);
                ctrl.audioManager.Play(ctrl.audioManager.passLevel, player.audioSource);
            }
        }

        #region check enemy

        foreach (var item in enemies)
        {
            deadEnemy = item.CheckDeadEnemyAndPlayerHurt(player);
            if (deadEnemy != null)
            {
                ctrl.audioManager.Play(ctrl.audioManager.attack, deadEnemy.GetComponent<AudioSource>());
                break;
            }
        }

        if (deadEnemy != null)
        {
            ctrl.model.GetScore(deadEnemy.tag);
            deadEnemy.EnemyFuneral();
            enemies.Remove(deadEnemy);
        }

        #endregion


        #region check reward

        foreach (var item in rewards)
        {
            if (item.checkReward)
            {
                item.RewardFuneral();
                deadReward = item;
                ctrl.model.GetScore(item.tag);
                if (deadReward != null)
                {
                    ctrl.audioManager.Play(ctrl.audioManager.commonReward, deadReward.GetComponent<AudioSource>());
                    break;
                }
            }
        }

        if (deadReward != null)
            rewards.Remove(deadReward);

        #endregion

    }

}
