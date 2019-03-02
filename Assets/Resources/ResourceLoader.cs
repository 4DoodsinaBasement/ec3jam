using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static Sprite[] tile_field =  new Sprite[4];
    public static Sprite[] tile_farm =  new Sprite[4];

    static ResourceLoader()
    {
        tile_field = Resources.LoadAll<Sprite>("Sprites/tile-field");
        tile_farm = Resources.LoadAll<Sprite>("Sprites/tile-farm");
    }

    public static Sprite[] GetSpriteSheet(TileType tile)
    {
        switch (tile)
        {
            case TileType.Field :
                return tile_field;
                break;
            case TileType.Farm :
                return tile_farm;
                break;
            default :
                return null;
        }
    }
}
