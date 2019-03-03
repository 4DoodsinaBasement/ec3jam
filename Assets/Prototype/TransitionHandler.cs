using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionHandler : MonoBehaviour
{
    public GameObject TransitionItem;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public int counter = 0;
    public int rate = 0;
    public int index = 0;
    public int transitions = 10;
    List<float> transparency = new List<float>();

    void Start()
    {
        for (float i = transitions; i > 0; i--)
        {
            transparency.Add(i / transitions);
            Debug.Log(i / transitions);
        }
        transparency.Add(0);
    }

    void Update()
    {
        if (fadeIn || fadeOut) { Transition(); }
    }


    public void Transition()
    {
        Color currentColor = TransitionItem.GetComponent<Image>().color;
        index = counter / rate;
        if (fadeIn)
        {
            currentColor.a = transparency[(transparency.Count) - (index) - 1];
            counter++;
        }
        if (fadeOut)
        {
            currentColor.a = transparency[index];
            counter++;
        }
        if (counter >= rate * transparency.Count)
        {
            counter = 0;
            fadeIn = false;
            fadeOut = false;
        }
        TransitionItem.GetComponent<Image>().color = currentColor;

    }
}
