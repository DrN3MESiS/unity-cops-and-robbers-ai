using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : GlobalMovement
{
    public int energyPoints = 20;
    public int currentState = 4;
    public PoliceStateMachine<Police> my_FSM;

    /*****************************************************/
    public bool ChangeState(PoliceBaseState<Police> newState)
    {
        my_FSM.ChangeState(newState);
        return (true);
    }

    /*******************/
    private void OnCollisionEnter(Collision obj)
    {
        string myTag = gameObject.tag;
        string victimTag = obj.gameObject.tag;


        switch (victimTag)
        {
            //Who am I colliding with
            case "Assasin":
                if (currentState != 4 && currentState != 3)
                {
                    Destroy(obj.gameObject);
                    energyPoints -= 1;
                    ResetSeek();
                    ChangeState(new PoliceSearchState());
                }
                break;
             
            case "Pedestrian":
                    ResetProperties();
                    ChangeState(new PoliceSearchState());
                break;
            case "Thief":
                if (currentState != 4 && currentState != 3)
                {
                    Destroy(obj.gameObject);
                    energyPoints -= 1;
                    ResetSeek();
                    ChangeState(new PoliceSearchState());
                }
                break;
            case "coffe":
                    energyPoints = 4;
                    ResetProperties();
                    ChangeState(new PoliceSearchState());
                break;

            case "policeStation":
                    ResetProperties();
                    ChangeState(new PoliceSearchState());
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        string victimTag = obj.gameObject.tag;
        switch (victimTag)
        {
            //Who am I colliding with
            case "Assasin":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if ( currentState != 3 && energyPoints >= 0)
                    {
<<<<<<< HEAD
                    TargetPursuit = obj.gameObject;
                    ChangeState(new PoliceArrestState());
                    TargetPursuit = obj.gameObject;
=======
                        ChangeState(new PoliceArrestState());
                        this.TargetSeek = obj.gameObject;
>>>>>>> dbe29a4831c133a23bb940d89c34e32c53b71f07
                    }

                    if (energyPoints <= 0 && currentState == 1)
                    {
                        ChangeState(new PoliceCoffeState());
                    }
                }
                break;
            case "Thief":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentState != 3 && energyPoints >= 0)
                    {
<<<<<<< HEAD
                    TargetPursuit = obj.gameObject;
                    ChangeState(new PoliceArrestState());
                    TargetPursuit = obj.gameObject;
=======
                        ChangeState(new PoliceArrestState());
                        this.TargetSeek = obj.gameObject;
>>>>>>> dbe29a4831c133a23bb940d89c34e32c53b71f07
                    }

                    if (energyPoints <= 0 && currentState == 1)
                    {
                        ChangeState(new PoliceCoffeState());
                    }
                }
                break;

            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentState == 1)
                    {
                        if (obj.gameObject.GetComponent("OnFlee"))
                        {
                            //ResetProperties();
                            TargetSeek = obj.gameObject;
                            OnSeek = true;
                                    }
                    }
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
                    if (currentState == 2)
                    {
                    ChangeState(new PoliceSearchState());
                    }
                }
                break;
            /*case "Assasin":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentState == 2)
                    {
                    ChangeState(new PoliceSearchState());
                    }
                }
                break;
            case "Thief":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentState == 2)
                    {
                    ChangeState(new PoliceSearchState());
                    }
                }
                break;*/
            default:
                break;
        }
    }

    public override void StartState()
    {
        // initialize FSM
        my_FSM = new PoliceStateMachine<Police>();
        my_FSM.SetOwner(this);
        my_FSM.Begin(new PoliceWorkState());
    }

    public override void UpdateState()
    {
        my_FSM.UpdateMachine();
    }
        public GameObject FindClosestWork()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("policeStation");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public GameObject FindClosestCofee()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("coffe");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}


