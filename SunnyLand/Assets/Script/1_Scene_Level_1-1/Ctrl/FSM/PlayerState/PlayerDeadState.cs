using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : State<PlayerControl>
{
    private static PlayerDeadState _instance;
    public static PlayerDeadState Instance
    {
        get
        {
            if (_instance == null)
                _instance = new PlayerDeadState();
            return _instance;
        }
    }

    private PlayerDeadState()
    {

    }

    public override void Enter()
    {
        owner.OnPlayerBeDead();
    }

    public override void Execute()
    {
        
    }

    public override void Exit()
    {
       
    }
}
