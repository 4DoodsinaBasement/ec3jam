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

    void Awake()
    {
        render = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (render == null) { render = gameObject.GetComponent<SpriteRenderer>(); }
        render.sprite = ResourceLoader.GetSpriteSheet(startType)[(int)seasonType];

        if (startType != finishedType)
        {
            SetAlpha(0.3f);
        }
        else
        {
            SetAlpha(1.0f);
        }
    }

    public void SetColor(Color newColor)
    {
        Debug.Log("New Color");
        render.color = newColor;
    }

    public void SetAlpha(float newAlpha)
    {
        Color temp = render.color;
        temp.a = newAlpha;
        render.color = temp;
    }
}
