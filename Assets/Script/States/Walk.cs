using UnityEngine;

public class Walk : States
{
    float p_myWalkSpeed;
    float p_myDistanceErrorWalk;

    Vector3 p_myOrigin;
    Vector3 p_myDestination;

    public Walk(GameObject entity) : base(entity)
    {
    }

    void Initialize()
    {
        p_myWalkSpeed = _myAi.getWalkSpeed();
        p_myDistanceErrorWalk = _myAi.getDistanceErrorWalk();
    }

    public override void doOnce()
    {
        _myAi.setIsDoingSomething(true);
        p_myOrigin = _myEntity.transform.position;
        p_myDestination = p_myOrigin + _myEntity.transform.forward * 1.0f;
    }

    public override void toDo()
    {
        _myEntity.transform.position += Vector3.Lerp(p_myOrigin, p_myDestination, p_myWalkSpeed);
        float distance = Vector3.Distance(_myEntity.transform.position, p_myDestination);
        if (distance <= p_myDistanceErrorWalk)
        {
            _myAi.setIsDoingSomething(false);
        }
    }
}