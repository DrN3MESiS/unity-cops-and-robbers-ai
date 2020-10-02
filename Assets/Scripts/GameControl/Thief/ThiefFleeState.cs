using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFleeState : ThiefBaseState<Thief>
{
    // action to execute when enter the state
    public override void Enter(Thief charac)
    {        
        charac.ResetProperties();        
    }

    // is call by update miner function
    public override void Execute(Thief charac)
    {
        if (charac.TargetFlee != null)
        {
            charac.vel_Flee = charac.Flee(charac.TargetFlee.transform.position);
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Flee");
        }
    }

    // execute when exit from state
    public override void Exit(Thief charac)
    {
        charac.ResetFlee();
    }
}
