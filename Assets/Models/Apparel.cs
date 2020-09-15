using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparel : LooseObject
{
	public Apparel(Material material, QualityEnum quality, int maxDurability, int currentDurability) : base(true, false, quality, maxDurability, currentDurability)
	{
		Material = material;
		DirectionSprite = new Dictionary<DirectionEnum, Sprite>();
		LoadSprites();
	}

	public Material Material { get; set; }
	public ApparelType apparelType { get; set; }
	Sprite South;
	Sprite North;
	Sprite East;
	public Dictionary<DirectionEnum, Sprite> DirectionSprite;
	private void LoadSprites()
	{

	}

	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum ApparelType
{
	Head,
	Garmet,
	Shirt,
	Trouser,
	Jacket,
	FlakArmor,
	FlakPants
}
public enum DirectionEnum
{
	South,
	North,
	East,
	West
}
