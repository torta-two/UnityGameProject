using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckPlayerAttack : MonoBehaviour
{
    //[HideInInspector]
    public bool checkPlayerAttack = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player" && collision.collider.name == "PlayerAttack")
        {
            checkPlayerAttack = true;
        }
    }
}
