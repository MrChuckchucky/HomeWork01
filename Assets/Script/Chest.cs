using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    string p_myName;
    int p_myVictory;
    int p_myDefeat;

    public Chest(string name)
    {
        p_myName = name;
        p_myVictory = 0;
        p_myDefeat = 0;
    }

    public void addVictory()
    {
        p_myVictory++;
    }

    public void addDefeat()
    {
        p_myDefeat++;
    }

    public string getName()
    {
        return p_myName;
    }

    public float getChance()
    {
        return p_myVictory / (float)(p_myVictory + p_myDefeat);
    }
}