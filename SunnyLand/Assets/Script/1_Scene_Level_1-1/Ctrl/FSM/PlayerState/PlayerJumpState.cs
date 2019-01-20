using UnityEngine;

public class PlayerJumpState : State<PlayerControl>
{
    private static PlayerJumpState _instance;
    public static PlayerJumpState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerJumpState();
            return _instance;
        }
    }

    private PlayerJumpState()
    {

    }

    public override void Enter()
    {
        owner.ctrl.audioManager.Play(owner.ctrl.audioManager.jump, owner.audioSource);

        owner.rgd2D.AddForce(owner.playerInfo.jumpForce * Vector2.up);
       
        owner.isJump = true;
        owner.isGrounded = false;      
    }

    public override void Execute()
    {
        #region check Grounded
        
        owner.CheckGrounded();
        owner.CheckLadderTopForLadder();

        if(owner.isGrounded)
        {
            owner.StateMachine.ChangeState(PlayerIdleState.Instance);
        }

        #endregion


        #region check ladder

        owner.CheckLadderTriggleForJumpState();

        if(owner.isClimb)
        {
            owner.StateMachine.ChangeState(PlayerClimbState.Instance);
        }

        #endregion
    }

    public override void Exit()
    {
        //Clear the rest of force
        owner.rgd2D.Sleep();
        owner.isJump = false;
    }   
}
