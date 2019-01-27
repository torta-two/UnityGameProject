using System;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public bool isMonsterTask;
    public int taskIndex;
    public int targetAmount;

    private TaskPanel taskPanel;

    private Text targetAmountText;

    private Text currentAmountText;
    private int currentAmount;

    private Button AchieveButton;
    private Button AchievePanelYesButton;
    private Transform taskAchievePanel;

    private void Awake()
    {
        taskPanel = transform.parent.parent.parent.GetComponent<TaskPanel>();

        targetAmountText = transform.Find("TargetAmount").GetComponent<Text>();
        currentAmountText = transform.Find("CurrentAmount").GetComponent<Text>();
        currentAmount = Convert.ToInt32(currentAmountText.text);

        AchieveButton = transform.Find("AchieveButton").GetComponent<Button>();
        AchieveButton.onClick.AddListener(OnClickAchieveButton);

        taskAchievePanel = taskPanel.transform.Find("TaskAchievePanel");
        taskAchievePanel.gameObject.SetActive(false);

        AchievePanelYesButton = taskAchievePanel.Find("YesButton").GetComponent<Button>();
        AchievePanelYesButton.onClick.AddListener(OnClickAchievePanelYesButton);

        if (GameRecord.Instance.taskState[taskIndex - 1] == 1)
        {
            MakeTaskToAchieveState();
            enabled = false;
        }
    }

    private void Update()
    {
        if (currentAmount >= targetAmount)
        {
            currentAmountText.gameObject.SetActive(false);
            targetAmountText.gameObject.SetActive(false);
            AchieveButton.gameObject.SetActive(true);
        }
        else
        {
            if (isMonsterTask)
                currentAmountText.text = GameRecord.Instance.killMonster.ToString();
            else
                currentAmountText.text = GameRecord.Instance.getCoin.ToString();
            currentAmount = Convert.ToInt32(currentAmountText.text);
        }
    }

    private void OnClickAchieveButton()
    {
        taskPanel.taskGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        taskAchievePanel.gameObject.SetActive(true);
        Text rewardMoneyText = taskAchievePanel.Find("RewardMoney").GetComponent<Text>();

        int rewardMoney = 0;
        switch (targetAmount)
        {
            case 5: rewardMoney = 50; break;
            case 10: rewardMoney = 100; break;
            case 100: rewardMoney = 1888; break;
            default: rewardMoney = 0; break;
        }
        rewardMoneyText.text = rewardMoney.ToString();
        GameRecord.Instance.money += rewardMoney;

        MakeTaskToAchieveState();
        GameRecord.Instance.taskState[taskIndex - 1] = 1;
    }

    private void OnClickAchievePanelYesButton()
    {
        taskPanel.taskGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        taskAchievePanel.gameObject.SetActive(false);
    }

    private void MakeTaskToAchieveState()
    {
        targetAmountText.gameObject.SetActive(false);
        currentAmountText.gameObject.SetActive(false);
        AchieveButton.gameObject.SetActive(true);

        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 0.5f;
        cg.blocksRaycasts = false;
    }
}
