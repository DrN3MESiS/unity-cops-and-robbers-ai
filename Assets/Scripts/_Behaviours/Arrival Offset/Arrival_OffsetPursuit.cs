using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using System;

public class moveVelSimple : MonoBehaviour
{

    // ** ARGUMENTS **
    // ***************
    public Vector3 vc_Velocity;
    public Vector3 vc_Heading;
    Vector3 vn_Velocity;
    public float s_rotSpeed = 20.0f;
    public float s_MaxSpeed = 10.0f;
    public float s_MinSpeed = 1.0f;
    public float offset = 2.0f; // This is used only for Offset Pursuit
    private Vector3 newPosition;
    public float s_panicDist;

    // Targets
    public GameObject TargetSeek;
    public GameObject TargetFlee;
    public GameObject TargetPursuit;
    public GameObject OffsetTargetPursuit;

    // Behaviors
    public bool OnSeek = false;
    public bool OnFlee = false;
    public bool OnOffsetPursuit = false;
    public bool OnArrival = false;

    // ** START **
    // ***********
    void Start()
    {
        s_MinSpeed = 0.2f;
        s_panicDist = 30.0f;
        vn_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        vc_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // ** UPDATE **
    // ************
    void Update()
    {

        vn_Velocity = Vector3.zero;

        // ON SEEK
        if (OnSeek)
        {
            if (Vector3.Distance(TargetSeek.transform.position, transform.position) > 1.0)
            {
                vn_Velocity = vn_Velocity + Seek(TargetSeek.transform.position);
            }

            else vn_Velocity = vc_Velocity * -1.0f;
        }

        // ON FLEE
        if (OnFlee)
        {
            vn_Velocity = vn_Velocity + Flee(TargetFlee.transform.position);
        }

        // ON OFFSET PURSUIT
        if (OnOffsetPursuit)
        {
            vn_Velocity = vn_Velocity + OffsetPursuit(OffsetTargetPursuit, offset);
        }

        // ON ARRIVAL
        if (OnArrival)
        {
            //Vector3 velocity = Arrival(TargetSeek);
            //transform.position -= velocity;
            if (Vector3.Distance(TargetSeek.transform.position, transform.position) > 1.0)
            {
                vn_Velocity = vn_Velocity + Arrival(TargetSeek.transform.position);
            }

            else vn_Velocity = vc_Velocity * -1.0f;

        }

        vc_Velocity += vn_Velocity;
        vc_Velocity = Vector3.ClampMagnitude(vc_Velocity, s_MaxSpeed);
        newPosition = transform.position + (vc_Velocity * Time.deltaTime);
        if (vc_Velocity.magnitude > s_MinSpeed) transform.position = newPosition;

        vc_Heading = vc_Velocity.normalized;

        float angle = Vector3.SignedAngle(transform.forward, vc_Heading, Vector3.up);
        float rotAngle = 0.0f;

        if (angle < -0.1f) rotAngle = Time.deltaTime * -1.0f * s_rotSpeed;
        if (angle > 0.1f) rotAngle = Time.deltaTime * s_rotSpeed;
        if (angle >= -0.1f && angle <= 0.1f)
        {
            if (Vector3.Dot(transform.forward, vc_Heading) >= 0.9) rotAngle = 0.0f;
            if (Vector3.Dot(transform.forward, vc_Heading) <= -0.9) rotAngle = 10.0f;
        }

        transform.Rotate(0.0f, rotAngle, 0.0f, Space.Self);
    }

    // ** FUNCTIONS **
    // ***************

    // ** SEEK ** // 
    public Vector3 Seek(Vector3 targetSeek)
    {
        Vector3 direction, desiredVelocity;

        direction = targetSeek - transform.position;
        direction.y = 0; // 2D Only

        if (direction.magnitude < 1.0f) return (Vector3.zero);

        direction.Normalize();
        desiredVelocity = direction * s_MaxSpeed;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, s_MaxSpeed);

        return (desiredVelocity - vc_Velocity);

    }

    // ** FLEE ** // 
    public Vector3 Flee(Vector3 targetFlee)
    {
        Vector3 direction, desiredVelocity;

        direction = transform.position - targetFlee;
        direction.y = 0; // 2D Only

        if (direction.magnitude > s_panicDist) return (Vector3.zero);

        direction.Normalize();
        desiredVelocity = direction * s_MaxSpeed;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, s_MaxSpeed);

        return (desiredVelocity - vc_Velocity);

    }

    // ** OFFSET PURSUIT ** // 
    public Vector3 OffsetPursuit(GameObject targetOffsetPursuit, float offset)
    {
        moveVelSimple objecto = targetOffsetPursuit.GetComponent<moveVelSimple>();
        Vector3 targetFuture = transform.position + objecto.vc_Heading * offset;

        float distance = Vector3.Distance(transform.position, targetOffsetPursuit.transform.position);
        print(distance);
        if (distance > offset) return Seek(OffsetTargetPursuit.transform.position);
        if(distance < offset) targetFuture = transform.position + objecto.vc_Heading * offset * 0.75f; //Slow down


        return Seek(targetFuture);
    }

    // ** ARRIVAL ** // 
    public Vector3 Arrival(Vector3 targetSeek)
    {
        Vector3 direction, desiredVelocity;
        float distance, radius = 10f;

        direction = targetSeek - transform.position;
        //direction.y = 0; // 2D Only
        distance = direction.magnitude;

        if (distance < 1.0f) return (Vector3.zero);

        // Seek
        direction.Normalize();
        desiredVelocity = direction * s_MaxSpeed;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, s_MaxSpeed);

        // Arrival
        if (distance <= radius) desiredVelocity *= (distance / radius);

        return (desiredVelocity - vc_Velocity);

    }

    // ** DRAW ARROWS **
    // *****************
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, vc_Heading * 10.0f + transform.position, Color.red);
        Debug.DrawLine(transform.position, transform.forward * 10.0f + transform.position, Color.green);

    }
}
