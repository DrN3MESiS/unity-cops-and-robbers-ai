using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSearchState : PedestrianBaseState<Pedestrian>
{
    string stateName = "PedestrianSearchState";
    // action to execute when enter the state
    public override void Enter(Pedestrian charac)
    {
        charac.ResetProperties();
        charac.currentState = 0;
        // Debug.Log("Entered State: " + stateName);

    }

    public override void Execute(Pedestrian charac)
    {
        charac.OnWander = true;
        if (charac.energyPoints <= 1)
        {
            charac.ChangeState(new PedestrianHomeState());
        }
        if (charac.hungry <= 1)
        {
            charac.ChangeState(new PedestrianRestaurantState());
        }
    }

    public override void Exit(Pedestrian charac)
    {
        charac.ResetProperties();

        GameObject.Destroy(charac.TargetWander);
        // Debug.Log("\tLeft State: " + stateName);
    }
}
