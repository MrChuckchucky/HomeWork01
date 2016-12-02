using UnityEngine;

public class Analyze : States
{
    GameObject p_myObjectTouched;

    string p_myTestName = "Coffre";

    public Analyze(GameObject entity) : base(entity)
    {
    }

    public override void doOnce()
    {
        RaycastHit hit;
        Vector3 pos = _myEntity.transform.position;
        Vector3 direction = _myEntity.transform.forward;

        if (Physics.Raycast(pos, direction, out hit, 1.0f))
        {
            GameObject temp = hit.collider.gameObject;
            _myAi.setItemInFront(temp);

            if (_myAi.isNewWords(temp.name))
            {

            }
            else
            {
                _myAi.addNewWords(temp.name);
            }
        }
    }

    public override void toDo()
    {
    }
}