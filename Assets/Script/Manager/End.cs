using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class End : MonoBehaviour {

    Text text;
    Vector3 posStart;
    Transform ai;

    Placeur placeur;

    float distanceTillEnd;
    public float distanceToEnd;

    // Use this for initialization
    void Start ()
    {
        placeur = GameObject.Find("Placeur").GetComponent<Placeur>();
        text = GetComponent<Text>();
        ai = GameObject.Find("Player").transform;
        posStart = ai.position;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        distanceTillEnd = distanceToEnd - (Vector3.Distance(posStart, ai.position));

        text.text = "Distance Left : " + Mathf.Max(Mathf.RoundToInt(distanceTillEnd), 0) + " meters";

        if (distanceTillEnd <= 0)
        {
            placeur.End();
            Destroy(this);
        }
	}
}
