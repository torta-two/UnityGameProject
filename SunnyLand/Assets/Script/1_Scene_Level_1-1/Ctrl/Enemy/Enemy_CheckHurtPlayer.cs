using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CheckHurtPlayer : MonoBehaviour
{
    [HideInInspector]
    public bool checkHurtPlayer = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            checkHurtPlayer = true;
        }
    }
}
