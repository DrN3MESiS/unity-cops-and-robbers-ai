using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinKillState : AssassinBaseState<Assassin>
{
    string stateName = "AssassinKillState";

    // action to execute when enter the state
    public override void Enter(Assassin charac)
    {
        charac.ResetProperties();
        charac.currentState = 1;
        if (charac.knifeAmmo <= 0)
        {
            charac.ChangeState(new AssassinStoreState());
        }
        Debug.Log("Entered State: " + stateName);
    }

    // is call by update miner function
    public override void Execute(Assassin charac)
    {

        if (charac.TargetSeek != null)
        {
            charac.vel_Seek = charac.Seek(charac.TargetSeek.transform.position);
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Seek");
        }

    }

    // execute when exit from state
    public override void Exit(Assassin charac)
    {
        charac.ResetSeek();
        Debug.Log("\tLeft State: " + stateName);
    }
}
