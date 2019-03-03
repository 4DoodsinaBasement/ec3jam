using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public TileType tileType;
    public SeasonType seasonType;
    public SpriteRenderer render;

    float tintAmount = 0.95f;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (render == null) { render = gameObject.GetComponent<SpriteRenderer>(); }
        render.sprite = ResourceLoader.GetSpriteSheet(tileType)[(int)seasonType];
    }

    // public void NewTint()
    // {
    //     float x = Random.Range(tintAmount, 1.0f);
    //     render.color = new Color(x, x, x);
    // }
}
