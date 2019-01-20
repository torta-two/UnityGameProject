using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public GameRecordInfo gameRecordInfo;
    public int thislevel = 1;

    private Transform outline;
    private Transform outline_Passed;
    private Transform lockPanel;
    private Transform playerImage;
    private Transform rewardUI;

    private float radian = 0; //漂浮动画弧度增量

    private void Start()
    {
        outline = transform.Find("outline");
        outline_Passed = transform.Find("outline_Passed");
        lockPanel = transform.Find("lockPanel");
        playerImage = transform.Find("Player");
        rewardUI = transform.Find("RewardUI");

        if (gameRecordInfo.beingPassedLevel > thislevel)
        {
            SetUIActive(outline_Passed: true, rewardUI: true);
        }
        else if (gameRecordInfo.beingPassedLevel == thislevel)
        {
            SetUIActive(outline: true, player: true);
        }
        else
        {
            SetUIActive(lockPanel: true);
        }
    }

    private void Update()
    {
        if (gameRecordInfo.beingPassedLevel == thislevel)
        {
            OutlineAnim();
        }
        ButtonAnim();        
    }
    
    private void OutlineAnim()
    {    
        if(outline.gameObject.activeSelf == true)
        {
            Transform temp = outline.transform;
            temp.transform.Rotate(Vector3.back, Time.deltaTime * 15f);
            outline.transform.rotation = temp.rotation;
        }       
    }

    private void ButtonAnim()
    {
        Vector3 temp = transform.localPosition;
        
        radian += 1f * Time.deltaTime;
        temp.y = Mathf.Sin(radian + thislevel) * 15f;

        transform.localPosition = temp;
    }

    private void SetUIActive(bool outline = false,bool outline_Passed = false, bool lockPanel = false, bool player = false, bool rewardUI = false)
    {
        this.outline.gameObject.SetActive(outline);
        this.outline_Passed.gameObject.SetActive(outline_Passed);
        this.lockPanel.gameObject.SetActive(lockPanel);
        this.playerImage.gameObject.SetActive(player);
        this.rewardUI.gameObject.SetActive(rewardUI);
    }

    public void OnLevelSelectButtonClick()
    {
        gameRecordInfo.thisLevel = thislevel;

        SceneManager.LoadScene(thislevel + 1);       
    }
}
