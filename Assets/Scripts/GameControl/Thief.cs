using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : GlobalMovement
{
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
                    this.TargetEvade = obj.gameObject;
                    this.OnEvade = true;
                }
                break;
            case "Police":
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is colliding with " + victimTag);
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
        string victimTag = obj.gameObject.tag;

        switch (victimTag)
        {
            //Who is no longer colliding with me
            case "Pedestrian":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is no longer colliding with " + victimTag);
                    ResetEvade();
                }
                break;
            case "Police":
            case "User":
                if (obj.GetType() == typeof(CapsuleCollider))
                {
                    Debug.Log("Thief is no longer colliding with " + victimTag);
                    ResetFlee();
                    //StartPathFollow();
                    this.OnPathFollow = true;
                }
                break;
            default:
                break;
        }             
    }
}
