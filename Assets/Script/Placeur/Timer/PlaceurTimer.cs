using UnityEngine;
using System.Collections;

public class PlaceurTimer : MonoBehaviour {

    [HideInInspector]
    public Placeur placeur;
    float currentTime;
    public float CurrentTime
    {
        get { return currentTime; }
    }
    bool isFlowing = true;
    public bool IsFlowing
    {
        get { return isFlowing; }
        set { isFlowing = value; }
    }
    public float nextPositioningTime;

    void Start ()
    {
        placeur = GetComponent<Placeur>();
	}
	
	void Update ()
    {
	    if (isFlowing)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= nextPositioningTime)
            {
                currentTime = 0;
                placeur.PlaceItem = true;
                IsFlowing = false;
            }
	    }
	}
}
