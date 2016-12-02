using UnityEngine;
using System.Collections;

public class Pickup
{
    public bool dead;
    public int bonus;

    public override string ToString()
    {
        return "This item is " + (dead ? "deadly" : "safe and the bonus given is " + bonus);
    }
}

public class Item : MonoBehaviour
{
    public Placeur placeur;
    
    public enum Effect
    {
        GOOD,
        BAD,
        NEUTRAL
    }

    public Effect currentEffect;
    [Range(0, 1)]
    public float deathPercent;
    public int bonus;

    public Pickup Use()
    {
        Pickup pickup = new Pickup();

        switch (currentEffect)
        {
            case Effect.NEUTRAL:
                float per = Random.Range(0.0f, 1000.0f);
                if ((per / 1000f) - deathPercent > 0)
                {
                    goto case Effect.GOOD;
                }
                else
                {
                    goto case Effect.BAD;
                }
            case Effect.GOOD:
                pickup.dead = false;
                pickup.bonus = bonus;
                break;
            case Effect.BAD:
                pickup.dead = true;
                pickup.bonus = 0;
                break;
            default:
                break;
        }
        DestroyItem();
        return pickup;
    }

    public void DestroyItem()
    {
        if (placeur)
        {
            placeur.ResetItem();
        }
    }
}
