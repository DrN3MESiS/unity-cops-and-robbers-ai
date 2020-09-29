using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : GlobalMovement
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision obj)
    {
        string myTag = gameObject.tag;
        string victimTag = obj.gameObject.tag;

        switch (myTag)
        {
            case "Assassin":
                switch (victimTag)
                {
                    //Who am I colliding with
                    case "Pedestrian":
                        Destroy(obj.gameObject);
                        ResetSeek();
                        OnWander = true;
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                        {
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        }
                        break;
                    case "Assassin":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    case "Thief":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    case "User":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        string myTag = gameObject.tag;
        string victimTag = obj.gameObject.tag;
        switch (victimTag)
        {
            //Who am I colliding with
            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log(myTag + " is colliding with " + victimTag);
                    if (!OnFlee)
                    {
                        ResetProperties();
                        TargetSeek = obj.gameObject;
                        OnSeek = true;
                    }
                }
                break;
            case "Police":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log(myTag + " is colliding with " + victimTag);
                    ResetProperties();
                    this.TargetFlee = obj.gameObject;
                    this.OnFlee = true;
                }
                break;
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log(myTag + " is colliding with " + victimTag);
                    ResetProperties();
                    this.TargetFlee = obj.gameObject;
                    this.OnFlee = true;
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
                    if (!OnSeek || TargetSeek == null)
                    {
                        Debug.Log(myTag + " is no longer colliding with " + victimTag);

                        ResetSeek();
                        this.OnWander = true;
                    }
                }
                break;
            case "Police":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log(myTag + " is no longer colliding with " + victimTag);
                    ResetFlee();
                    ResetSeek();
                    this.OnWander = true;
                }
                break;
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log(myTag + " is no longer colliding with " + victimTag);
                    ResetFlee();
                    ResetSeek();
                    this.OnWander = true;
                }
                break;
            default:
                break;
        }
    }
}
