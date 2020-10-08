using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianStateMachine<T> where T : Pedestrian
{
    T Owner;

    public PedestrianBaseState<T> CurrentState;
    //**********************************************************************************************
    public void UpdateMachine()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute(Owner);
        }
    }
    //**********************************************************************************************

    public void SetOwner(T owner)
    {
        Owner = owner;
    }
    //**********************************************************************************************

    public bool ChangeState(PedestrianBaseState<T> newState)
    {
        //verify the state are valid
        if (newState == null)
        {
            return false;
        }
        // call the exit of the current state
        CurrentState.Exit(Owner);
        // change of state
        CurrentState = newState;
        // call the entry method of the newstate
        CurrentState.Enter(Owner);

        return true;
    }
    //**********************************************************************************************

    public bool Begin(PedestrianBaseState<T> initialST)
    {
        if (initialST == null)
        {
            return false;
        }
        CurrentState = initialST;
        CurrentState.Enter(Owner);
        return true;
    }
}
