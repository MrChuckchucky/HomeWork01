using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AI : MonoBehaviour
{
    #region Variables
    StateMachine p_myStateMachine;

    List<string> p_myNewWords;
    List<string> p_myDangers;
    List<string> p_mySafety;
    List<Chest> p_myChests;

    GameObject p_myItemInFront;

    string p_myNextState;

    Vector3 p_myOrigin;

    int p_myRewards;

    bool p_myIsDoingSomething;

    [SerializeField]
    float p_myWalkSpeed;
    [SerializeField]
    float p_myDistanceErrorWalk;
    #endregion

    #region Setters
    public void addNewWords(string name)
    {
        string[] temp = name.Split(' ');
        foreach (string str in temp)
        {
            if (!p_myNewWords.Contains(str))
            {
                p_myNewWords.Add(str);
            }
        }
    }

    public void addDanger(string danger)
    {
        if (!isDangers(danger))
        {
            p_myDangers.Add(danger);
        }
    }

    public void addSafe(string safe)
    {
        if (!isSafety(safe))
        {
            p_mySafety.Add(safe);
        }
    }

    public void addChest(string chest)
    {
        if (!isKnownChest(chest))
        {
            p_myChests.Add(new Chest(chest));
        }
    }

    public void setItemInFront(GameObject item)
    {
        p_myItemInFront = item;
    }

    public void setNextState(string nextState)
    {
        p_myNextState = nextState;
    }

    public void setItemUsed(bool isLethal, int reward)
    {
        if (isLethal)
        {
            reset();
        }
        else
        {
            p_myRewards += reward;
        }
    }

    public void setChestUsed(string name, bool isLethal, int reward)
    {
        foreach (Chest c in p_myChests)
        {
            if (c.getName() == name)
            {
                if (isLethal)
                {
                    c.addDefeat();
                    reset();
                }
                else
                {
                    c.addVictory();
                    p_myRewards += reward;
                }
            }
        }
    }

    public void setIsDoingSomething(bool value)
    {
        p_myIsDoingSomething = value;
    }
    #endregion

    #region Getters
    public bool isNewWords(string name)
    {
        string[] temp = name.Split(' ');
        foreach (string str in temp)
        {
            if (!p_myNewWords.Contains(str))
            {
                return false;
            }
        }
        return true;
    }

    public bool isDangers(string danger)
    {
        return p_myDangers.Contains(danger);
    }

    public bool isSafety(string safe)
    {
        return p_mySafety.Contains(safe);
    }

    public bool isKnownChest(string chest)
    {
        return p_myChests.Where(p => p.name.Contains(chest)).Count() > 0;
    }

    public bool getIsDoingSomething()
    {
        return p_myIsDoingSomething;
    }

    public float getWalkSpeed()
    {
        return p_myWalkSpeed;
    }

    public float getDistanceErrorWalk()
    {
        return p_myDistanceErrorWalk;
    }

    public GameObject getItemInFront()
    {
        return p_myItemInFront;
    }

    public string getNextState()
    {
        return p_myNextState;
    }

    public Chest getChest(string chest)
    {
        foreach (Chest c in p_myChests)
        {
            if (c.getName() == chest)
            {
                return c;
            }
        }
        return null;
    }
    #endregion

    void Start()
    {
        p_myOrigin = transform.position;
        p_myNewWords = new List<string>();
        p_myDangers = new List<string>();
        p_mySafety = new List<string>();
        reset();
    }

    void Update()
    {
        p_myStateMachine.toDo();
    }

    void InitIA()
    {
        Walk walk = new Walk(this.gameObject);
        TestItem testItem = new TestItem(this.gameObject);
        Analyze analyze = new Analyze(this.gameObject);

        p_myStateMachine = new StateMachine(this.gameObject, walk);

        //Walk Transitions
        List<Transitions> transitions = new List<Transitions>();
        List<Conditions> conditions = new List<Conditions>();
        ActionEnded condition = new ActionEnded(this.gameObject);
        conditions.Add(condition);
        Transitions transition = new Transitions(analyze, p_myStateMachine, conditions);
        transitions.Add(transition);
        walk.setTransitions(transitions);

        //Analyze Transitions
        transitions = new List<Transitions>();
        conditions = new List<Conditions>();
        SomethingBehind condition2 = new SomethingBehind(this.gameObject);
        conditions.Add(condition2);
        transition = new Transitions(testItem, p_myStateMachine, conditions);
        transitions.Add(transition);
        conditions = new List<Conditions>();
        condition = new ActionEnded(this.gameObject);
        conditions.Add(condition);
        transition = new Transitions(walk, p_myStateMachine, conditions);
        transitions.Add(transition);
        analyze.setTransitions(transitions);

        //TestItem Transitions
        transitions = new List<Transitions>();
        conditions = new List<Conditions>();
        condition = new ActionEnded(this.gameObject);
        conditions.Add(condition);
        transition = new Transitions(walk, p_myStateMachine, conditions);
        transitions.Add(transition);
        testItem.setTransitions(transitions);
    }

    public void reset()
    {
        p_myRewards = 0;
        transform.position = p_myOrigin;
        setIsDoingSomething(false);
        setItemInFront(null);
        InitIA();
    }
}