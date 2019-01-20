using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1;
    public float patrolRadius = 5;
    private bool isFacingRight = false;
    private Vector2 startPoint;
    private EnemyCheckPlayerAttack checkPlayerAttack;
    private EnemyCheckPlayerHurt checkPlayerHurt;
    private Animator anim;
    private AudioSource audioSource;
    private Ctrl ctrl;

    private readonly Vector3 colliderOffset = new Vector3(0.07f, 0, 0);

    private void Start()
    {        
        checkPlayerAttack = GetComponentInChildren<EnemyCheckPlayerAttack>();
        checkPlayerHurt = GetComponentInChildren<EnemyCheckPlayerHurt>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        ctrl = FindObjectOfType<Ctrl>();

        startPoint = transform.position;
        patrolRadius += Random.Range(0, 2);
        float r = Random.Range(0, 2);
        if(r == 0)
            Flip();
    }

    private void Update()
    {
        #region remove collider's offset when enemy flip

        if (GetComponent<SpriteRenderer>().flipX)
        {
            checkPlayerAttack.transform.localPosition = colliderOffset;
            checkPlayerHurt.transform.localPosition = colliderOffset;
        }
        else
        {
            checkPlayerAttack.transform.localPosition = Vector3.zero;
            checkPlayerHurt.transform.localPosition = Vector3.zero;
        }

        #endregion

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
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        isFacingRight = !isFacingRight;
    }

    public EnemyMove CheckDeadEnemyAndPlayerHurt(PlayerControl player)
    {
        EnemyMove deadEnemy = null;

        if (checkPlayerAttack.checkPlayerAttack)
        {
            deadEnemy = this;
        }

        if (player.isPlayHurtAnim)
        {
            checkPlayerHurt.checkPlayerHurt = false;
        }

        if (!player.isPlayHurtAnim && checkPlayerHurt.checkPlayerHurt)
        {
            ctrl.audioManager.Play(ctrl.audioManager.hurt, audioSource);
            player.isHurt = true;
            checkPlayerHurt.checkPlayerHurt = false;
        }

        return deadEnemy;
    }

    public void EnemyFuneral()
    {
        checkPlayerAttack.gameObject.SetActive(false);
        checkPlayerHurt.gameObject.SetActive(false);        
        anim.SetBool("isDead", true);
        enabled = false;
        Destroy(gameObject, 0.6f);
    }
}
