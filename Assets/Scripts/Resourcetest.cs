using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourcetest : MonoBehaviour
{
    public int i = 0;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = ResourceLoader.tile_field[state];
    }

    // Update is called once per frame
    void Update()
    {
        if(i > 50)
        {
            i = 0;
            if (state == 3) { state = 0; } else { state++; }
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ResourceLoader.tile_field[state];
        }
        i++;
    }
}
