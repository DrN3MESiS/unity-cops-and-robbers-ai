using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinStoreState : AssassinBaseState<Assassin>
{
    string stateName = "AssassinStoreState";

    bool timerHasStarted = false;
    bool timerHasFinished = false;

    IEnumerator Wait()
    {
        Debug.Log("Started Waiting in Home");
        yield return new WaitForSeconds(5f);
        charac.knifeAmmo = 2;
        timerHasFinished = true;
    }

    // action to execute when enter the state
    public override void Enter(Assassin charac)
    {
        charac.currentState = 3;
        enableState = false;
        charac.ResetProperties();
        Debug.Log("Entered State: " + stateName);
    }

    // is call by update miner function
    public override void Execute(Assassin charac)
    {
        if (enableState)
        {
            if (!timerHasStarted)
            {
                timerHasStarted = true;
                charac.StartCoroutine(Wait());
            }
            if (timerHasFinished)
            {
                charac.ChangeState(new AssassinSearchState());
            }
        }
    }

    // execute when exit from state
    public override void Exit(Assassin charac)
    {
        charac.ResetFlee();
        Debug.Log("\tLeft State: " + stateName);
    }
}
