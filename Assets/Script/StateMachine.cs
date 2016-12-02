using UnityEngine;

public class StateMachine : States
{
    States p_myCurrentState;

    bool p_myFirstTime;

    public StateMachine(GameObject entity, States currentState) : base(entity)
    {
        setCurrentState(currentState);
    }

    public void setCurrentState(States currentState)
    {
        p_myFirstTime = false;
        p_myCurrentState = currentState;
    }

    public override void doOnce()
    {
    }

    public override void toDo()
    {
        if(!p_myFirstTime)
        {
            p_myFirstTime = true;
            p_myCurrentState.doOnce();
        }
        else
        {
            p_myCurrentState.toDo();
        }
        CheckTransitions();
    }

    void CheckTransitions()
    {
        foreach (Transitions t in p_myCurrentState.getTransitions())
        {
            t.CheckConditions();
        }
    }
}