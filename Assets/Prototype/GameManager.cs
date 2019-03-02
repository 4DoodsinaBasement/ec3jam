using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int turnCount; // number of maxiumn turns
    public float turnTimeBaseSec; // the number of seconds in a season
    float NumberOfElapsedSeasonSec = 0; // the elapsed time in season  
    public int SeasonIndicator; // 0 = spring, 1 summer 2 autumn 3 winter 


    //player resources 
    public int prPopulation;
    public int prSterling;
    public int prLumber;
    public int prGrain;

    //scalers
    
    
    int NumberBuildingsUnderConstruction; // currently under construction 

    public int scFarmstead; //the bounus of grain for each farm
    public int scLumberCamp;//the bounus of Lumber for each lubmer camp
    public int scStarvation; // scaler used for determining how many people die from starvation
    public int scExposure; // scaler used for determining how many people die from Exposuser
    public int scFireChance; //base for fire chance 
    int FireChance; 

    //buildingCount
    int bcFarmstead = 0;
    int bcCottage = 0;
    int bcLumberCamp = 0;
    int bcMarket = 0;
    int bcForts = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
