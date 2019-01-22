using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool checkCoin = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<CircleCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            checkCoin = true;           
        }
    }

    public void OnGetCoins()
    {
        anim.SetBool("checkReward", true);
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
        checkCoin = false;
    }
}
