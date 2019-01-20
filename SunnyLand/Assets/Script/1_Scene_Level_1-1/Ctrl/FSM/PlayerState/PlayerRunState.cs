using UnityEngine;

public class PlayerRunState : State<PlayerControl>
{
    private static PlayerRunState _instance;
    public static PlayerRunState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerRunState();
            return _instance;
        }
    }

    private PlayerRunState()
    {

    }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        owner.HorizontalMove();


        #region change state

        if (Mathf.Abs(owner.speedX) < 0.1f || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            owner.StateMachine.ChangeState(PlayerIdleState.Instance);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            owner.StateMachine.ChangeState(PlayerJumpState.Instance);
        }
        else if(owner.isLadderTop == false && Input.GetKey(KeyCode.DownArrow))
        {
            owner.StateMachine.ChangeState(PlayerCrouchState.Instance);
        }

        #endregion
    }

    public override void Exit()
    {

    }

    //private void Flip()
    //{
    //    Vector3 tempScale = owner.transform.localScale;
    //    tempScale.x *= -1;
    //    owner.transform.localScale = tempScale;

    //    owner.isFacingRight = !owner.isFacingRight;
    //}
}
