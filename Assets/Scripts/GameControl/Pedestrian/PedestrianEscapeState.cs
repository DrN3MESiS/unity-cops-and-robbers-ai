using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PedestrianEscapeState : PedestrianBaseState<Pedestrian>
{
    string stateName = "PedestrianEscapeState";

    // action to execute when enter the state
    public override void Enter(Pedestrian charac)
    {
        charac.currentState = 3;
        charac.ResetProperties();
        // Debug.Log("Entered State: " + stateName);
    }

    // is call by update miner function
    public override void Execute(Pedestrian charac)
    {
        if (charac.TargetFlee != null)
        {
            charac.vel_Flee = charac.Flee(charac.TargetFlee.transform.position);
            charac.OnFlee = true;
            charac.TargetSeek = charac.FindClosestPolice();
            charac.OnSeek = true;

        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Flee");
        }

        if (charac.energyPoints <= 1)
        {
            charac.ChangeState(new PedestrianHomeState());
        }
    }

    // execute when exit from state
    public override void Exit(Pedestrian charac)
    {
        charac.ResetProperties();

        // Debug.Log("\tLeft State: " + stateName);
    }
}
