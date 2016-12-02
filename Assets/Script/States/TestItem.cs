using UnityEngine;

public class TestItem : States
{
    GameObject p_myObjToTest;
    Pickup p_myPickUp;

    public TestItem(GameObject entity) : base(entity)
    {
    }

    public override void doOnce()
    {
        p_myObjToTest = _myAi.getItemInFront();
        if (_myAi.isNewWords(p_myObjToTest.name) && !p_myObjToTest.name.Contains("Coffre"))
        {
            if (_myAi.isDangers(p_myObjToTest.name))
            {
                p_myObjToTest.GetComponent<Item>().DestroyItem();
                return;
            }
            if (_myAi.isSafety(p_myObjToTest.name))
            {
                p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
                _myAi.setItemUsed(p_myPickUp.dead, p_myPickUp.reward);
                return;
            }
            Debug.LogError("There is a bug");
        }
        else if (p_myObjToTest.name.Contains("Coffre"))
        {
            if (_myAi.isKnownChest(p_myObjToTest.name))
            {
                Chest chest = _myAi.getChest(p_myObjToTest.name);
                p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
                float rand = Random.Range(0.0f, 1.0f);
                if (rand <= chest.getChance())
                {
                    _myAi.setChestUsed(chest.getName(), p_myPickUp.dead, p_myPickUp.reward);
                }
                return;
            }
            else
            {
                _myAi.addChest(p_myObjToTest.name);
                p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
                _myAi.setChestUsed(p_myObjToTest.name, p_myPickUp.dead, p_myPickUp.reward);
            }
        }
        else
        {
            p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
            _myAi.addNewWords(p_myObjToTest.name);
            if (p_myPickUp.dead)
            {
                _myAi.addDanger(p_myObjToTest.name);
            }
            else
            {
                _myAi.addSafe(p_myObjToTest.name);
            }
        }
        _myAi.setItemUsed(p_myPickUp.dead, p_myPickUp.reward);
    }

    public override void toDo()
    {
    }
}