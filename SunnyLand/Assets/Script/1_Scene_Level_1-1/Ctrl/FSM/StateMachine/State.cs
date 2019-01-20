using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T>
{
    public T owner;

    public abstract void Enter();

    public abstract void Execute();

    public abstract void Exit();  
}
