using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public TileType finishedType;
    public TileType startType;
    public SeasonType seasonType;
    public SpriteRenderer render;

    public bool underConstruction = false;
    public float buildProgress = 0;
    public float buildTarget;

    float tintAmount = 0.95f;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (render == null) { render = gameObject.GetComponent<SpriteRenderer>(); }
        render.sprite = ResourceLoader.GetSpriteSheet(startType)[(int)seasonType];

        if (startType != finishedType)
        {
            Color temp = render.color;
            temp.a = 0.25f;
            render.color = temp;
        }
        else
        {
            Color temp = render.color;
            temp.a = 1.0f;
            render.color = temp;
        }
    }
}
