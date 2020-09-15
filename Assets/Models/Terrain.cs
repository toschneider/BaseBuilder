using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
	public string Type { get; set; }
	public float MoveSpeedModifier { get; set; }
	public int Fertility { get; set; }
	public TerrainSupportType TerrainSupport { get; set; }

	public Terrain(string type, float moveSpeedModifier, int fertility, TerrainSupportType terrainSupport)
	{
		Type = type;
		MoveSpeedModifier = moveSpeedModifier;
		Fertility = fertility;
		TerrainSupport = terrainSupport;
	}
}
