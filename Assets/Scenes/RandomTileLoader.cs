using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileLoader : MonoBehaviour
{
    
    public TileObject[] backgroundGrid;
    
    // Start is called before the first frame update
    void Awake()
    {
        int randomInt;
        TileType randomTile;
        backgroundGrid = GetComponentsInChildren<TileObject>();
        
        foreach (TileObject tile in backgroundGrid)
        {
            randomInt = Random.Range((int)TileType.Rocks0, (int)TileType.Rocks4 + 1);
            randomTile = (TileType)randomInt;
            if (randomTile == TileType.Rocks0) { randomTile = TileType.Field; }
            tile.SetCompleteSprite(randomTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
