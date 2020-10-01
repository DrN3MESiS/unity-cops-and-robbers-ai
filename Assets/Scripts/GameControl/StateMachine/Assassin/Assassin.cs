using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : GlobalMovement
{
    public int knifeAmmo = 2;
    public int energyPoints = 20;

    public int currentStateCode = 0;

    /*Action Reference*/
    public bool imBeingChasedByPolice = false;
    public bool imTryingToKillAPedestrian = false;
    public bool iNeedSleep = false;
    public bool imSleeping = false;

    public bool iNeedAmmo = false;
    public bool buyingAmmo = false;

    /*Physical Places*/
    public GameObject gunStore;
    public GameObject house;

    IEnumerator EnergyDiscount()
    {
        Debug.Log("Started Energy Discount");
        while (true)
        {
            if (!imSleeping)
            {
                Debug.Log("Discounted 1");
                yield return new WaitForSeconds(1f);
                energyPoints -= 1;
            }
        }
    }
    IEnumerator HouseTimeout()
    {
        iNeedSleep = false;
        imSleeping = true;
        yield return new WaitForSeconds(10f);
        imSleeping = false;
        energyPoints = 20;
        StateZero_CivilSearching();
    }
    IEnumerator GunStoreTimeout()
    {
        iNeedAmmo = false;
        buyingAmmo = true;
        yield return new WaitForSeconds(5f);
        buyingAmmo = false;
        StateZero_CivilSearching();
    }

    IEnumerator ActionChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (knifeAmmo <= 0 && !iNeedAmmo && !buyingAmmo)
            {
                Debug.Log("Need ammo!");
                ResetProperties();
                iNeedAmmo = true;
                // StateThree_GunStore();
            }
            else if (energyPoints < 1 && !iNeedSleep && !imSleeping)
            {
                Debug.Log("Need Sleep!");
                ResetProperties();
                iNeedSleep = true;
                // StateFour_Home();
            }
            Debug.Log("Checking...");
        }
    }
    void Start()
    {
        this.defaultSpeed = 1.3f;
        StartCoroutine(EnergyDiscount());
        StateZero_CivilSearching();
        StartCoroutine(ActionChecker());
    }


    private void OnCollisionEnter(Collision obj)
    {
        string victimTag = obj.gameObject.tag;
        switch (victimTag)
        {
            //Who am I colliding with
            case "Pedestrian":
                if (currentStateCode == 1)
                {
                    if (knifeAmmo <= 0)
                    {
                        //Actions
                        iNeedAmmo = true;
                        //Next State
                        StateThree_GunStore();
                    }

                    if (!iNeedAmmo && !iNeedSleep && !imBeingChasedByPolice)
                    {
                        //Actions
                        Destroy(obj.gameObject);
                        knifeAmmo -= 1;
                        imTryingToKillAPedestrian = false;
                        //Next State
                        StateZero_CivilSearching();
                    }
                }
                break;
            case "Assassin Home":
                if (currentStateCode == 4)
                {
                    if (iNeedSleep)
                    {
                        StartCoroutine(HouseTimeout());
                    }
                }
                break;
            case "Assassin GunStore":
                if (currentStateCode == 3)
                {
                    if (iNeedAmmo)
                    {
                        StartCoroutine(GunStoreTimeout());
                    }
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
            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    //Check current State
                    if (currentStateCode == 0)
                    {
                        if (!imTryingToKillAPedestrian && !imBeingChasedByPolice)
                        {
                            //Next State
                            StateOne_CivilKilling(obj.gameObject);
                        }
                    }
                }
                break;
            case "Police":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    //Check current State
                    if (currentStateCode == 0 || currentStateCode == 1 || currentStateCode == 3)
                    {
                        if (!iNeedSleep)
                        {
                            //Next State
                            StateTwo_EscapePolice(obj.gameObject);
                        }
                    }
                }
                break;
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    //Check current State
                    if (currentStateCode == 0 || currentStateCode == 1 || currentStateCode == 3)
                    {
                        if (!iNeedSleep)
                        {
                            //Next State
                            StateTwo_EscapePolice(obj.gameObject);
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
        string myTag = gameObject.tag;
        string victimTag = obj.gameObject.tag;
        switch (victimTag)
        {
            //Who is no longer colliding with me
            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentStateCode == 1)
                    {
                        StateZero_CivilSearching();
                    }
                }
                break;
            case "Police":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentStateCode == 2)
                    {
                        StateZero_CivilSearching();
                    }
                }
                break;
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    if (currentStateCode == 2)
                    {
                        StateZero_CivilSearching();
                    }
                }
                break;
            default:
                break;
        }
    }


    private void StateZero_CivilSearching()
    {
        currentStateCode = 0;
        imBeingChasedByPolice = false;
        imTryingToKillAPedestrian = false;
        iNeedSleep = false;
        imSleeping = false;
        iNeedAmmo = false;
        buyingAmmo = false;

        ResetProperties();
        if (knifeAmmo <= 0)
        {
            StateThree_GunStore();
        }
        OnWander = true;
    }

    private void StateOne_CivilKilling(GameObject target)
    {
        currentStateCode = 1;
        imTryingToKillAPedestrian = true;

        ResetProperties();
        TargetSeek = target;
        OnSeek = true;

    }

    private void StateTwo_EscapePolice(GameObject target)
    {
        currentStateCode = 2;
        imTryingToKillAPedestrian = false;
        iNeedSleep = false;
        iNeedAmmo = false;
        imSleeping = false;
        buyingAmmo = false;
        imBeingChasedByPolice = true;

        ResetProperties();
        TargetFlee = target;
        OnFlee = true;
    }

    private void StateThree_GunStore()
    {
        currentStateCode = 3;
        imBeingChasedByPolice = false;
        imTryingToKillAPedestrian = false;
        //Store: gunStore
        //Once arrives to GunStore
        // GunStoreTimeout();
    }

    private void StateFour_Home()
    {
        currentStateCode = 4;
        imSleeping = true;

        //Home: house
        //Once arrives to Home
        // HouseTimeout();
    }
}

