using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMovement : MonoBehaviour {
    /* Public Properties*/
    public float s_rotSpeed=5.0f;
    public float s_MaxSpeed=3.0f;
    public float s_MinSpeed = 1.0f;

    /* Triggers */
    public bool OnSeek = false;
    public bool OnFlee = false;
    public bool OnEvade = false;
    public bool OnArrival = false;
    public bool OnPathFollow = false;
    public bool OnPursuit = false;
    public bool OnWander = false;
    public bool OnOffsetPursuit = false;

    /* Trigger Targets */
    public GameObject TargetSeek;
    public GameObject TargetFlee;
    public GameObject TargetPursuit;
    public GameObject TargetEvade;
    public List<GameObject> TargetPathFollow;
    public GameObject TargetArrival;
    public GameObject TargetWander;
    public GameObject TargetOffsetPursuit;


    public float s_panicDist;
    public float pursuitDist;

    public Vector3 vc_Velocity;
    public Vector3 vc_Heading;

    /** Entity Properties */
    Rigidbody EntityRB;
    private Vector3 vn_Velocity;
    private Vector3 newPosition;

    /*Behaviours Properties*/
    //Wander
    float distance = 5f;
     float sAngle = 0f;
     float fAngle = 360f;
    //Offset Pursuit
    float offset = 2.0f;
    //PathFollow
    public List<Point> pathPoints;
    public List<Vector3> pathVectors = new List<Vector3>();
    
    public bool isGamePath = true;
    public float speed = 1.0f;
    private Point curr;


    /* Functions */
    void Start () {
        EntityRB = GetComponent<Rigidbody>();
        s_MinSpeed = 0.2f;
        vn_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        vc_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }
	
	void Update () {
        vn_Velocity = Vector3.zero;

        if (OnSeek)
        {
            if (TargetSeek != null)
            {
                if (Vector3.Distance(TargetSeek.transform.position, transform.position) > 1.0)
                    vn_Velocity = vn_Velocity + Seek(TargetSeek.transform.position);
                else
                    vn_Velocity = vc_Velocity * -1.0f;
            } else
            {
                Debug.Log("No hay un Target seleccionado para hacer Seek");
            }
        }

        if (OnFlee)
        {
            if (TargetFlee != null)
                vn_Velocity = vn_Velocity + Flee(TargetFlee.transform.position);
             else
                Debug.Log("No hay un Target seleccionado para hacer Flee");
        }

        if(OnPursuit)
        {
            if(TargetPursuit != null)
                EntityRB.velocity = Pursuit(TargetPursuit);
            else
                Debug.Log("No hay un Target seleccionado para hacer Pursuit");

        }

        if (OnWander)
        {
            if(TargetWander != null)
                Wander(TargetWander);
             else
                Debug.Log("No hay un Target seleccionado para hacer Wander");
        }

        if (OnOffsetPursuit)
        {
            if (TargetOffsetPursuit != null)
                vn_Velocity = vn_Velocity + OffsetPursuit(TargetOffsetPursuit, offset);
            else
                Debug.Log("No hay un Target seleccionado para hacer OffsetPursuit");
        }

        if (OnArrival)
        {
            if(TargetArrival != null)
            {
                if (Vector3.Distance(TargetArrival.transform.position, transform.position) > 1.0)
                {
                    vn_Velocity = vn_Velocity + Arrival(TargetArrival.transform.position);
                }

                else vn_Velocity = vc_Velocity * -1.0f;
            } else
            {
                Debug.Log("No hay un Target seleccionado para hacer Arrival");

            }
        }

        if (OnPathFollow)
        {
            if (isGamePath)
            {
                foreach (GameObject dot in GameObject.FindGameObjectsWithTag("Point"))
                {
                    Point point = dot.GetComponent<Point>();
                    point.position = dot.transform.position;
                    pathPoints.Add(point);
                }
            }
            else
            {
                foreach (Vector3 dot in pathVectors)
                {
                    Point point = new Point(dot);
                    pathPoints.Add(point);
                }
            }
            if (pathPoints.Count > 0)
            {
                curr = pathPoints[0];
                pathPoints.RemoveAt(0);
            }
            else
            {
                OnPathFollow = false;
            }
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
        
    //******************************************************************
    public Vector3 Seek(Vector3 targetSeek)
    {
        Vector3 direction;
       
       direction =  targetSeek-transform.position;
        direction.y = 0;

        if (direction.magnitude < 1.0f )
        {
            
            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);


    }

    //******************************************************************
    public Vector3 Flee(Vector3 targetFlee)
    {
        Vector3 direction;

        direction =  transform.position-targetFlee;
        direction.y = 0;

        if (direction.magnitude>s_panicDist)
        {
           
            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);


    }

    //******************************************************************
    public Vector3 Pursuit(GameObject target)
    {
        Vector3 direction;

        
 
        Vector3 FP = target.transform.position + target.GetComponent<Rigidbody>().velocity * 100f * Time.deltaTime;
        direction = FP - transform.position;
        direction.y = 0;
        if (direction.magnitude < pursuitDist)
        {

            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed ;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return DesiredVelocity - EntityRB.velocity;

         

    }

    //******************************************************************
    public Vector3 Evade(GameObject target)
    {
        Vector3 direction;

        direction = target.transform.position + target.GetComponent<Rigidbody>().velocity * Time.deltaTime * 15f + transform.position;
        direction.y = 0;

        if (direction.magnitude < s_panicDist)
        {

            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);


    }

    //******************************************************************
    private void Wander(GameObject target)
    {
        EntityRB.MovePosition(target.transform.position);
        transform.rotation = Quaternion.Euler(0, Random.Range(sAngle, fAngle), 0);
        transform.Translate(Vector3.forward * distance);
    }

    //******************************************************************
    public Vector3 OffsetPursuit(GameObject targetOffsetPursuit, float offset)
    {
        moveVelSimple objecto = targetOffsetPursuit.GetComponent<moveVelSimple>();
        Vector3 targetFuture = transform.position + objecto.vc_Heading * offset;

        float distance = Vector3.Distance(transform.position, targetOffsetPursuit.transform.position);
        print(distance);
        if (distance > offset) return Seek(TargetOffsetPursuit.transform.position);
        if (distance < offset) targetFuture = transform.position + objecto.vc_Heading * offset * 0.75f; //Slow down


        return Seek(targetFuture);
    }

    //******************************************************************
    public Vector3 Arrival(Vector3 targetSeek)
    {
        Vector3 direction, desiredVelocity;
        float distance, radius = 10f;

        direction = targetSeek - transform.position;
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

    //******************************************************************
    public Vector3 Path()
    {
        if (curr.withinRadius(transform.position))
        {
            if (pathPoints.Count > 0)
            {
                curr = pathPoints[0];
                pathPoints.RemoveAt(0);
            }
            else
            {
                OnPathFollow = false;
            }
        }
        return Vector3.MoveTowards(transform.position, curr.position, speed * Time.deltaTime);
    }

    //*************************************************************************
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, vc_Heading * 10.0f + transform.position, Color.red);
        Debug.DrawLine(transform.position, transform.forward * 10.0f + transform.position, Color.green);
    }
}
