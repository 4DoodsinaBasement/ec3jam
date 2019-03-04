using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static Sprite[] spriteSheet_rocks1 = new Sprite[4];
    public static Sprite[] spriteSheet_rocks2 = new Sprite[4];
    public static Sprite[] spriteSheet_field =  new Sprite[4];
    public static Sprite[] spriteSheet_cottage =  new Sprite[4];
    public static Sprite[] spriteSheet_farm =  new Sprite[4];
    public static Sprite[] spriteSheet_mill =  new Sprite[4];
    public static Sprite[] spriteSheet_market =  new Sprite[4];

    public static AudioClip[] gameMusic = new AudioClip[4];

    static ResourceLoader()
    {
        spriteSheet_rocks1 = Resources.LoadAll<Sprite>("Sprites/tile-rocks1");
        spriteSheet_rocks2 = Resources.LoadAll<Sprite>("Sprites/tile-rocks2");
        spriteSheet_field = Resources.LoadAll<Sprite>("Sprites/tile-field");
        spriteSheet_cottage = Resources.LoadAll<Sprite>("Sprites/tile-cottage");
        spriteSheet_farm = Resources.LoadAll<Sprite>("Sprites/tile-farm");
        spriteSheet_mill = Resources.LoadAll<Sprite>("Sprites/tile-mill");
        spriteSheet_market = Resources.LoadAll<Sprite>("Sprites/tile-market");

        gameMusic[0] = Resources.Load<AudioClip>("Music/Spring");
        gameMusic[1] = Resources.Load<AudioClip>("Music/Summer");
        gameMusic[2] = Resources.Load<AudioClip>("Music/Autumn");
        gameMusic[3] = Resources.Load<AudioClip>("Music/Winter");
    }

    public static Sprite[] GetSpriteSheet(TileType tile)
    {
        switch (tile)
        {
            case TileType.Rocks1 :
                return spriteSheet_rocks1;
            case TileType.Rocks2 :
                return spriteSheet_rocks2;
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

    public static AudioClip[] GetMusic() { return gameMusic; }
}
