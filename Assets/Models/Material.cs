using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : MonoBehaviour
{
	public Material(MaterialEnum name, bool foodResource, bool apparelResource, bool buildResource, bool craftResource)
	{
		Name = name;
		FoodResource = foodResource;
		ApparelResource = apparelResource;
		BuildResource = buildResource;
		CraftResource = craftResource;
	}

	public MaterialEnum Name { get; set; }
	public bool FoodResource { get; set; }
	public bool ApparelResource { get; set; }
	public bool BuildResource { get; set; }
	public bool CraftResource { get; set; }



}

public enum MaterialEnum
{
	Wood,
	Steel,
	Wool,
	Rice
}
