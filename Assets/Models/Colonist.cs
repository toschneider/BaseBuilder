using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Colonist : Pawn
{
	//public GameObject Scheduler;
	public GameObject Head;
	public GameObject Body;
	public GameObject Hair;
	public GameObject WeaponSlot;
	public GameObject HeadApparelSlot;
	public GameObject GarmetApparelSlot;
	public GameObject ShirtApparelSlot;
	public GameObject TrouserApparelSlot;
	public GameObject JacketApparelSlot;
	public GameObject FlakArmorApparelSlot;
	public GameObject FlakTrouserApparelSlot;
	public Apparel HeadApparel { get; set; }
	public Apparel GarmetApparel { get; set; }
	public Apparel ShirtApparel { get; set; }
	public Apparel TrouserApparel { get; set; }
	public Apparel JacketApparel { get; set; }
	public Apparel FlakArmorApparel { get; set; }
	public Apparel FlakTrouserApparel { get; set; }
	public Colonist(string name, int health, Stat[] stats, float movementSpeed) : base(name,health,stats,movementSpeed)
	{
		LoadSPrites();
		HeadSpriteDictionary = new Dictionary<DirectionEnum, Sprite>();
	}
	Dictionary<DirectionEnum, Sprite> HeadSpriteDictionary;
	Dictionary<DirectionEnum, Sprite> BodySpriteDictionary;
	Dictionary<DirectionEnum, Sprite> HairSpriteDictionary;
	private void LoadSPrites()
	{
		//Todo Load ColonistSprites into Dictionaries
	}
	 public Vector2 GetPosition()
	{
		Transform transform = this.GetComponent<Transform>();
		return transform.position;
	}

	MyScheduler<List<int>, Job> schedule;
	public MyScheduler<List<int>, Job> Schedule
	{
		get
		{
			return schedule;
		}
		set
		{
			schedule = value;
		}
	}
	void Start()
	{
		//Todo Fix Scheduler
		Scheduler = GameObject.Find("Scheduler");
		Schedule = new MyScheduler<List<int>, Job>();
		for (int i = 0; i < Schedule.length; i++)
		{
			foreach(var sc in Enum.GetValues(typeof(Job)).Cast<Job>().ToList())
			{
				//Schedule[sc][0] = 3;
			}
		}
		//Todo remove AStar debug
		//World world = WorldController.Instance.World;
		//Vector2 dest = new Vector2(UnityEngine.Random.Range(0,world.Width), UnityEngine.Random.Range(0, world.Height));
		////Debug.Log("Calculate Path from " + GetPosition() + " to " + dest + ".");
		//AStar.CalculatePath(GetPosition(), dest, world, this);
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		World world = WorldController.Instance.World;
		Vector2 dest = new Vector2(UnityEngine.Random.Range(0, world.Width), UnityEngine.Random.Range(0, world.Height));
		//Debug.Log("Calculate Path from " + GetPosition() + " to " + dest + ".");
		//AStar.CalculatePath(GetPosition(), dest, world, this);
	}
	public void SetSchedule(Job job, int priority)
	{
		Schedule[job][0] = priority;
	}
	public new void Move(Vector2 direction)
	{
		base.Move(direction);
		if(direction.x > Math.Abs(direction.y))
		{
			//East
			SetApparel(DirectionEnum.East);
			SetSprites(DirectionEnum.East);
		} else if(direction.x < 0-Math.Abs(direction.y))
		{
			//West
			SetApparel(DirectionEnum.West);
			SetSprites(DirectionEnum.West);
		} else if(direction.y > Math.Abs(direction.x))
		{
			//North
			SetApparel(DirectionEnum.North);
			SetSprites(DirectionEnum.North);
		} else if(direction.y < 0 - Math.Abs(direction.x))
		{
			//South
			SetApparel(DirectionEnum.South);
			SetSprites(DirectionEnum.South);
		}
	}
	private void SetSprites(DirectionEnum dirEnum)
	{
		SpriteRenderer headRenderer = Head.GetComponent<SpriteRenderer>();
		headRenderer.sprite = HeadSpriteDictionary[dirEnum];
		SpriteRenderer bodyRenderer = Body.GetComponent<SpriteRenderer>();
		bodyRenderer.sprite = BodySpriteDictionary[dirEnum];
		SpriteRenderer hairRenderer = Hair.GetComponent<SpriteRenderer>();
		hairRenderer.sprite = HairSpriteDictionary[dirEnum];

		if (dirEnum == DirectionEnum.West)
		{
			headRenderer.flipX = true;
			bodyRenderer.flipX = true;
			hairRenderer.flipX = true;
		}
		else
		{
			headRenderer.flipX = false;
			bodyRenderer.flipX = false;
			hairRenderer.flipX = false;
		}

	}
	private void SetApparel(DirectionEnum dirEnum)
	{
		SpriteRenderer headRenderer = HeadApparelSlot.GetComponent<SpriteRenderer>();
		headRenderer.sprite = HeadApparel.DirectionSprite[dirEnum];

		SpriteRenderer garmetRenderer = GarmetApparelSlot.GetComponent<SpriteRenderer>();
		garmetRenderer.sprite = HeadApparel.DirectionSprite[dirEnum];

		SpriteRenderer shirtRenderer = ShirtApparelSlot.GetComponent<SpriteRenderer>();
		shirtRenderer.sprite = ShirtApparel.DirectionSprite[dirEnum];

		SpriteRenderer trouserRenderer = TrouserApparelSlot.GetComponent<SpriteRenderer>();
		trouserRenderer.sprite = TrouserApparel.DirectionSprite[dirEnum];

		SpriteRenderer jacketRenderer = JacketApparelSlot.GetComponent<SpriteRenderer>();
		jacketRenderer.sprite = JacketApparel.DirectionSprite[dirEnum];

		SpriteRenderer flakArmorRenderer = FlakArmorApparelSlot.GetComponent<SpriteRenderer>();
		flakArmorRenderer.sprite = FlakArmorApparel.DirectionSprite[dirEnum];

		SpriteRenderer flakTrouserrRenderer = FlakTrouserApparelSlot.GetComponent<SpriteRenderer>();
		flakTrouserrRenderer.sprite = FlakTrouserApparel.DirectionSprite[dirEnum];




		if (dirEnum == DirectionEnum.West)
		{
			headRenderer.flipX = true;
			garmetRenderer.flipX = true;
			shirtRenderer.flipX = true;
			trouserRenderer.flipX = true;
			jacketRenderer.flipX = true;
			flakArmorRenderer.flipX = true;
			flakTrouserrRenderer.flipX = true;

		} else
		{
			headRenderer.flipX = false;
			garmetRenderer.flipX = false;
			shirtRenderer.flipX = false;
			trouserRenderer.flipX = false;
			jacketRenderer.flipX = false;
			flakArmorRenderer.flipX = false;
			flakTrouserrRenderer.flipX = false;
		}
	}

}
