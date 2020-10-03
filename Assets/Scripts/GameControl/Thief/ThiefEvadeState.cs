using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefEvadeState : ThiefBaseState<Thief>
{
    // action to execute when enter the state
    public override void Enter(Thief charac)
    {        
        
    }

    // is call by update miner function
    public override void Execute(Thief charac)
    {
        if (charac.TargetEvade != null)
        {
            charac.vel_Evade = charac.Evade(charac.TargetEvade);
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Evade");
        }
    }

    // execute when exit from state
    public override void Exit(Thief charac)
    {
        charac.ResetEvade();        
    }
}
