using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinKillState : AssassinBaseState<Assassin>
{
    // action to execute when enter the state
    public override void Enter(Assassin charac)
    {
        charac.ResetProperties();
    }

    // is call by update miner function
    public override void Execute(Assassin charac)
    {
        if (charac.TargetSeek != null)
        {
            charac.vel_Seek = charac.Seek(charac.TargetSeek.transform.position);
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Seek");
        }
    }

    // execute when exit from state
    public override void Exit(Assassin charac)
    {
        charac.ResetSeek();
    }
}
