using UnityEngine;
using System.Collections;

public abstract class Conditions
{
    protected GameObject _myEntity;
    protected AI _myAI;

    public Conditions(GameObject entity)
    {
        _myEntity = entity;
        _myAI = entity.GetComponent<AI>();
    }

    public abstract bool Check();
}
