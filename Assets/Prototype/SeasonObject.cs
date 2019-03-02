using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonType {Spring = 0, Summer = 1, Autumn = 2, Winter = 3};

[CreateAssetMenu(menuName = "Season", fileName = "Season", order = 1)]
public class SeasonObject : ScriptableObject
{
    public SeasonType seasonType;

    public int scLumberCamp;//the bounus of Lumber for each lubmer camp
    public int scStarvation; // scaler used for determining how many people die from starvation
    public int scExposure; // scaler used for determining how many people die from Exposuser
    public int scFireChance; //base for fire chance 

    
}