using UnityEngine;
using System.Collections;
using System;

public class ActionEnded : Conditions
{
    public ActionEnded(GameObject entity) : base(entity)
    {
    }

    public override bool Check()
    {
        return !_myAI.getIsDoingSomething();
    }
}
