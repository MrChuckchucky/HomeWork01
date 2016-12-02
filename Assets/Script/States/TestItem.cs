using UnityEngine;

public class TestItem : States
{
    GameObject p_myObjToTest;
    PickUp p_myPickUp;

    public TestItem(GameObject entity) : base(entity)
    {
    }

    public override void doOnce()
    {
        p_myObjToTest = _myAI.getItemInFront();
        if (!_myAI.isNewWords(p_myObjToTest.name) || !p_myObjToTest.Contains("Coffre"))
        {
            if (_myAI.isDangers(p_myObjToTest.name))
            {
                return;
            }
            if (_myAI.isSafety(p_myObjToTest.name))
            {
                p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
                _myAI.setItemUsed(p_myPickUp.dead, p_myPickUp.bonus);
                return;
            }
            Debug.LogError("There is a bug");
        }
        else if (p_myObjToTest.Contains("Coffre"))
        {
            if (_myAI.isKnownChest(p_myObjToTest.name))
            {
                Chest chest = _myAI.getChest();
                float rand = Random.Range(0.0f, 1.0f);
                if (rand < chest.getChance())
                {
                    _myAI.setChestUsed(chest.getName(), p_myPickUp.dead, p_myPickUp.reward);
                }
                return;
            }
            else
            {
                _myAI.addChest(p_myObjToTest.name);
                p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
                _myAI.setChestUsed(p_myObjToTest.name, p_myPickUp.dead, p_myPickUp.reward);
            }
        }
        else
        {
            _myAI.addNewWords(p_myPickUp.name);
            if (p_myPickUp.dead)
            {
                _myAI.addDanger(p_myPickUp.name);
            }
            else
            {
                _myAI.addSafe(p_myPickUp.name);
            }
        }
        p_myPickUp = p_myObjToTest.GetComponent<Item>().Use();
        _myAI.setItemUsed(p_myPickUp.dead, p_myPickUp.bonus);
    }

    public override void toDo()
    {
    }
}