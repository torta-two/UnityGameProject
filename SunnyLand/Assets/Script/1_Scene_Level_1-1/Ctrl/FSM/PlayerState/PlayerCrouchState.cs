using UnityEngine;

public class PlayerCrouchState : State<PlayerControl>
{
    private static PlayerCrouchState _instance;
    public static PlayerCrouchState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerCrouchState();
            return _instance;
        }
    }

    Collider2D[] colliders = null;

    private PlayerCrouchState()
    {
        
    }

    public override void Enter()
    {
        owner.isCrouch = true;

        if(colliders == null)
            colliders = owner.GetComponents<BoxCollider2D>();

        foreach (var item in colliders)
            item.enabled = false;
    }

    public override void Execute()
    {
        owner.HorizontalMove();

        if(!Input.GetKey(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            owner.StateMachine.ChangeState(PlayerIdleState.Instance);
        }
    }

    public override void Exit()
    {
        owner.isCrouch = false;
        foreach (var item in colliders)
            item.enabled = true;
    }
}
