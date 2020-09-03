using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apparel : LooseObject
{
	public Apparel(Material material, QualityEnum quality, int maxDurability, int currentDurability) : base(true, false, quality, maxDurability, currentDurability)
	{
		Material = material;
	}

	public Material Material { get; set; }




	// Start is called before the first frame update
	void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
