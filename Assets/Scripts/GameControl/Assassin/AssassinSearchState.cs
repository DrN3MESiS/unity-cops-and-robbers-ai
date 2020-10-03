using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinSearchState : AssassinBaseState<Assassin>
{
    string stateName = "AssassinSearchState";
    // action to execute when enter the state
    public override void Enter(Assassin charac)
    {
        charac.ResetProperties();
        charac.currentState = 0;
        Debug.Log("Entered State: " + stateName);

    }

    public override void Execute(Assassin charac)
    {
        if (charac.energyPoints <= 1)
        {
            charac.ChangeState(new AssassinHomeState());
        }
        charac.OnWander = true;
    }

    public override void Exit(Assassin charac)
    {
        charac.ResetWander();
        charac.OnWander = false;
        Debug.Log("\tLeft State: " + stateName);
    }
}
