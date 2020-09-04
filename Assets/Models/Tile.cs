using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tile
{
	World world;
	public int x { get; }
	public int y { get; }
	Action<Tile> cbTileTypeCHanged;
	TileType type = TileType.ChestDeepMovingWater;
	FloorType floorType = FloorType.None;
	Ground ground;
	Floor floor;
	public TileType tileType {
		get {
			return type;
		}
		set {
			TileType oldType = type;
			type = value;
			//Call the Callback
			if(cbTileTypeCHanged != null)
				cbTileTypeCHanged(this);
		}
	}
	public Tile(World world, TileType tileType, int x, int y)
	{

		this.x = x;
		this.y = y;
		this.world = world;
		this.tileType = tileType;
	}
	public void RegisterTileTypeChangedCallback(Action<Tile> callback)
	{
		cbTileTypeCHanged += callback;
	}
	public void UnRegisterTileTypeChangedCallback(Action<Tile> callback)
	{
		cbTileTypeCHanged -= callback;
	}
	public static Terrain getTerrain()
	{

		return null;
	}








}
public enum TileType
	{
		StonySoil,
		Soil,
		SoftSand,
		ShallowWater,
		ShallowOceanWater,
		ShallowMovingWater,
		Sand,
		RoughStoneTile,
		RichSoil,
		Mud,
		MarshySoil,
		Marsh,
		LichenCoveredSoil,
		Ice,
		DeepWater,
		DeepOceanWater,
		ChestDeepMovingWater
	}
public enum FloorType
{
	None
}
public enum TerrainSupportType
		{
			Heavy,
			Light,
			None
		}
