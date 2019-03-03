using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public TileType tileType;
    public SeasonType seasonType;
    public SpriteRenderer render;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        render.sprite = ResourceLoader.GetSpriteSheet(tileType)[(int)seasonType];
    }
}
