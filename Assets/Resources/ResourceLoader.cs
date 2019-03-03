using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static Sprite[] spriteSheet_borderless = new Sprite[4];
    public static Sprite[] spriteSheet_field =  new Sprite[4];
    public static Sprite[] spriteSheet_cottage =  new Sprite[4];
    public static Sprite[] spriteSheet_farm =  new Sprite[4];
    public static Sprite[] spriteSheet_mill =  new Sprite[4];
    public static Sprite[] spriteSheet_market =  new Sprite[4];

    static ResourceLoader()
    {
        spriteSheet_borderless = Resources.LoadAll<Sprite>("Sprites/tile-borderless");
        spriteSheet_field = Resources.LoadAll<Sprite>("Sprites/tile-field");
        spriteSheet_cottage = Resources.LoadAll<Sprite>("Sprites/tile-cottage");
        spriteSheet_farm = Resources.LoadAll<Sprite>("Sprites/tile-farm");
        spriteSheet_mill = Resources.LoadAll<Sprite>("Sprites/tile-mill");
        spriteSheet_market = Resources.LoadAll<Sprite>("Sprites/tile-market");
    }

    public static Sprite[] GetSpriteSheet(TileType tile)
    {
        switch (tile)
        {
            case TileType.Borderless :
                return spriteSheet_borderless;            
            case TileType.Field :
                return spriteSheet_field;
            case TileType.Cottage :
                return spriteSheet_cottage;
            case TileType.Farm :
                return spriteSheet_farm;
            case TileType.Mill :
                return spriteSheet_mill;
            case TileType.Market :
                return spriteSheet_market;
            default :
                return null;
        }
    }
}
