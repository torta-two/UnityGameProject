using UnityEngine;

public class PlayerIdleState : State<PlayerControl>
{
    private static PlayerIdleState _instance;
    public static PlayerIdleState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerIdleState();
            return _instance;
        }
    }

    private PlayerIdleState()
    {

    }

    public override void Enter()
    {

    }

    public override void Execute()
    {
        #region check Ground

        owner.CheckLadderTopForGround();
        owner.CheckGrounded();

        #endregion


        #region change state

        if (!owner.isEnding)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                owner.StateMachine.ChangeState(PlayerRunState.Instance);
            }
            else if (owner.isLadderTop == false && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow)))
            {
                owner.StateMachine.ChangeState(PlayerJumpState.Instance);
            }
            else if (owner.isLadderTop == false && Input.GetKey(KeyCode.DownArrow))
            {
                owner.StateMachine.ChangeState(PlayerCrouchState.Instance);
            }
        }
        #endregion
    }

    public override void Exit()
    {

    }
}
