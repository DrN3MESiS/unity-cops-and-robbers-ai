using System.Collections.Generic;
using UnityEngine;

public class GlobalMovement : MonoBehaviour {
    /* Public Properties*/
    public float s_rotSpeed = 1.5f;
    public float s_MaxSpeed = 8.0f;
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
    private Vector3 vel_Wander = Vector3.zero;
    private Vector3 vel_Seek = Vector3.zero;
    private Vector3 vel_Flee = Vector3.zero;
    private Vector3 vel_Arrive = Vector3.zero;
    private Vector3 vel_Pursuit = Vector3.zero;
    private Vector3 vel_OffsetPursuit = Vector3.zero;
    private Vector3 vel_Evade = Vector3.zero;

    /*Global Radius*/
    private SphereCollider myCollider;
    public float radius = 6.0f;


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
    //Wander
    public bool isGamePath = true;
    public float speed = 1.0f;
    private Point curr;

    /***/
    float elapsed = 0f;


    /* Functions */
    void Start () {
        EntityRB = GetComponent<Rigidbody>();
        s_MinSpeed = 0.2f;
        vn_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        vc_Velocity = new Vector3(0.0f, 0.0f, 0.0f);
        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = radius;
        

        switch (gameObject.tag)
        {
            case "Assassin":
                OnWander = true;
                break;
            case "Pedestrian":
                //OnWander = true;
                break;
            case "Police":
                //OnWander = true;
                break;
            case "Thief":
                break;
            case "User":
                break;
        }
    }

    private void OnTriggerEnter(Collider obj)
    {
        string myTag = gameObject.tag;
        string victimTag = obj.gameObject.tag;
        //What am I
        switch (myTag)
        {
            case "Pedestrian":
                //Who am I colliding with
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
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
            case "Police":
                //Who am I colliding with
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
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
            case "Assassin":
                //Who am I colliding with
                switch (victimTag)
                {
                    case "Pedestrian":
                        if(obj.GetType() == typeof(CapsuleCollider))
                        {
                            Debug.Log(myTag + " is colliding with " + victimTag);
                            this.OnWander = false;
                            this.TargetWander = null;
                            this.TargetSeek = null;
                            this.TargetSeek = obj.gameObject;
                            this.OnSeek = true;
                        }
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                        {
                            Debug.Log(myTag + " is colliding with " + victimTag);
                            this.OnWander = false;
                            this.TargetWander = null;
                            this.TargetSeek = null;
                            this.OnSeek = false;

                            this.TargetFlee = obj.gameObject;
                            this.OnFlee = true;
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
            case "Thief":
                //Who am I colliding with
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
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
            case "User":
                //Who am I colliding with
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is colliding with " + victimTag);
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
    private void OnTriggerExit(Collider obj)
    {
        string myTag = gameObject.tag;
        string victimTag = obj.gameObject.tag;
        //What am I
        switch (myTag)
        {
            case "Pedestrian":
                //Who is no longer colliding with me
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Assassin":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Thief":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "User":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    default:
                        break;
                }
                break;
            case "Police":
                //Who is no longer colliding with me
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Assassin":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Thief":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "User":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    default:
                        break;
                }
                break;
            case "Assassin":
                //Who is no longer colliding with me
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                        {
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                            this.OnSeek = false;
                            this.TargetSeek = null; //Necessary so trigger creates it's own WanderObj
                            this.OnWander = true;
                        }
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                        {
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                            this.TargetFlee = null;
                            this.OnFlee = false;

                            this.TargetWander = null;
                            this.TargetSeek = null;
                            this.OnSeek = false;
                            this.OnWander = true;
                        }
                        break;
                    case "Assassin":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Thief":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "User":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    default:
                        break;
                }
                break;
            case "Thief":
                //Who is no longer colliding with me
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Assassin":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Thief":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "User":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    default:
                        break;
                }
                break;
            case "User":
                //Who is no longer colliding with me
                switch (victimTag)
                {
                    case "Pedestrian":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Police":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Assassin":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "Thief":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    case "User":
                        if (obj.GetType() == typeof(CapsuleCollider))
                            Debug.Log(myTag + " is no longer colliding with " + victimTag);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
    }

    void Update () {
        vn_Velocity = Vector3.zero;

        if (OnSeek)
        {
            if (TargetSeek != null)
            {
                if (Vector3.Distance(TargetSeek.transform.position, transform.position) > 1.0)
                    vel_Seek = Seek(TargetSeek.transform.position);
                else
                    vel_Seek = vc_Velocity * -1.0f;
                //CustomSeek(TargetSeek.transform.position);
            } else
            {
                Debug.Log("No hay un Target seleccionado para hacer Seek");
            }
        }

        if (OnFlee)
        {
            if (TargetFlee != null)
                vel_Flee = Flee(TargetFlee.transform.position);
             else
                Debug.Log("No hay un Target seleccionado para hacer Flee");
        }

        if(OnPursuit)
        {
            if(TargetPursuit != null)
                vel_Pursuit = Pursuit(TargetPursuit);
            else
                Debug.Log("No hay un Target seleccionado para hacer Pursuit");
        }

        if (OnWander)
        {
            speed = 10.0f;
            if(TargetWander != null)
            {
                elapsed += Time.deltaTime;
                if (elapsed >= .5f)
                {
                    elapsed = elapsed % .5f;
                    Wander(TargetWander);
                }
               

                this.TargetSeek = TargetWander;
                this.OnSeek = true;
            }
             else
            {
                GameObject temp = new GameObject();
                TargetWander = Instantiate(temp, transform.position, Quaternion.identity);
                Destroy(temp);
                speed = 1.0f;
            }

        } else
        {
            if(TargetWander != null)
            {
                vel_Seek = Vector3.zero;
                Destroy(TargetWander);
                TargetWander = null;
                this.TargetSeek = null;
                this.OnSeek = false;
            }
        }

        if (OnOffsetPursuit)
        {
            if (TargetOffsetPursuit != null)
                vel_OffsetPursuit = OffsetPursuit(TargetOffsetPursuit, offset);
            else
                Debug.Log("No hay un Target seleccionado para hacer OffsetPursuit");
        }

        if (OnArrival)
        {
            if(TargetArrival != null)
            {
                if (Vector3.Distance(TargetArrival.transform.position, transform.position) > 1.0)
                {
                    vel_Arrive = Arrival(TargetArrival.transform.position);
                }

                else vel_Arrive = vc_Velocity * -1.0f;
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

        vc_Velocity = Vector3.zero;
        vc_Velocity += vel_Seek + vel_Arrive + vel_Evade + vel_Flee + vel_OffsetPursuit + vel_Pursuit + vel_Wander;
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

        direction = targetSeek - transform.position;
        direction.y = 0;

        if (direction.magnitude < 1.0f)
        {

            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);
    }
    public void CustomSeek(Vector3 targetSeek)
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, targetSeek, speed * Time.fixedDeltaTime);
        EntityRB.MovePosition(pos);
        transform.LookAt(targetSeek);
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
        target.transform.position = transform.position;
        target.transform.rotation = Quaternion.Euler(0, Random.Range(sAngle, fAngle), 0);
        target.transform.Translate(Vector3.forward * distance);
    }

    //******************************************************************

    public void CustomOffsetPursuit(GameObject targetOffsetPursuit, float offset)
    {
        moveVelSimple objecto = targetOffsetPursuit.GetComponent<moveVelSimple>();
        Vector3 targetFuture = transform.position + objecto.vc_Heading * offset;


        CustomSeek(targetFuture);
    }
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
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.blue;
    }
}
