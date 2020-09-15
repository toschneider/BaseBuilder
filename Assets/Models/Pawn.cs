using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
	public string Name { get; set; }
	public int Health { get; set; }
	public Stat[] Stats { get; set; }
	List<LooseObject> Inventory;
	public float BaseMovementSpeed { get; set; }
	Vector2 Movement;
	public GameObject Position;
	//public GameObject Schedulerr;
	public Scheduler Scheduler;
	public Pawn(string name, int health, Stat[] stats, float movementSpeed)
	{
		Name = name;
		Health = health;
		Stats = stats;
		Movement = new Vector2(0,0);
		Inventory = new List<LooseObject>();
		BaseMovementSpeed = movementSpeed;
	}

	public float calcMovementspeed(float modifier)
	{
		float move = BaseMovementSpeed;
		//Todo Movementboni/mali
		move *= modifier;

		return move;
	}

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
	public void Move(Vector2 direction)
	{
		Vector3 pos = Position.transform.position;
		Tile tile = WorldController.Instance.World.getTileAt((int)pos.x, (int)pos.y);


	}

    // Update is called once per frame
    void Update()
    {

    }

	private void doTask()
	{
		Task task = Scheduler.
	}
}
