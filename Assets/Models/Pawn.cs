using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
	public string Name { get; set; }
	public int Health { get; set; }
	public Stat[] Stats { get; set; }

    public GameObject Scheduler;
    List<LooseObject> Inventory;

	// Start is called before the first frame update
	void Start()
    {
        Inventory = new List<LooseObject>();
    }
    public void PickUpItem(LooseObject item)
	{
		if (item.Carriable)
		{
			PickUp(item);
		}
		else if (item.Uninstallable)
		{
			item.Uninstall();
			PickUp(item);
		}
		return;
	}
	private void PickUp(LooseObject item)
	{
		GameObject go = item.GetComponent<GameObject>();
		Inventory.Add(item);
		go.SetActive(false);
	}


    // Update is called once per frame
    void Update()
    {

    }
}
