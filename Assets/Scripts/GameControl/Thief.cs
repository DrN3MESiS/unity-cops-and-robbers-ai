using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    /* Public Properties*/
    public float s_rotSpeed = 1.5f;
    public float s_MaxSpeed = 16.0f;
    public float s_MinSpeed = 1.0f;

    /* Triggers */
    public bool OnFlee = false;
    public bool OnEvade = false;
    public bool OnPathFollow = false;

    /* Trigger Targets */
    public GameObject TargetFlee;
    public GameObject TargetEvade;
    public Point TargetPathFollow;

    public float s_panicDist;

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
    private Vector3 newPosition;

    //PathFollow
    public static List<Point> pathPoints = new List<Point>();
    //Wander
    public bool isGamePath = true;
    public float speed = 1.0f;

    private float defaultSpeed = 1.0f;

    //Thief
    public int jewls = 0;

    /* Functions */
    bool debug = false;
    // Start is called before the first frame update
    void Start()
    {
        EntityRB = GetComponent<Rigidbody>();
        s_MinSpeed = 0.2f;
        vc_Velocity = new Vector3(0.0f, 0.0f, 0.0f);


        myCollider = gameObject.AddComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = radius;
        s_panicDist = radius;

        gameObject.tag = "Thief";
        defaultSpeed = 1.3f;
        OnPathFollow = true;
        isGamePath = true;
        StartPathFollow();

        speed = defaultSpeed;
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

    // Update is called once per frame
    void Update()
    {
        if (OnEvade)
        {
            if (TargetEvade != null)
            {
                vel_Evade = Evade(TargetEvade);
            }
        }

        if (OnFlee)
        {

            if (TargetFlee != null)
            {
                vel_Flee = Flee(TargetFlee.transform.position);
            }
            else
            {
                Debug.Log("No hay un Target seleccionado para hacer Flee");
            }
        }

        if (OnPathFollow)
        {
            if (TargetPathFollow != null)
            {
                if (Vector3.Distance(TargetPathFollow.position, transform.position) > 1.0)
                    vel_Seek = Path();
                //else
                //vel_Seek = vc_Velocity * -1.0f;
                if (TargetPathFollow.withinRadius(transform.position))
                {
                    jewls++;
                    StartPathFollow();
                }
            }
        }

        vc_Velocity = Vector3.zero;
        vc_Velocity += vel_Seek + vel_Arrive + vel_Evade + vel_Flee + vel_OffsetPursuit + vel_Pursuit + vel_Wander;
        if (debug)
        {
            Debug.Log("vc_Velocity(" + vc_Velocity + ") = " + "vel_Seek(" + vel_Seek + ") + " + "vel_Arrive(" + vel_Arrive + ") + " + "vel_Evade(" + vel_Evade + ") + " + "vel_Flee(" + vel_Flee + ") + " + "vel_OffsetPursuit(" + vel_OffsetPursuit + ") + " + "vel_Pursuit(" + vel_Pursuit + ") + " + "vel_Wander(" + vel_Wander + ")");
        }

        vc_Velocity = Vector3.ClampMagnitude(vc_Velocity, s_MaxSpeed);

        newPosition = transform.position + (vc_Velocity * speed * Time.deltaTime);
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
    public Vector3 Path()
    {
        if (TargetPathFollow.withinRadius(transform.position))
        {
            StartPathFollow();
        }

        Vector3 targetSeek = TargetPathFollow.position;
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

        //return (DesiredVelocity - vc_Velocity);

        return (DesiredVelocity);
    }

    //******************************************************************
    public Vector3 Flee(Vector3 targetFlee)
    {
        Vector3 direction;

        direction = transform.position - targetFlee;
        direction.y = 0;


        if (direction.magnitude > s_panicDist)
        {
            return (Vector3.zero);
        }
        direction.Normalize();
        Vector3 DesiredVelocity = direction * s_MaxSpeed;
        DesiredVelocity = Vector3.ClampMagnitude(DesiredVelocity, s_MaxSpeed);

        return (DesiredVelocity - vc_Velocity);
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

    //*************************************************************************
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, vc_Heading * 10.0f + transform.position, Color.red);
        Debug.DrawLine(transform.position, transform.forward * 10.0f + transform.position, Color.green);
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.blue;
    }

    public void ResetProperties()
    {
        this.TargetFlee = null;
        this.OnFlee = false;
        vel_Flee = Vector3.zero;

        this.TargetEvade = null;
        this.OnEvade = false;
        vel_Evade = Vector3.zero;

        this.OnPathFollow = false;
    }

    public void ResetFlee()
    {
        this.TargetFlee = null;
        this.OnFlee = false;
        vel_Flee = Vector3.zero;
    }

    public void ResetEvade()
    {
        this.TargetEvade = null;
        this.OnEvade = false;
        vel_Evade = Vector3.zero;
    }

    public void ResetPathFollow()
    {
        this.TargetPathFollow = null;
        this.OnPathFollow = false;
    }

    public void StartPathFollow()
    {
        if (pathPoints.Count > 0)
        {
            this.TargetPathFollow = pathPoints[0];
            pathPoints.RemoveAt(0);
        }
        else
        {
            this.TargetPathFollow = null;
            ResetProperties();
        }
    }
}
