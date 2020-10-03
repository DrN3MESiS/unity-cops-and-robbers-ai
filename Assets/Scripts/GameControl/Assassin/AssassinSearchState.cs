using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinSearchState : AssassinBaseState<Assassin>
{
    // action to execute when enter the state
    public override void Enter(Assassin charac)
    {
        charac.ResetProperties();
    }

    public override void Execute(Assassin charac)
    {

        if (charac.TargetWander != null)
        {
            charac.OnWander = true;
        }
        else
        {
            Debug.Log("No hay un Target seleccionado para hacer Wander");
        }
    }

    public override void Exit(Assassin charac)
    {
        charac.ResetFlee();
        charac.OnWander = false;
    }
}
