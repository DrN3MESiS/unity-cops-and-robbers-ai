using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFleeState : ThiefBaseState<Thief>
{
    // action to execute when enter the state
    public override void Enter(Thief charac)
    {        
        charac.ResetProperties();
        charac.OnFlee = true;
    }

    // is call by update miner function
    public override void Execute(Thief charac)
    {
        
    }

    // execute when exit from state
    public override void Exit(Thief charac)
    {
        charac.ResetFlee();
        //StartPathFollow();
        charac.OnPathFollow = true;
    }
}
