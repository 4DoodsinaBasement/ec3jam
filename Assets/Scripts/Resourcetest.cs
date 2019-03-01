using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resourcetest : MonoBehaviour
{
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(i > 200)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ResourceLoader.tileSprites[1];
        }
        i++;
    }
}
