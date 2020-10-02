using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : GlobalMovement
{
    public ThiefStateMachine<Thief> my_FSM;
    public static GameObject thiefStorage;
    public static int thiefDiamonds = 0;
    public bool needsToStore = false;

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
                    ChangeState(new ThiefEvadeState());
                    this.TargetEvade = obj.gameObject;
                }
                break;
            case "Police":
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    ChangeState(new ThiefFleeState());
                    this.TargetFlee = obj.gameObject;
                }
                break;
            case "ThiefStorage":
                thiefDiamonds -= this.jewls;
                this.jewls = 0;
                ChangeState(new ThiefGatheringState());
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
                if (obj.GetType() == typeof(CapsuleCollider)){
                    if(this.jewls >= 3){
                        ChangeState(new ThiefStoringState());
                        this.TargetSeek = thiefStorage;
                    } else {
                        ChangeState(new ThiefGatheringState());
                    }
                }
                break;
            case "Police":
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider)){
                    if(this.jewls >= 3){
                        ChangeState(new ThiefStoringState());
                        this.TargetSeek = thiefStorage;
                    } else {
                        ChangeState(new ThiefGatheringState());
                    }
                }
                break;
            default:
                break;
        }             
    }

    public override void StartState()
    {
        thiefStorage = GameObject.FindGameObjectWithTag("ThiefStorage");
        defaultSpeed = 1.3f;
        StartPathFollow();
        // initialize FSM
        my_FSM = new ThiefStateMachine<Thief>();
        my_FSM.SetOwner(this);

        my_FSM.Begin(new ThiefGatheringState());
    }
    public override void UpdateState()
    {
        my_FSM.UpdateMachine();
        if(this.needsToStore){
            ChangeState(new ThiefStoringState());
            this.TargetSeek = thiefStorage;
            this.needsToStore = false;
        }
    }
}
