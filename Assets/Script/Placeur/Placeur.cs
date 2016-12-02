using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Placeur : MonoBehaviour {

    GameObject enemy;

    PlaceurTimer timer;

    public List<GameObject> itemsAvailable;
    public List<KeyCode> inputs;
    GameObject currentItem;
    bool placeItem;
    public bool setDirtyItem;

    public bool PlaceItem
    {
        get
        {
            return placeItem;
        }
        set
        {
            placeItem = value;
        }
    }

    void Start ()
    {
        enemy = GameObject.Find("Player");
        itemsAvailable = new List<GameObject>(Resources.LoadAll<GameObject>("Item"));
        timer = GetComponent<PlaceurTimer>();
	}
	
	void FixedUpdate ()
    {
        if (setDirtyItem)
        {
            DestroyItem();
        }

        if (placeItem)
        {
            Debug.LogWarning("Veuillez poser un objet");
        }

	    if (placeItem && Input.anyKeyDown)
	    {
            foreach (var input in inputs)
            {
                if (Input.GetKeyDown(input) && itemsAvailable.Count > inputs.IndexOf(input))
                {
                    GameObject prefab = itemsAvailable[inputs.IndexOf(input)];
                    currentItem = Instantiate(prefab, enemy ? enemy.transform.position + enemy.transform.forward * 3 : Vector3.zero, prefab.transform.rotation) as GameObject;
                    currentItem.name = prefab.name;
                    currentItem.GetComponent<Item>().placeur = this;
                    placeItem = false;
                }
            }
        }
    }

    public void DestroyItem()
    {
        if (!currentItem)
        {
            return;
        }
        Destroy(currentItem);
        currentItem = null;
        timer.IsFlowing = true;
        setDirtyItem = false;
    }

    public void End()
    {
        DestroyItem();
        timer.IsFlowing = false;
        placeItem = false;
    }

    public void ResetItem()
    {
        setDirtyItem = true;
    }
}
