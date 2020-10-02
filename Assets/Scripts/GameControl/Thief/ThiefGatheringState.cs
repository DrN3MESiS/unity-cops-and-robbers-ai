using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefGatheringState : ThiefBaseState<Thief>
{
    // action to execute when enter the state
    public override void Enter(Thief charac)
    {                
        charac.isGamePath = true;
    }

    // is call by update miner function
    public override void Execute(Thief charac)
    {
        if (charac.TargetPathFollow != null)
        {
            if (Vector3.Distance(charac.TargetPathFollow.position, charac.transform.position) > 1.0)
                charac.vel_Seek = charac.Path();
            //else
            //vel_Seek = vc_Velocity * -1.0f;
            if (charac.TargetPathFollow.withinRadius(charac.transform.position))
            {
                charac.jewls++;
                charac.needsToStore = charac.jewls >= 3;
                charac.StartPathFollow();
            }
        }
    }

    // execute when exit from state
    public override void Exit(Thief charac)
    {
        
    }
}
