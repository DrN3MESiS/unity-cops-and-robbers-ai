using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector3 position;
    public float radius = 0.1f;

    public Point(Vector3 position)
    {
        this.position = position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public bool withinRadius(Vector3 player)
    {
        if (Vector3.Distance(player, position) <= radius)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
