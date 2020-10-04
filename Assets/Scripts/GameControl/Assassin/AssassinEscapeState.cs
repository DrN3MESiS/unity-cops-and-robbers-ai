using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssassinEscapeState : AssassinBaseState<Assassin>
{
    string stateName = "AssassinEscapeState";

    // action to execute when enter the state
    public override void Enter(Assassin charac)
    {
        charac.currentState = 2;
        charac.ResetProperties();
        // Debug.Log("Entered State: " + stateName);
    }

    // is call by update miner function
    public override void Execute(Assassin charac)
    {
        if (charac.TargetFlee != null)
        {
            charac.vel_Flee = charac.Flee(charac.TargetFlee.transform.position);
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Flee");
        }

        if (charac.energyPoints <= 1)
        {
            charac.ChangeState(new AssassinHomeState());
        }
    }

    // execute when exit from state
    public override void Exit(Assassin charac)
    {
        charac.ResetProperties();

        // Debug.Log("\tLeft State: " + stateName);
    }
}
