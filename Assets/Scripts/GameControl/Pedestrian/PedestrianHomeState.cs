using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianHomeState : PedestrianBaseState<Pedestrian>
{
    string stateName = "PedestrianHomeState";
    bool timerHasStarted = false;
    bool timerHasFinished = false;

    IEnumerator Wait(Pedestrian charac)
    {
        // Debug.Log("Started Waiting in Home");
        yield return new WaitForSeconds(10f);
        charac.energyPoints = 40;
        timerHasFinished = true;
    }

    public override void Enter(Pedestrian charac)
    {
        charac.currentState = 1;
        enableState = false;
        charac.ResetProperties();
        // Debug.Log("Entered State: " + stateName);
        charac.TargetSeek = charac.FindClosestHouse();
        charac.OnSeek = true;
    }

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
                charac.ResetProperties();
                charac.ChangeState(new PedestrianSearchState());
            }
        }
    }

    public override void Exit(Pedestrian charac)
    {
        charac.ResetProperties();
        // Debug.Log("\tLeft State: " + stateName);
    }


}
