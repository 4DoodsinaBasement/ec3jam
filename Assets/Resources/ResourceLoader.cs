using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static Sprite[] tile_field =  new Sprite[4];

    static ResourceLoader()
    {
        tile_field = Resources.LoadAll<Sprite>("Sprites/tile-field");
    }
}
