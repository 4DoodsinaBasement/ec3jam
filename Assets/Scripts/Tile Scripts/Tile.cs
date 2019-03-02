using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType {Field, Farm};

public class Tile : MonoBehaviour
{
    public TileType tileType;
    // Season season;

    Sprite[] tileSprites;
    Sprite currentSprite;

    int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        tileSprites = ResourceLoader.GetSpriteSheet(tileType);
        currentSprite = tileSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        i++;
        if (i >= 100)
        {
            i = 0;
            NextSeason();
        }
    }

    void NextSeason()
    {
        // if (season == Season.Winter) { season = Season.Spring; } else { season++; }
        // tileSprites = ResourceLoader.GetSpriteSheet(tileType);
        // currentSprite = tileSprites[(int)season];
        // this.gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
}
