using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{

    public static WorldController Instance { get; protected set; }
    public Sprite soilSprite;
    public Sprite deepWaterSprite;
    public Sprite richSoilSprite;
    float randomizeTileTimer;
    public World World { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
		{
            Debug.Log("There should never be two world controllers.");
		}
        Instance = this;

        World = new World(Biome.Desert, 22, 36);
        randomizeTileTimer = 2f;

        // Create a GameObject for each of out TIles, so they show visually.
        for (int y = 0; y < World.Height; y++)
		{
			for (int x = 0; x < World.Width; x++)
			{
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                Tile tile_data = World.getTileAt(x, y);
                tile_go.transform.position = new Vector3(tile_data.x, tile_data.y, 0);
                tile_go.transform.SetParent(this.transform, true);

                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback( (tile) => { OnTileTypeChanged(tile, tile_go); } );

                /*if(tile_data.tileType == TileType.Soil)
				{
                    tile_sr.sprite = soilSprite;
				} else if(tile_data.tileType == TileType.DeepWater)
				{
                    tile_sr.sprite = deepWaterSprite;

                }*/
			}
		}
        World.RandomizeTiles();
    }



    // Update is called once per frame
    void Update()
    {
       /* randomizeTileTimer -= Time.deltaTime;
        if(randomizeTileTimer < 0)
		{
            World.RandomizeTiles();
            randomizeTileTimer = 2f;
		}*/
    }

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
	{
        if (tile_data.tileType == TileType.Soil)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = soilSprite;
        } else if(tile_data.tileType == TileType.DeepWater)
		{
            tile_go.GetComponent<SpriteRenderer>().sprite = deepWaterSprite;
        }
        else if (tile_data.tileType == TileType.RichSoil)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = richSoilSprite;
        }
        else
		{
            Debug.LogError("OnTileTypeChanged - Unrecognized TileType.");
		}
    }

    public Tile GetTileAtWorldCoordinate(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return Instance.World.getTileAt(x, y);
    }

}
