using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceWorkState : PoliceBaseState<Police>
{
    string stateName = "PoliceWorkState";

    public override void Enter(Police charac)
    {
        charac.currentState = 4;
        charac.ResetProperties();
        charac.TargetSeek = charac.FindClosestWork();
        charac.OnSeek = true;
    }
    public override void Execute(Police charac)
    {
      
    }

       public override void Exit(Police charac)
    {
        charac.ResetProperties();
    }

}
