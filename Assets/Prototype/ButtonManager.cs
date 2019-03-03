using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject gameMasterObject;
    GameMaster gameMaster;
    
    public Button farmButton;
    public Button lumberButton;
    public Button marketButton;
    public Button houseButton;
    public Text squareText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameMaster = gameMasterObject.GetComponent<GameMaster>();
        // squareText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextSeason() { gameMaster.SeasonUpkeep(); }
    public void NextTick() { gameMaster.GameTick(); }

    public void SelectFarm()
    {
        if (gameMaster.BuildBuilding(TileType.Farm))
        {
            Debug.Log("Clicked Farm of " + gameObject.name);
            squareText.text = "Farm";
            DisableButtons();
        } else { Debug.Log("...No..."); }
    }

    public void SelectLumber()
    {
        if (gameMaster.BuildBuilding(TileType.Mill))
        {
            Debug.Log("Clicked Lumber of " + gameObject.name);
            squareText.text = "Lumber";
            DisableButtons();
        } else { Debug.Log("...No..."); }
    }

    public void SelectMarket()
    {
        if (gameMaster.BuildBuilding(TileType.Market))
        {
            Debug.Log("Clicked Market of " + gameObject.name);
            squareText.text = "Market";
            DisableButtons();
        } else { Debug.Log("...No..."); }
    }

    public void SelectHouse()
    {
        if (gameMaster.BuildBuilding(TileType.Cottage))
        {
            Debug.Log("Clicked House of " + gameObject.name);
            squareText.text = "House";
            DisableButtons();
        } else { Debug.Log("...No..."); }
    }

    void DisableButtons()
    {
        farmButton.gameObject.SetActive(false);
        lumberButton.gameObject.SetActive(false);
        marketButton.gameObject.SetActive(false);
        houseButton.gameObject.SetActive(false);
    }
}
