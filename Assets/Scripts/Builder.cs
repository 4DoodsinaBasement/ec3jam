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
        infoMenu.gameObject.SetActive(true);
    }
    
    public void PointerExit()
    {
        infoMenu.gameObject.SetActive(false);
    }
    
    
    public void BuildCottage()
    {

    }

    public void BuildFarm()
    {
        
    }
    
    public void BuildMill()
    {
        
    }
    
    public void BuildMarket()
    {
        
    }
}