using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianRestaurantState : PedestrianBaseState<Pedestrian>
{
    string stateName = "PedestrianRestaurantState";

    bool timerHasStarted = false;
    bool timerHasFinished = false;

    IEnumerator Wait(Pedestrian charac)
    {
        Debug.Log("Started eating in restaurant");
        yield return new WaitForSeconds(10f);
        charac.hungry = 20;
        timerHasFinished = true;
    }

    // action to execute when enter the state
    public override void Enter(Pedestrian charac)
    {
        charac.currentState = 2;
        enableState = false;
        charac.ResetProperties();
        // Debug.Log("Entered State: " + stateName);
        charac.TargetSeek = charac.FindClosestRestaurant();
        charac.OnSeek = true;
    }

    // is call by update miner function
    public override void Execute(Pedestrian charac)
    {
        if (enableState)
        {
            if (!timerHasStarted)
            {
                timerHasStarted = true;
                charac.StartCoroutine(Wait(charac));
            }
            if (timerHasFinished)
            {
                charac.ChangeState(new PedestrianSearchState());
            }
        }
    }

    // execute when exit from state
    public override void Exit(Pedestrian charac)
    {
        charac.ResetProperties();
        // Debug.Log("\tLeft State: " + stateName);
    }
}
