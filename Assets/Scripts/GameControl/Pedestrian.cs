using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : GlobalMovement
{
    public int energyPoints = 40;
    public int hungry = 20;
    public int currentState = 0;
    public PedestrianStateMachine<Pedestrian> my_FSM;

    /*****************************************************/
    public bool ChangeState(PedestrianBaseState<Pedestrian> newState)
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
            case "Restaurant":
                if (currentState == 2)
                {
                    ResetProperties();
                    my_FSM.CurrentState.enableState = true;
                }
                break;
            case "Pedestrian House":
                if (currentState == 1)
                {
                    ResetProperties();
                    my_FSM.CurrentState.enableState = true;
                }
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
            case "Police":
                if (currentState != 0 && currentState != 1)
                {
                    //ResetProperties();
                    ChangeState(new PedestrianSearchState());
                }
                break;
            case "User":
                if (currentState != 0 && currentState != 1)
                {
                    //ResetProperties();
                    ChangeState(new PedestrianSearchState());
                }
                break;
            case "Assasin":
                if (currentState != 1)
                {
                    //ResetProperties();
                    ChangeState(new PedestrianEscapeState());
                    this.TargetFlee = obj.gameObject;

                }
                break;
            case "Thief":
                if (currentState != 1)
                {
                    //ResetProperties();
                    ChangeState(new PedestrianEscapeState());
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
            case "Assasin":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentState != 0 && currentState != 1 && currentState != 2)
                    {
                        // Debug.Log("Assassin is no longer colliding with " + victimTag);
                        ChangeState(new PedestrianSearchState());
                    }
                }
                break;
            default:
                break;
        }
    }

    IEnumerator HungryDiscount()
    {
        Debug.Log("Started Hungry Discount");

        while (true)
        {
            yield return new WaitForSeconds(1f);
            hungry -= 1;
        }

    }

    IEnumerator EnergyDiscount()
    {
        Debug.Log("Started Energy Discount");

        while (true)
        {
            yield return new WaitForSeconds(1f);
            energyPoints -= 1;
        }

    }
    public override void StartState()
    {
        // initialize FSM
        my_FSM = new PedestrianStateMachine<Pedestrian>();
        my_FSM.SetOwner(this);
        my_FSM.Begin(new PedestrianSearchState());
        StartCoroutine(EnergyDiscount());
        StartCoroutine(HungryDiscount());
    }
    public override void UpdateState()
    {
        my_FSM.UpdateMachine();
    }

    public GameObject FindClosestPolice()
    {
        GameObject[] allPolice;
        allPolice = GameObject.FindGameObjectsWithTag("Police");
        GameObject closestPolice = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject police in allPolice)
        {
            Vector3 diff = police.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPolice = police;
                distance = curDistance;
            }
        }
        return closestPolice;
    }
    public GameObject FindClosestRestaurant()
    {
        GameObject[] allRes;
        allRes = GameObject.FindGameObjectsWithTag("Restaurant");
        GameObject closestRes = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject res in allRes)
        {
            Vector3 diff = res.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestRes = res;
                distance = curDistance;
            }
        }
        return closestRes;
    }

    public GameObject FindClosestHouse()
    {
        GameObject[] allHouses;
        allHouses = GameObject.FindGameObjectsWithTag("Pedestrian House");
        GameObject closestHouse = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject house in allHouses)
        {
            Vector3 diff = house.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestHouse = house;
                distance = curDistance;
            }
        }
        return closestHouse;
    }
}