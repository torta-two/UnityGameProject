using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teststb : MonoBehaviour
{
    public GameRecordInfo gameRecord;

    private Text ver;
    private Button clickButton;

    public string SaveGameRecordPath => 
        System.IO.Path.Combine(Application.dataPath + "/Json/GameRecord");

    private void Awake()
    {
        if(System.IO.File.Exists(SaveGameRecordPath))
        {
            GameRecordInfo.LoadFromJSON(SaveGameRecordPath);
        }
        else
        {
            GameRecordInfo.InitializeFromDefault(gameRecord);
        }

        ver = GameObject.Find("Canvas/Panel/ver").GetComponent<Text>();

        ver.text = GameRecordInfo.Instance.money.ToString();

        clickButton = GameObject.Find("Canvas/Click").GetComponent<Button>();
        clickButton.onClick.AddListener(OnClickButton);
    }


    private void OnClickButton()
    {
        GameRecordInfo.Instance.money += 10;
        ver.text = GameRecordInfo.Instance.money.ToString();
        GameRecordInfo.Instance.SaveToJSON(SaveGameRecordPath);
    }
    
}
