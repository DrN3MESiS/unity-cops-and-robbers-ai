using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlSB : MonoBehaviour {

    public Vector2 boundaries;
    public GameObject[] initsThief;
    public GameObject[] initsAssassin;
    public GameObject[] initsPolice;
    public GameObject[] initsPedestrian;
    public GameObject thief;
    public GameObject assassin;
    public GameObject police;
    public GameObject pedestrian;
    public int thiefNumber = 1;
    public int assassinNumber = 1;
    public int policeNumber = 1;
    public int pedestrianNumber = 1;
    // Use this for initialization
    void Awake () {
        /*float spos = 10.0f;
        for (int i = 0; i < 3; i = i + 1)
            for (int j = 0; j < 3; j = j + 1)
            {
                GameObject newObj= Instantiate(myObject, new Vector3(i * spos, 0.5f, j * spos), Quaternion.identity);
                moveVelSimple mymove = newObj.GetComponent<moveVelSimple>();
                mymove.TargetSeek = targetSeek;
                mymove.OnSeek = true;
            }*/

        foreach(GameObject initThief in initsThief)
        {
            Vector2 init = new Vector2(initThief.transform.position.x, initThief.transform.position.z);
            for (int i = 0; i < thiefNumber; i++)
            {
                GameObject newObj = Instantiate(thief, new Vector3(init.x + Random.Range(-boundaries.x, boundaries.x), thief.transform.position.y, init.y + Random.Range(-boundaries.y, boundaries.y)), Quaternion.identity);
                newObj.tag = "Thief";
            }
        }

        foreach (GameObject initgb in initsAssassin)
        {

            Vector2 init = new Vector2(initgb.transform.position.x, initgb.transform.position.z);
            for (int i = 0; i < assassinNumber; i++)
            {
                GameObject newObj = Instantiate(assassin, new Vector3(init.x + Random.Range(-boundaries.x, boundaries.x), thief.transform.position.y, init.y + Random.Range(-boundaries.y, boundaries.y)), Quaternion.identity);
                newObj.tag = "Assassin";
            }
        }

        foreach (GameObject initgb in initsPolice)
        {
            Vector2 init = new Vector2(initgb.transform.position.x, initgb.transform.position.z);
            for (int i = 0; i < policeNumber; i++)
            {
                GameObject newObj = Instantiate(police, new Vector3(init.x + Random.Range(-boundaries.x, boundaries.x), thief.transform.position.y, init.y + Random.Range(-boundaries.y, boundaries.y)), Quaternion.identity);
                newObj.tag = "Police";
            }

        }

        foreach (GameObject initgb in initsPedestrian)
        {
            Vector2 init = new Vector2(initgb.transform.position.x, initgb.transform.position.z);
            for (int i = 0; i < pedestrianNumber; i++)
            {
                GameObject newObj = Instantiate(pedestrian, new Vector3(init.x + Random.Range(-boundaries.x, boundaries.x), thief.transform.position.y, init.y + Random.Range(-boundaries.y, boundaries.y)), Quaternion.identity);
                newObj.tag = "Pedestrian";
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
