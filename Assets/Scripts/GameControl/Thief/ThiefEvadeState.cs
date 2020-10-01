using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefEvadeState : ThiefBaseState<Thief>
{
    // action to execute when enter the state
    public override void Enter(Thief charac)
    {        
        charac.OnEvade = true;
    }

    // is call by update miner function
    public override void Execute(Thief charac)
    {
        
    }

    // execute when exit from state
    public override void Exit(Thief charac)
    {
        charac.ResetEvade();        
    }
}
