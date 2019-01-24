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
}
