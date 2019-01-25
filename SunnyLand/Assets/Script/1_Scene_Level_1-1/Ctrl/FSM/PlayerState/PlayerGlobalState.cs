using UnityEngine;

public class PlayerGlobalState : State<PlayerControl>
{
    private static PlayerGlobalState _instance;
    public static PlayerGlobalState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerGlobalState();
            return _instance;
        }
    }

    private PlayerGlobalState()
    {

    }
  
    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        if (!owner.isPlayHurtAnim && owner.isHurt)
        {
            owner.OnPlayerBeHurt_Player();
        }

        if(owner.isPassLevel)
        {
            owner.OnPassLevel();
        }

        if(owner.HP <= 0)
        {
            if(owner.StateMachine.CurrentState != PlayerDeadState.Instance)
                owner.StateMachine.ChangeState(PlayerDeadState.Instance);
        }
    }

    public override void Exit()
    {

    }
}
