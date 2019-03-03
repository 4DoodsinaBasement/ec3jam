using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingBuilder : MonoBehaviour
{
    public GameObject infoMenu;
    void Start()
    {
        infoMenu.gameObject.SetActive(false);
    }

    public void PointerEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("mouseing enter");
        ShowInfo();
    }
    
    public void PointerExit()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("mouseing exit");
        HideInfo();
    }
    
    void ShowInfo()
    {
        infoMenu.gameObject.SetActive(true);
    }

    void HideInfo()
    {
        infoMenu.gameObject.SetActive(false);
    }
}