using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlSB : MonoBehaviour {
    public GameObject myObject;

    public GameObject targetSeek;
    public GameObject targetFlee;

    public Vector2 boundaries;
    public Vector2[] initsThief;
    public GameObject thief;
    public int thiefNumber = 10;
    // Use this for initialization
    void Start () {
        /*float spos = 10.0f;
        for (int i = 0; i < 3; i = i + 1)
            for (int j = 0; j < 3; j = j + 1)
            {
                GameObject newObj= Instantiate(myObject, new Vector3(i * spos, 0.5f, j * spos), Quaternion.identity);
                moveVelSimple mymove = newObj.GetComponent<moveVelSimple>();
                mymove.TargetSeek = targetSeek;
                mymove.OnSeek = true;
            }*/

        foreach(Vector2 initThief in initsThief)
        {
            for (int i = 0; i < thiefNumber; i++)
            {
                GameObject newObj = Instantiate(thief, new Vector3(initThief.x + Random.Range(-boundaries.x, boundaries.x), thief.transform.position.y, initThief.y + Random.Range(-boundaries.y, boundaries.y)), Quaternion.identity);
                newObj.tag = "Thief";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
