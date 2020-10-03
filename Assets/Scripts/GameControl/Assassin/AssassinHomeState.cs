using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinHomeState : AssassinBaseState<Assassin>
{
    string stateName = "AssassinHomeState";
    bool timerHasStarted = false;
    bool timerHasFinished = false;

    IEnumerator Wait(Assassin charac)
    {
        // Debug.Log("Started Waiting in Home");
        yield return new WaitForSeconds(10f);
        charac.energyPoints = 30;
        timerHasFinished = true;
    }

    public override void Enter(Assassin charac)
    {
        charac.currentState = 4;
        enableState = false;
        charac.ResetProperties();
        // Debug.Log("Entered State: " + stateName);
        charac.TargetSeek = charac.FindClosestHouse();
        charac.OnSeek = true;
    }

    public override void Execute(Assassin charac)
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
                charac.ChangeState(new AssassinSearchState());
            }
        }
    }

    public override void Exit(Assassin charac)
    {
        charac.ResetProperties();
        // Debug.Log("\tLeft State: " + stateName);
    }


}
