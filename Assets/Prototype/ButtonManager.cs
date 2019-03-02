using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button farmButton;
    public Button lumberButton;
    public Button marketButton;
    public Button houseButton;
    public Text squareText;
    
    // Start is called before the first frame update
    void Start()
    {
        squareText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectFarm()
    {
        Debug.Log("Clicked Farm of " + gameObject.name);
        squareText.text = "Farm";
        DisableButtons();
    }

    public void SelectLumber()
    {
        Debug.Log("Clicked Lumber of " + gameObject.name);
        squareText.text = "Lumber";
        DisableButtons();
    }

    public void SelectMarket()
    {
        Debug.Log("Clicked Market of " + gameObject.name);
        squareText.text = "Market";
        DisableButtons();
    }

    public void SelectHouse()
    {
        Debug.Log("Clicked House of " + gameObject.name);
        squareText.text = "House";
        DisableButtons();
    }

    void DisableButtons()
    {
        farmButton.gameObject.SetActive(false);
        lumberButton.gameObject.SetActive(false);
        marketButton.gameObject.SetActive(false);
        houseButton.gameObject.SetActive(false);
    }
}
