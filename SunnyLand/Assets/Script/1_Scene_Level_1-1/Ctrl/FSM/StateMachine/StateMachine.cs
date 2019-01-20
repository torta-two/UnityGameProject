using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    //当前状态
    public State<T> CurrentState { get; private set; }

    //上一个状态
    //public State<T> PreviousState { get; private set; }

    //全局状态
    public State<T> GlobalState { get; private set; }

    public StateMachine(T owner ,State<T> firstState, State<T> globalState = null)
    {    
        if(globalState != null)
        {
            GlobalState = globalState;
            GlobalState.owner = owner;
            GlobalState.Enter();
        }
        
        CurrentState = firstState;
        CurrentState.owner = owner;
        CurrentState.Enter();
    }

    public void ChangeState(State<T> targetState)
    {
        if(targetState == null)
            throw new System.Exception("Can't find target state!");

        targetState.owner = CurrentState.owner;
        CurrentState.Exit();
        CurrentState = targetState;
        CurrentState.Enter();

        //targetState.owner = CurrentState.owner;

        //PreviousState = CurrentState;
        //PreviousState.Exit();

        //CurrentState = targetState;
        //CurrentState.Enter();
    }

	public void SMUpDate()
    {
        if (GlobalState != null)
            GlobalState.Execute();
        if (CurrentState != null)
            CurrentState.Execute();
    }
}
