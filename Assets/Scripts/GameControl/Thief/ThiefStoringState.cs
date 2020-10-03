using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefStoringState : ThiefBaseState<Thief>
{
    // action to execute when enter the state
    public override void Enter(Thief charac)
    {
        
    }

    // is call by update miner function
    public override void Execute(Thief charac)
    {
        if (charac.TargetSeek != null)
        {
            if (Vector3.Distance(charac.TargetSeek.transform.position, charac.transform.position) > 1.0)
                charac.vel_Seek = charac.Seek(charac.TargetSeek.transform.position);
            else
                charac.vel_Seek = charac.vc_Velocity * -1.0f;
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Seek");
        }
    }

    // execute when exit from state
    public override void Exit(Thief charac)
    {
        charac.ResetSeek();
    }
}
