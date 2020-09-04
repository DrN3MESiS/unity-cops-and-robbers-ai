using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class w2 : MonoBehaviour
{
    public Transform target;
    Rigidbody rig;
    public float distance = 5f;
    public float sAngle = 0f;
    public float fAngle = 360f;

    // Start is called before the first frame update
    void Start()
    {  
        wander();
    }

    // Update is called once per frame
    void Update()
    {
        wander();
    }

     private void wander() {
            rig = GetComponent<Rigidbody>();
            rig.MovePosition(target.transform.position);
            transform.rotation = Quaternion.Euler(0, Random.Range(sAngle, fAngle), 0);
            transform.Translate(Vector3.forward * distance);
    }
}
