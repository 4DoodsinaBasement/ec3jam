using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceLoader
{
    public static Sprite[] tileSprites =  new Sprite[2];

    static ResourceLoader()
    {
        tileSprites[0] = Resources.Load("Sprites/tile-field", typeof(Sprite)) as Sprite;
        tileSprites[1] = Resources.Load("Sprites/tile-farm", typeof(Sprite)) as Sprite;
    }
}
