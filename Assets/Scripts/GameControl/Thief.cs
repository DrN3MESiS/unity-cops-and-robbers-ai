using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : GlobalMovement
{
    public ThiefStateMachine<Thief> my_FSM;

    //*****************************************************
    public bool ChangeState(ThiefBaseState<Thief> newState)
    {
        my_FSM.ChangeState(newState);   
        return (true);
    }

    private void OnTriggerEnter(Collider obj)
    {
        string victimTag = obj.gameObject.tag;            
        switch (victimTag)
        {
            //Who am I colliding with
            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is colliding with " + victimTag);
                    ChangeState(new ThiefEvadeState());
                    this.TargetEvade = obj.gameObject;
                }
                break;
            case "Police":
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is colliding with " + victimTag);
                    ChangeState(new ThiefFleeState());
                    this.TargetFlee = obj.gameObject;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider obj)
    {
        string victimTag = obj.gameObject.tag;

        switch (victimTag)
        {
            //Who is no longer colliding with me
            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is no longer colliding with " + victimTag);
                    ChangeState(new ThiefGatheringState());
                }
                break;
            case "Police":
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is no longer colliding with " + victimTag);
                    ChangeState(new ThiefGatheringState());
                }
                break;
            default:
                break;
        }             
    }

    public override void StartState()
    {
        // initialize FSM
        my_FSM = new ThiefStateMachine<Thief>();
        my_FSM.SetOwner(this);

        my_FSM.Begin(new ThiefGatheringState());
    }
    public override void UpdateState()
    {
        my_FSM.UpdateMachine();
    }
}
