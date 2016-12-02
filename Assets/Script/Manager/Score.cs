using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    AI ai;
    Text text;

    void Start()
    {
        ai = GameObject.Find("Player").GetComponent<AI>();
        text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        text.text = "Score : " + ai.getReward();
    }

}
