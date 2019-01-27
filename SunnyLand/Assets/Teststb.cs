using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teststb : MonoBehaviour
{
    public GameRecord gameRecord;

    private Text ver;
    private Button clickButton;

    public string SaveGameRecordPath => 
        System.IO.Path.Combine(Application.dataPath + "/Json/GameRecord");

    private void Awake()
    {
        if(System.IO.File.Exists(SaveGameRecordPath))
        {
            //GameRecord.LoadFromJSON(SaveGameRecordPath);
        }
        else
        {
            //GameRecord.InitializeFromDefault(gameRecord);
        }

        ver = GameObject.Find("Canvas/Panel/ver").GetComponent<Text>();

        ver.text = GameRecord.Instance.money.ToString();

        clickButton = GameObject.Find("Canvas/Click").GetComponent<Button>();
        clickButton.onClick.AddListener(OnClickButton);
    }


    private void OnClickButton()
    {
        GameRecord.Instance.money += 10;
        ver.text = GameRecord.Instance.money.ToString();
        GameRecord.Instance.Save(SaveGameRecordPath);
    }
    
}
