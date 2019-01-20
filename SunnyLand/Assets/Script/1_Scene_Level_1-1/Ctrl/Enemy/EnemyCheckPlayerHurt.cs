using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckPlayerHurt : MonoBehaviour
{
    [HideInInspector]
    public bool checkPlayerHurt = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            checkPlayerHurt = true;
        }
    }
}
