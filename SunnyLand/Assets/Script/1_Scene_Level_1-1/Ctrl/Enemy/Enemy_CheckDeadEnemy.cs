using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CheckDeadEnemy : MonoBehaviour
{
    [HideInInspector]
    public bool checkDeadEnemy = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && collision.collider.name == "PlayerAttack")
        {
            checkDeadEnemy = true;
        }
    }
}
