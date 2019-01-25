using System.Collections;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    private StateMachine<PlayerControl> _stateMachine;
    public StateMachine<PlayerControl> StateMachine
    {
        get
        {
            if (_stateMachine == null)
                _stateMachine = new StateMachine<PlayerControl>(this, PlayerIdleState.Instance, PlayerGlobalState.Instance);
            return _stateMachine;
        }
    }

    public PlayerInfo playerInfo;

    [HideInInspector]
    public Ctrl ctrl;

    [HideInInspector]
    public Rigidbody2D rgd2D;

    [HideInInspector]
    public AudioSource audioSource;

    [HideInInspector]
    public Animator anim;       
    
    [HideInInspector]
    public int HP;

    [HideInInspector]
    public float speedX;

    [HideInInspector]
    public float speedY;

    [HideInInspector]
    public Transform ladderTriggle;

    [HideInInspector]
    public bool isGrounded = true;

    [HideInInspector]
    public bool isJump = false;

    [HideInInspector]
    public bool isClimb = false;

    [HideInInspector]
    public bool isCrouch = false;

    [HideInInspector]
    public bool isHurt = false;

    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public bool isPassLevel = false;

    [HideInInspector]
    public bool isPlayHurtAnim = false;

    [HideInInspector]
    public bool isLadderTop = false;

    private bool isFacingRight = true;
   
    private Transform groundCheck;
    private readonly float groundRadius = 0.1f;
    private Transform ladderCheck;
    private readonly float ladderRadius = 0.6f;

    private void Awake()
    {
        ctrl = transform.parent.GetComponent<Ctrl>();
        rgd2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        groundCheck = transform.Find("GroundCheck");
        ladderCheck = transform.Find("LadderCheck");
    }

    private void Start()
    {
        HP = playerInfo.maxHP;
    }

    private void FixedUpdate()
    {
        StateMachine.SMUpDate();

        anim.SetFloat("SpeedX", Mathf.Abs(rgd2D.velocity.x));
        anim.SetFloat("SpeedY", speedY);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isJump", isJump);
        anim.SetBool("isClimb", isClimb);
        anim.SetBool("isLadderTop", isLadderTop);
        anim.SetBool("isCrouch", isCrouch);
        anim.SetBool("isEnding", isPassLevel);
    }

    //Check is Grounded or not in generally
    public void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, playerInfo.Ground);
        foreach (var item in colliders)
        {
            if (item != gameObject && item.tag != "CheckLadderTop")
            {
                isGrounded = true;
                isLadderTop = false;
            }
        }
    }

    //Check is it necessary to change to ClimbState in the IdleState  
    public void CheckLadderTopForGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, playerInfo.Ground);
        foreach (var item in colliders)
        {
            if (item.tag == "CheckLadderTop")
            {
                isLadderTop = true;
                ladderTriggle = item.transform.parent.Find("LadderTrigger");

                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    //Let groundCheck(a gameObject,player's childObject) far from CheckLadderTopForGround
                    //let player at the middle of the ladder
                    Vector2 newVec = transform.position;
                    newVec.x = ladderTriggle.position.x;
                    newVec.y -= 0.5f;
                    transform.position = newVec;

                    isClimb = true;
                    StateMachine.ChangeState(PlayerClimbState.Instance);
                }
            }
        }
    }

    //Help change to IdleState when player in the top of ladder 
    public void CheckLadderTopForLadder()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, playerInfo.Ladder);
        foreach (var item in colliders)
        {
            //Check Player is already in the top of ladder
            if (item.tag == "CheckLadderTop")
            {
                isLadderTop = true;
                isGrounded = true;
            }
        }
    }

    //Check is it necessary to change to ClimbState in the JumpState  
    public void CheckLadderTriggleForJumpState()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(ladderCheck.position, ladderRadius, playerInfo.Ladder);
        foreach (var item in colliders)
        {
            if (item.tag == "LadderTrigger")
            {
                ladderTriggle = item.transform;

                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
                {
                    isClimb = true;

                    //let player at the middle of the ladder
                    Vector3 newVec = transform.position;
                    newVec.x = item.transform.position.x;
                    transform.position = newVec;
                }
            }
        }
    }

    //Control player' Horizontal move when state is Run and Crouch
    public void HorizontalMove()
    {
        float moveFactor = 1; 

        if(StateMachine.CurrentState == PlayerRunState.Instance)
        {
            moveFactor = 1;
        }
        else if (StateMachine.CurrentState == PlayerCrouchState.Instance)
        {
            moveFactor = playerInfo.crouchSpeedFactor;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speedX = -playerInfo.maxSpeed * moveFactor;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            speedX = playerInfo.maxSpeed * moveFactor;
        }
        else
        {
            speedX = 0;
        }

        rgd2D.velocity = new Vector2(speedX, rgd2D.velocity.y);

        if (speedX > 0 && isFacingRight == false)
        {
            HorizontalFilp();
        }
        else if (speedX < 0 && isFacingRight == true)
        {
            HorizontalFilp();
        }
    }

    //Control player' horizontal flip when state is Run and Crouch
    private void HorizontalFilp()
    {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        isFacingRight = !isFacingRight;
    }
   
    public void OnPassLevel()
    {
        rgd2D.velocity = Vector2.zero;
        anim.SetFloat("SpeedX", 0);
        anim.SetFloat("SpeedY", 0);
        rgd2D.constraints |= RigidbodyConstraints2D.FreezePositionX;
    }

    public void OnPlayerBeHurt_Player()
    {
        HP--;
        isPlayHurtAnim = true;
        isHurt = false;
        StartCoroutine(HurtAnim());
    }

    //Player's hurt Animation with Coroutine
    //Player can't hurt when the hurt Animation play
    public IEnumerator HurtAnim()
    {
        Vector4 tempColor = GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 20; i++)
        {
            if (i % 2 == 0)
                tempColor.w = 0.3f;
            else
                tempColor.w = 1f;

            GetComponent<SpriteRenderer>().color = tempColor;
            yield return new WaitForSeconds(0.2f);
        }

        StopCoroutine(HurtAnim());

        isPlayHurtAnim = false;
    }

    public void OnPlayerBeDead()
    {
        ctrl.audioManager.Play(ctrl.audioManager.dead, audioSource);
        isDead = true;
        StopAllCoroutines();
        Vector4 tempColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Vector4(tempColor.x,tempColor.y,tempColor.z,1);
        anim.SetLayerWeight(1, 1);
    }
}
