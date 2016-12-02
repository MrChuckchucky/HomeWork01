using UnityEngine;

public class SomethingBehind : Conditions
{
    public SomethingBehind(GameObject entity) : base(entity)
    {
    }

    public override bool Check()
    {
        RaycastHit hit;
        Vector3 pos = _myEntity.transform.position;
        Vector3 forward = _myEntity.transform.forward;
        if (Physics.Raycast(pos, forward, out hit, 1.0f))
        {
            if (hit.collider.gameObject != null)
            {
                _myAI.setItemInFront(hit.collider.gameObject);
                return true;
            }
        }
        return false;
    }
}
