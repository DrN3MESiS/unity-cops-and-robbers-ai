using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase<T> where T : CharacClass
{
    public T charac;
    public void GetPlayer(T val1)
    {
        charac = val1;
        return;
    }

    public bool enableState = false;
    // action to execute when enter the state
    public abstract void Enter(T charac);

    // is call by update miner function
    public abstract void Execute(T charac);

    // execute when exit from state
    public abstract void Exit(T charac);
        
}
