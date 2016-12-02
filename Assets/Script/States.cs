using UnityEngine;
using System.Collections.Generic;

public abstract class States
{
    protected GameObject _myEntity;
    protected AI _myAi;

    List<Transitions> p_myTransitions;

    public States(GameObject entity)
    {
        _myEntity = entity;
        _myAi = _myEntity.GetComponent<AI>();
    }

    public void setTransitions(List<Transitions> transitions)
    {
        p_myTransitions = transitions;
    }
    public List<Transitions> getTransitions()
    {
        return p_myTransitions;
    }

    public abstract void doOnce();
    public abstract void toDo();
}