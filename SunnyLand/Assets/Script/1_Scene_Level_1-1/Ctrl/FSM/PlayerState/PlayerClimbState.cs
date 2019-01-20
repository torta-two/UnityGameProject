using UnityEngine;

public class PlayerClimbState:State<PlayerControl>
{
    private static PlayerClimbState _instance;
    public static PlayerClimbState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerClimbState();
            return _instance;
        }
    }

    private PlayerClimbState()
    {

    }

    public override void Enter()
    {
        owner.isGrounded = false;

        owner.speedY = owner.playerInfo.climbSpeed;
        owner.ladderTriggle.parent.Find("LadderTop").GetComponent<BoxCollider2D>().enabled = false;
    }

    public override void Execute()
    {
        //remove player's gravity
        owner.rgd2D.constraints |= RigidbodyConstraints2D.FreezePositionY;
                
        owner.CheckGrounded();
        owner.CheckLadderTopForLadder();


        #region control vertical move when player in the ladder

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector2 tempVec = owner.transform.position;
            tempVec.y += Time.deltaTime * owner.playerInfo.climbSpeed;
            owner.transform.position = tempVec;
            owner.speedY = owner.playerInfo.climbSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 tempVec = owner.transform.position;
            tempVec.y -= Time.deltaTime * owner.playerInfo.climbSpeed;
            owner.transform.position = tempVec;
            owner.speedY = owner.playerInfo.climbSpeed;
        }
        else
        {
            owner.speedY = 0;
        }

        #endregion


        if (owner.isGrounded)
        {
            owner.rgd2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            owner.StateMachine.ChangeState(PlayerIdleState.Instance);
        }        
    }

    public override void Exit()
    {
        owner.speedY = 0;
        owner.isClimb = false;

        owner.ladderTriggle.parent.Find("LadderTop").GetComponent<BoxCollider2D>().enabled = true;        
    }    
   
}
