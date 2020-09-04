
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
		this.CurrentTemperature = UnityEngine.Random.Range(this.MinTemperature, this.MaxTemperature);

        Tiles = new Tile[Height, Width];
		for (int i = 0; i < Height; i++)
		{
			for (int j = 0; j < Width; j++)
			{
                Tiles[i, j] = new Tile(this, TileType.Soil, j, i);
			}
		}
		Debug.Log("World created with " + (Height * Width) + " tiles.");
		Debug.Log("Create and save Textures");
		createTextures();
		Debug.Log("Created and saved Textures");
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
		string jsonString = File.ReadAllText(AssetPath + "/Data/Ground/" + "Ground.json");
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
	/// <summary>
	/// stores hsv-values for each TileType
	/// </summary>
	Dictionary<TileType, (float, float, float)> TileHSVColor = new Dictionary<TileType, (float, float, float)>()
	{
		{TileType.StonySoil, (0, 0, 65)},
		{TileType.Soil,  (29, 29, 39) },
		{TileType.SoftSand,  (39, 22, 51) },
		{TileType.ShallowWater , (195, 12, 38)},
		{TileType.ShallowOceanWater , (206, 18, 36)},
		{TileType.ShallowMovingWater , (195, 12, 38)},
		{TileType.Sand , (33, 28, 50)},
		{TileType.RoughStoneTile , (0, 0, 65)},
		{TileType.RichSoil , (27, 37, 29)},
		{TileType.Mud , (24, 36, 29)},
		{TileType.MarshySoil ,(47, 26, 29)},
		{TileType.Marsh , (200, 1, 98)},
		{TileType.LichenCoveredSoil , (29, 29, 39)},
		{TileType.Ice , (200, 1, 98)},
		{TileType.DeepWater , (212, 22, 33)},
		{TileType.DeepOceanWater , (212, 22, 33)},
		{TileType.ChestDeepMovingWater , (206, 18, 36)}
	};
	private void LoadFloorDictionary()
	{
		string AssetPath = Application.dataPath;
		string jsonString = File.ReadAllText(AssetPath + "/Data/Floor/" + "Floor.json");
	}
	Dictionary<FloorType, Floor> FloorDictionary = new Dictionary<FloorType, Floor>()
	{
	};
	/// <summary>
	/// normalize hsv-values to float between 0...1
	/// </summary>
	/// <param name="f">normalized hsv</param>
	/// <returns></returns>
	private (float, float, float) hsvTofloat((float,float,float) f)
	{
		float h, s, v;
		h = f.Item1;
		s = f.Item2;
		v = f.Item3;
		return (h / 360f, s / 100f, v / 100f);
	}

	/// <summary>
	/// creates and saves Textures as png for all TileTypes via SimplexNoise and HSV
	/// </summary>
	public void createTextures()
	{
		float[,] noises;
		int size = 1024;
		foreach (var tt in Enum.GetValues(typeof(TileType)).Cast<TileType>().ToList())
		{
			noises = createNoiseTextures(size);
			saveTextureToFile(noises, size, tt);
		}
	}
	private void saveTextureToFile(float[,] noiseMap,int size, TileType tileType)
	{
		Texture2D tex = new Texture2D(size, size);
		string name = tileType.ToString();
		Color color;
		Debug.Log("Texture size " + size);
		Debug.Log("Texture created with " + (size * size) + " pixels.");
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				float r, g, b, a, h, s, v;
				r = 255;
				g = 0;
				b = 0;
				a = 0.75f + 0.25f * noiseMap[i, j];
				(h, s, v) = hsvTofloat(TileHSVColor[tileType]);
				v = v/2 + (v/2)* noiseMap[i, j];


				//color = new Color(r,g,b,a);
				color = Color.HSVToRGB(h,s,v);
				tex.SetPixel(i, j, color);
			}
		}
		//Encode to png
		byte[] bytes = tex.EncodeToPNG();
		UnityEngine.Object.Destroy(tex);
		File.WriteAllBytes(Application.dataPath + "/Textures/"+ name +".png", bytes);
		Debug.Log(Application.dataPath + "/Textures/" + name + ".png");
	}

	private float[,] createNoiseTextures(int size)
	{
		float TextureScale = 3f;//300f;
		float[,] noises = new float[size, size];
		OpenSimplexNoise simplexNoise = new OpenSimplexNoise(1321414143333443);
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				noises[i, j] = (float)simplexNoise.Evaluate(TextureScale * ((float)j) / ((float)Width), TextureScale * ((float)i) / ((float)Height));
			}
		}

		return noises;
	}

}
public enum Biome
{
	Desert
}
