using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCoffeState :PoliceBaseState<Police>
{
    string stateName = "PoliceCoffeState";

    public override void Enter(Police charac)
    {
        charac.ResetProperties();
        charac.TargetSeek = charac.FindClosestCofee();
        charac.OnSeek = true;
    }
    public override void Execute(Police charac)
    {
      
    }

       public override void Exit(Police charac)
    {   
        charac.currentState = 4;
        charac.ResetProperties();
    }

}