using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlSB : MonoBehaviour {
    public GameObject myObject;

    public GameObject targetSeek;
    public GameObject targetFlee;
    // Use this for initialization
    void Start () {
        float spos = 10.0f;
        for (int i = 0; i < 3; i = i + 1)
            for (int j = 0; j < 3; j = j + 1)
            {
                GameObject newObj= Instantiate(myObject, new Vector3(i * spos, 0.5f, j * spos), Quaternion.identity);
                moveVelSimple mymove = newObj.GetComponent<moveVelSimple>();
                mymove.TargetSeek = targetSeek;
                mymove.OnSeek = true;
            }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
