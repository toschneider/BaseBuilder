
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class World
{
    Tile[,] Tiles;
	public int Width { get; }
	public int Height { get; }

	public Biome WorldBiome { get; set; }
	public int MaxTemperature { get; set; }
	public int MinTemperature { get; set; }
	public int CurrentTemperature { get; set; }
	public float NoiseScale = 10f; ///10f;

	public World(Biome biome, int minTemperature, int maxTemperature, int width = 125, int height = 125)
	{
        this.Width = width;
        this.Height = height;
		this.WorldBiome = biome;
		this.MaxTemperature = maxTemperature;
		this.MinTemperature = minTemperature;
		this.CurrentTemperature = Random.Range(this.MinTemperature, this.MaxTemperature);

        Tiles = new Tile[Height, Width];
		for (int i = 0; i < Height; i++)
		{
			for (int j = 0; j < Width; j++)
			{
                Tiles[i, j] = new Tile(this, TileType.Soil, j, i);
			}
		}
		Debug.Log("World created with " + (Height * Width) + " tiles.");
	}

	public void RandomizeTiles()
	{
		Debug.Log("RandomizeTiles");
		float noise;
		int countSoil = 0;
		float maxNoise = 0;
		for (int i = 0; i < Height; i++)
		{
			for (int j = 0; j < Width; j++)
			{
				/*
				if(Random.Range(0,2) == 0)
				{
					Tiles[i, j].tileType = TileType.DeepWater;
				} else
				{
					Tiles[i, j].tileType = TileType.Soil;
				}
				*/
				//noise = Mathf.PerlinNoise(NoiseScale*((float)j)/ ((float)Width), NoiseScale*((float)i) / ((float)Height));
				OpenSimplexNoise simplexNoise = new OpenSimplexNoise(1321414143333443);
				noise = (float) simplexNoise.Evaluate(NoiseScale * ((float)j) / ((float)Width), NoiseScale * ((float)i) / ((float)Height));

				//Debug.Log("noise at w,h: " + j + "," + i + ": " + noise);
				if(noise > 0.5)
				{
					Tiles[i, j].tileType = TileType.RichSoil;
					countSoil++;
					maxNoise = noise > maxNoise ? noise : maxNoise;
				} else if(noise > 0.3)
				{
					Tiles[i, j].tileType = TileType.Soil;
					countSoil++;
					maxNoise = noise > maxNoise ? noise : maxNoise;
				}
				else
				{
					Tiles[i, j].tileType = TileType.DeepWater;
				}
			}
			Debug.Log("soilRatio: " + countSoil + "/" + (Height * Width) + " maxnoise: " + maxNoise);
		}
	}

	Dictionary<TileType, Terrain> MyDictionary = new Dictionary<TileType, Terrain>()
	{
		{TileType.StonySoil, new Terrain("Stony soil", 87, 70, TerrainSupportType.Heavy) },
		{TileType.Soil, new Terrain("Soil", 87, 100, TerrainSupportType.Heavy) },
		{TileType.SoftSand, new Terrain("Soft sand", 52, 0, TerrainSupportType.Light) },
		{TileType.ShallowWater , new Terrain("Shallow water", 30, 0, TerrainSupportType.None)},
		{TileType.ShallowOceanWater , new Terrain("Shallow ocean water", 30, 0, TerrainSupportType.None)},
		{TileType.ShallowMovingWater , new Terrain("Shallow moving water", 30, 0, TerrainSupportType.None)},
		{TileType.Sand , new Terrain("Sand", 76, 10, TerrainSupportType.Heavy)},
		{TileType.RoughStoneTile , new Terrain("Rough stone tile", 87, 0, TerrainSupportType.Heavy)},
		{TileType.RichSoil , new Terrain("Rich soil", 87, 140, TerrainSupportType.Heavy)},
		{TileType.Mud , new Terrain("Mud", 52, 0, TerrainSupportType.None)},
		{TileType.MarshySoil , new Terrain("Marshy soil", 46, 100, TerrainSupportType.None)},
		{TileType.Marsh , new Terrain("Marsh", 30, 0, TerrainSupportType.None)},
		{TileType.LichenCoveredSoil , new Terrain("Lichen-covered soil", 81, 100, TerrainSupportType.Heavy)},
		{TileType.Ice , new Terrain("Ice", 52, 0, TerrainSupportType.Heavy)},
		{TileType.DeepWater , new Terrain("Deep water", 0, 0, TerrainSupportType.None)},
		{TileType.DeepOceanWater , new Terrain("Deep ocean water", 0, 0, TerrainSupportType.None)},
		{TileType.ChestDeepMovingWater , new Terrain("Chest-deep moving water", 22, 0, TerrainSupportType.None)}
	};
	public void LoadTerrainDictionaryFromJson()
	{
		string AssetPath = Application.dataPath;
		string jsonString = File.ReadAllText(AssetPath + "/Data/" + "Tiles.json");
	}
	public void SaveTerrainDictionaryToJson()
	{

	}

	public Tile getTileAt(int x, int y)
	{
		if(x >= Width || y >= Height || x < 0 || y < 0)
		{
			return null;
		}
		return Tiles[y, x];
	}
}
public enum Biome
{
	Desert
}
