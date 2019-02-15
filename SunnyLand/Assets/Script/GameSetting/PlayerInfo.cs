using System;
using UnityEngine;

[CreateAssetMenu(menuName = "MyMenu/PlayerInfo")]
[Serializable]
public class PlayerInfo : ScriptableObject
{
    public string playerName;

    public int price;

    public bool isPurchase;

    public bool isSelect;

    public int maxHP;

    public float maxSpeed;

    public float jumpForce;

    [Range(0, 1)]
    public float crouchSpeedFactor;

    public float climbSpeed;

    public LayerMask Ground;
    public LayerMask Ladder;

    private string playerInfoJsonPath;

    private void OnEnable()
    {
        playerInfoJsonPath = Application.persistentDataPath + "/json_PlayerInfo_" + playerName + ".json";

        if (System.IO.File.Exists(playerInfoJsonPath))
        {
            JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(playerInfoJsonPath), this);
        }
        else
        {
            System.IO.File.WriteAllText(playerInfoJsonPath, "");
        }
    }

    private void OnDisable()
    {
        System.IO.File.WriteAllText(playerInfoJsonPath, JsonUtility.ToJson(this, true));
    }

}
