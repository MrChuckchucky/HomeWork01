using UnityEngine;
using System.Collections.Generic;

public class Transitions
{
    States p_myNextState;

    StateMachine p_myStateMachine;

    List<Conditions> p_myConditions;

    public Transitions(States nextState, StateMachine stateMachine, List<Conditions> conditions)
    {
        p_myNextState = nextState;
        p_myStateMachine = stateMachine;
        p_myConditions = conditions;
    }

    public bool CheckConditions()
    {
        foreach (Conditions c in p_myConditions)
        {
            if (!c.Check())
            {
                return false;
            }
        }
        ChangeState();
        return true;
    }

    public void ChangeState()
    {
        p_myStateMachine.setCurrentState(p_myNextState);
    }
}