using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : MonoBehaviour
{
    public GameObject diamond;
    public Vector2 boundaries;
    public int number;

    void Awake()
    {
        for(int i = 0; i < number; i++)
        {
            GameObject newDiamond = Instantiate(diamond);
            float x = Random.Range(-boundaries.x, boundaries.x);
            float z = Random.Range(-boundaries.y, boundaries.y);
            newDiamond.transform.position = new Vector3(x, diamond.transform.position.y, z);
        }


        foreach (GameObject dot in GameObject.FindGameObjectsWithTag("Point"))
        {
            Point point = dot.GetComponent<Point>();
            point.position = dot.transform.position;
            GlobalMovement.pathPoints.Add(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
