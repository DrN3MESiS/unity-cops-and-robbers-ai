using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceArrestState : PoliceBaseState<Police>
{
    string stateName = "PoliceArrestState";

    // action to execute when enter the state
    public override void Enter(Police charac)
    {
        charac.currentState = 2;
        if (charac.energyPoints <= 0)
        {
            enableState = false;
            charac.ChangeState(new PoliceCoffeState());
        }
        else
        {
            enableState = true;
        }

        // Debug.Log("Entered State: " + stateName);
    }

    // is call by update miner function
    public override void Execute(Police charac)
    {
        if (charac.energyPoints <= 0)
        {
        charac.ChangeState(new PoliceCoffeState());
        }else
        {   
                charac.OnSeek = false;
                charac.OnWander = false;
                charac.OnPursuit = true;
            }
            
    }

    // execute when exit from state
    public override void Exit(Police charac)
    {
        charac.ResetProperties();
    }
}