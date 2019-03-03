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

    public static AudioClip music_spring;
    public static AudioClip music_summer;
    public static AudioClip music_autumn;
    public static AudioClip music_winter;

    static ResourceLoader()
    {
        spriteSheet_field = Resources.LoadAll<Sprite>("Sprites/tile-field");
        spriteSheet_cottage = Resources.LoadAll<Sprite>("Sprites/tile-cottage");
        spriteSheet_farm = Resources.LoadAll<Sprite>("Sprites/tile-farm");
        spriteSheet_mill = Resources.LoadAll<Sprite>("Sprites/tile-mill");
        spriteSheet_market = Resources.LoadAll<Sprite>("Sprites/tile-market");

        music_spring = Resources.Load<AudioClip>("Music/Spring");
        music_summer = Resources.Load<AudioClip>("Music/Summer");
        music_autumn = Resources.Load<AudioClip>("Music/Autumn");
        music_winter = Resources.Load<AudioClip>("Music/Winter");
    }

    public static Sprite[] GetSpriteSheet(TileType tile)
    {
        switch (tile)
        {
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