//     private void OnCollisionEnter(Collision obj)
//     {
//         string myTag = gameObject.tag;
//         string victimTag = obj.gameObject.tag;

//         switch (myTag)
//         {
//             case "Assassin":
//                 switch (victimTag)
//                 {
//                     //Who am I colliding with
//                     case "Pedestrian":
//                         Destroy(obj.gameObject);
//                         ResetSeek();
//                         OnWander = true;
//                         break;
//                     case "Police":
//                         if (obj.GetType() == typeof(CapsuleCollider))
//                         {
//                             Debug.Log(myTag + " is colliding with " + victimTag);
//                         }
//                         break;
//                     case "Assassin":
//                         if (obj.GetType() == typeof(CapsuleCollider))
//                             Debug.Log(myTag + " is colliding with " + victimTag);
//                         break;
//                     case "Thief":
//                         if (obj.GetType() == typeof(CapsuleCollider))
//                             Debug.Log(myTag + " is colliding with " + victimTag);
//                         break;
//                     case "User":
//                         if (obj.GetType() == typeof(CapsuleCollider))
//                             Debug.Log(myTag + " is colliding with " + victimTag);
//                         break;
//                     default:
//                         break;
//                 }
//                 break;
//             default:
//                 break;
//         }
//     }

//     private void OnTriggerEnter(Collider obj)
//     {
//         string myTag = gameObject.tag;
//         string victimTag = obj.gameObject.tag;
//         switch (victimTag)
//         {
//             //Who am I colliding with
//             case "Pedestrian":
//                 if (obj.GetType() == typeof(CapsuleCollider))
//                 {
//                     Debug.Log(myTag + " is colliding with " + victimTag);
//                     if (!OnFlee)
//                     {
//                         ResetProperties();
//                         TargetSeek = obj.gameObject;
//                         OnSeek = true;
//                     }
//                 }
//                 break;
//             case "Police":
//                 if (obj.GetType() == typeof(CapsuleCollider))
//                 {
//                     Debug.Log(myTag + " is colliding with " + victimTag);
//                     ResetProperties();
//                     this.TargetFlee = obj.gameObject;
//                     this.OnFlee = true;
//                 }
//                 break;
//             case "User":
//                 if (obj.GetType() == typeof(CapsuleCollider))
//                 {
//                     Debug.Log(myTag + " is colliding with " + victimTag);
//                     ResetProperties();
//                     this.TargetFlee = obj.gameObject;
//                     this.OnFlee = true;
//                 }
//                 break;
//             default:
//                 break;
//         }
//     }

//     private void OnTriggerExit(Collider obj)
//     {
//         string myTag = gameObject.tag;
//         string victimTag = obj.gameObject.tag;
//         switch (victimTag)
//         {
//             //Who is no longer colliding with me
//             case "Pedestrian":
//                 if (obj.GetType() == typeof(CapsuleCollider))
//                 {
//                     if (!OnSeek || TargetSeek == null)
//                     {
//                         Debug.Log(myTag + " is no longer colliding with " + victimTag);

//                         ResetSeek();
//                         this.OnWander = true;
//                     }
//                 }
//                 break;
//             case "Police":
//                 if (obj.GetType() == typeof(CapsuleCollider))
//                 {
//                     Debug.Log(myTag + " is no longer colliding with " + victimTag);
//                     ResetFlee();
//                     ResetSeek();
//                     this.OnWander = true;
//                 }
//                 break;
//             case "User":
//                 if (obj.GetType() == typeof(CapsuleCollider))
//                 {
//                     Debug.Log(myTag + " is no longer colliding with " + victimTag);
//                     ResetFlee();
//                     ResetSeek();
//                     this.OnWander = true;
//                 }
//                 break;
//             default:
//                 break;
//         }
//     }
// }
