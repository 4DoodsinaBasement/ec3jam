using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransitionHandler : MonoBehaviour
{
    public GameObject TransitionScreen;
    public GameObject[] TransitionItems;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public bool textIn = false;
    public int counter = 0;
    public int counter2 = 0;
    public int rate = 0;
    public int rateOut = 0;
    public int index = 0;
    public int transitions = 10;
    List<float> transparency = new List<float>();

    void Start()
    {
        for (float i = transitions; i > 0; i--)
        {
            transparency.Add(i / transitions);
        }
        transparency.Add(0);
    }

    void Update()
    {
        if (fadeIn || fadeOut)
        {
            Transition();
        }
    }


    public void Transition()
    {
        if (fadeIn)
        {
            if (!textIn)
            {
                BackgroundTransition();
            }
            if (textIn)
            {
                TextTransition();
            }
        }
        if (fadeOut)
        {
            BackgroundTransition();
            TextTransition();
        }
    }



    public void BackgroundTransition()
    {
        Color currentColor;
        Image Background = TransitionItems[0].GetComponent<Image>();
        Background.gameObject.SetActive(true);
        currentColor = Background.color;
        if (fadeIn)
        {
            index = counter / rate;
            currentColor.a = transparency[(transparency.Count) - (index) - 1];
            counter++;
        }
        if (fadeOut)
        {
            index = counter / rateOut;
            currentColor.a = transparency[index];
        }
        Background.color = currentColor;
        if (counter >= rate * transparency.Count)
        {
            counter = 0;
            textIn = true;
            if (fadeOut)
            {
                Background.gameObject.SetActive(false);
            }

        }
    }

    public void TextTransition()
    {
        float currentColor;
        for (int i = 1; i < TransitionItems.Length; i++)
        {
            TextMeshProUGUI Item = TransitionItems[i].GetComponent<TextMeshProUGUI>();
            Item.gameObject.SetActive(true);
            currentColor = Item.alpha;

            //if(i == 1) { Item.fontSize = 200; }

            Item.alignment = TMPro.TextAlignmentOptions.Center;
            if (fadeIn)
            {
                index = counter2 / rate;
                currentColor = transparency[(transparency.Count) - (index) - 1];
            }
            if (fadeOut)
            {
                index = counter2 / rateOut;
                currentColor = transparency[index];
            }
            Item.alpha = currentColor;
            if (counter2 >= rate * transparency.Count)
            {
                counter2 = 0;
            }
        }
        counter++;
        counter2++;

        if (counter >= rate * transparency.Count)
        {
            if (fadeOut)
            {
                for (int i = 0; i < TransitionItems.Length; i++)
                {
                    GameObject ItemToDeactivate = TransitionItems[i];
                    ItemToDeactivate.gameObject.SetActive(false);

                }
            }
            counter = 0;
            counter2 = 0;
            textIn = false;
            fadeIn = false;
            fadeOut = false;
        }
    }
}
