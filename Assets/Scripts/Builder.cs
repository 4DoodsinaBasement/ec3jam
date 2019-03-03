using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Builder : MonoBehaviour
{
    public GameMaster gameMaster;
    public GameObject infoMenu;
    
    void Start()
    {
        infoMenu.gameObject.SetActive(false);
    }

    public void PointerEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        infoMenu.gameObject.SetActive(true);
    }
    
    public void PointerExit()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        infoMenu.gameObject.SetActive(false);
    }
    
    
    public void BuildCottage()
    {
        gameMaster.BuildBuilding(TileType.Cottage);
    }

    public void BuildFarm()
    {
        gameMaster.BuildBuilding(TileType.Farm);
    }
    
    public void BuildMill()
    {
        gameMaster.BuildBuilding(TileType.Mill);
    }
    
    public void BuildMarket()
    {
        gameMaster.BuildBuilding(TileType.Market);
    }
}