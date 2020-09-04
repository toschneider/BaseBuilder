using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
	string Type;
	int MoveSpeedModifier;
	int Fertility;
	TerrainSupportType TerrainSupport;
	public Terrain(string type, int moveSpeedModifier, int fertility, TerrainSupportType terrainSupport)
	{
		Type = type;
		MoveSpeedModifier = moveSpeedModifier;
		Fertility = fertility;
		TerrainSupport = terrainSupport;
	}
}
