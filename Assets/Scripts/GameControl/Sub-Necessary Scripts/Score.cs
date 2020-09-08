using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private List<GlobalMovement> thiefs = new List<GlobalMovement>();
    public int score = 0;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score = 0;        
        foreach (GameObject thief in GameObject.FindGameObjectsWithTag("Thief"))
        {
            score -= thief.GetComponent<GlobalMovement>().jewls;
        }        
        text.text = score.ToString();
    }
}
