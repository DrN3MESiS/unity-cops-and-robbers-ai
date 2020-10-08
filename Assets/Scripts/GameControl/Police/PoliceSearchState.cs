using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSearchState : PoliceBaseState<Police>
{
    string stateName = "PoliceSearchState";
   public override void Enter(Police charac)
    {
        charac.ResetProperties();
        charac.currentState = 1;

    }

    public override void Execute(Police charac)
    {
        charac.OnWander = true;
    }

    public override void Exit(Police charac)
    {
        charac.ResetProperties();
        GameObject.Destroy(charac.TargetWander);
    }
}
