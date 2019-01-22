using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{    
    //private Ctrl ctrl;
    //private Text score;
    //private Text currentScore;
    //private Text maxScore;
    //private Image[] hearts;
    //private Slider scrollbar;

    private void Start ()
    {
        //ctrl = GameObject.FindWithTag("Ctrl").GetComponent<Ctrl>();
        //score = transform.Find("Canvas/MainMenuPanel/ScorePanel/Score").GetComponent<Text>();
        //currentScore = transform.Find("Canvas/PausePanel/CurrentScore").GetComponent<Text>();
        //maxScore = transform.Find("Canvas/PausePanel/MaxScore").GetComponent<Text>();
        //hearts = new Image[ctrl.gameManager.player.playerInfo.maxHP];

        //Image[] allHearts = transform.Find("Canvas/MainMenuPanel/HeartsPanel").GetComponentsInChildren<Image>();

        //for (int i = 0; i < allHearts.Length; i++)
        //{
        //    if (i < hearts.Length)
        //    {                
        //        hearts[i] = allHearts[i];
        //    }
        //    else
        //    {
        //        allHearts[i].gameObject.SetActive(false);
        //    }
        //}       
       
        //scrollbar = transform.Find("Canvas/SystemSettingPanel/Volume/Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        //score.text = ctrl.score.ToString();
        //currentScore.text = score.text;
        //maxScore.text = ctrl.maxScore.ToString();
       
        //switch(ctrl.loseHearts)
        //{
        //    case 0: break;
        //    case 1:
        //    case 2: 
        //    case 3: hearts[ctrl.loseHearts - 1].gameObject.SetActive(false); break;
        //    default: break;
        //}

        //ctrl.volume = scrollbar.value;
    }
}
