﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1;
    public float patrolRadius = 5;

    public AudioSource audioSource;

    private Ctrl ctrl;
    private Animator anim;
    


    private bool isFacingRight = false;
    private Vector2 startPoint;
    private Enemy_CheckDeadEnemy checkPlayerAttack;
    private Enemy_CheckHurtPlayer checkPlayerHurt;

    
    

    //private readonly Vector3 colliderOffset = new Vector3(0.07f, 0, 0);
    
    private void Awake()
    {        
        checkPlayerAttack = GetComponentInChildren<Enemy_CheckDeadEnemy>();
        checkPlayerHurt = GetComponentInChildren<Enemy_CheckHurtPlayer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        ctrl = FindObjectOfType<Ctrl>();

        startPoint = transform.position;
        patrolRadius += Random.Range(0, 2);
        float r = Random.Range(0, 2);
        if(r == 0)
        {
            Flip();
        }
    }

    private void Update()
    {
        //#region remove collider's offset when enemy flip

        //if (GetComponent<SpriteRenderer>().flipX)
        //{
        //    checkPlayerAttack.transform.localPosition = colliderOffset;
        //    checkPlayerHurt.transform.localPosition = colliderOffset;
        //}
        //else
        //{
        //    checkPlayerAttack.transform.localPosition = Vector3.zero;
        //    checkPlayerHurt.transform.localPosition = Vector3.zero;
        //}

        //#endregion

        if (isFacingRight)
        {
            Vector2 tempVec = transform.position;
            tempVec.x += Time.deltaTime * speed;

            if(tempVec.x <= startPoint.x + patrolRadius)
                transform.position = tempVec;
            else
                Flip();
        }
        else
        {
            Vector2 tempVec = transform.position;
            tempVec.x -= Time.deltaTime * speed;

            if (tempVec.x >= startPoint.x - patrolRadius)
                transform.position = tempVec;
            else
                Flip();
        }

    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        //GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        isFacingRight = !isFacingRight;
    }

    public Enemy CheckDeadEnemy()
    {
        Enemy deadEnemy = null;

        if (checkPlayerAttack.checkDeadEnemy)
        {
            deadEnemy = this;
        }

        return deadEnemy;
    }

    public void CheckPlayBeAttacked(PlayerControl player)
    {
        if (player.isPlayHurtAnim)
        {
            checkPlayerHurt.checkHurtPlayer = false;
        }

        if (!player.isPlayHurtAnim && checkPlayerHurt.checkHurtPlayer)
        {
            ctrl.audioManager.Play(ctrl.audioManager.hurt, audioSource);
            player.isHurt = true;
            checkPlayerHurt.checkHurtPlayer = false;
        }
    }

    public Enemy CheckDeadEnemyAndPlayerHurt(PlayerControl player)
    {
        Enemy deadEnemy = null;

        if (checkPlayerAttack.checkDeadEnemy)
        {
            deadEnemy = this;
        }

        if (player.isPlayHurtAnim)
        {
            checkPlayerHurt.checkHurtPlayer = false;
        }

        if (!player.isPlayHurtAnim && checkPlayerHurt.checkHurtPlayer)
        {
            ctrl.audioManager.Play(ctrl.audioManager.hurt, audioSource);
            player.isHurt = true;
            checkPlayerHurt.checkHurtPlayer = false;
        }

        return deadEnemy;
    }

    public void OnEnemyDead()
    {
        checkPlayerAttack.gameObject.SetActive(false);
        checkPlayerHurt.gameObject.SetActive(false);

        ctrl.audioManager.Play(ctrl.audioManager.attack, audioSource);
        anim.SetBool("isDead", true);
        
        enabled = false;
        Destroy(gameObject, 0.6f);
    }
}
