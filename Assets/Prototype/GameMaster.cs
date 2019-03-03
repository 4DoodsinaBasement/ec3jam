using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonType {Spring = 0, Summer = 1, Autumn = 2, Winter = 3};
public enum TileType {Field = 0, Cottage = 1, Farm = 2, Mill = 3, Market = 4, Fort = 5};


public class GameMaster : MonoBehaviour
{
    public SeasonData[] seasonData;
    
    // Game Settings
    public int maxTurns = 12; // number of maxiumn turns
    public float totalTurnTime = 60;
    public float currentTurnTime = 0;
    public float totalTickTime = 6;
    public float currentTickTime = 0;
    public SeasonData currentSeason;
    public int currentSeasonIndex = 0;

    // Player Resources 
    public int populationCap = 0;
    public int population = 120;
    public int grain = 300;
    public int lumber = 200;
    public int sterling = 80;
    public int buildingsConstructing;

    // Player Building Count
    public int cottageCount = 0;
    public int farmCount = 0;
    public int millCount = 0;
    public int marketCount = 0;
    public int fortsCount = 0;

    // Base Multipliers
    public float populationIncreasePercent;
    public float buildRate = 1.0f;
    public float starveChance = 0.5f;
    public float exposeChance;
    public float fireChance;

    // Encounter Bonuses
    public int populationEncounterBonus = 0;
    public int grainEncounterBonus = 0;
    public int lumberEncounterBonus = 0;
    public int sterlingEncounterBonus = 0;

    // Season Report
    public int report_populationIncrease = 0;
    public int report_grainIncrease = 0;
    public int report_sterlingIncrease = 0;
    public int report_upkeepCost_grain = 0;
    public int report_upkeepCost_lumber = 0;
    public int report_upkeepCost_sterling = 0;
    public int report_grainSpentOnPop = 0;
    public int report_starvationDeaths = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentSeason = seasonData[0];
    }

    // Update is called once per frame
    void Update() { }


    // Game Time Functions
    #region
    public void SeasonUpkeep()
    {
        NextSeason();
        ZeroReports();
        IncreasePopulation();
        CollectGrain();
        CollectSterling();
        UpkeepCost();
        PopulationEat();
    }

    void GameTick()
    {

    }
    #endregion


    // Game Phase Functions
    #region 
    void NextSeason()
    {
        if (currentSeasonIndex == 3) { currentSeasonIndex = 0; } else { currentSeasonIndex++; }
        currentSeason = seasonData[currentSeasonIndex];
    }
    
    void ZeroReports()
    {
        report_populationIncrease = 0;
        report_grainIncrease = 0;
        report_sterlingIncrease = 0;
        report_upkeepCost_grain = 0;
        report_upkeepCost_lumber = 0;
        report_upkeepCost_sterling = 0;
        report_grainSpentOnPop = 0;
        report_starvationDeaths = 0;
    }

    void IncreasePopulation()
    {
        populationIncreasePercent = Random.Range(0.0f, 0.1f);
        report_populationIncrease = (int)(population * populationIncreasePercent * currentSeason.PopulationMod) + populationEncounterBonus;
        populationEncounterBonus = 0;
        population += report_populationIncrease;
    }

    void CollectGrain()
    {
        if (currentSeason.seasonType == SeasonType.Autumn)
        {
            report_grainIncrease += farmCount * (FarmData.grainIncrease + grainEncounterBonus);
            grainEncounterBonus = 0;
        }
        grain += report_grainIncrease;
    }

    void CollectSterling()
    {
        report_sterlingIncrease += (int)(marketCount * MarketData.sterlingIncrease * currentSeason.SterlingMod) + sterlingEncounterBonus;
        sterling += report_sterlingIncrease;
    }

    void UpkeepCost()
    {
        // Grain Upkeep
        report_upkeepCost_grain += 
            (cottageCount * CottageData.grainUpkeep) + 
            (millCount * MillData.grainUpkeep) + 
            (marketCount * MarketData.grainUpkeep);
        report_upkeepCost_grain += 
            (currentSeason.seasonType == SeasonType.Spring) ? 
            (farmCount * FarmData.grainUpkeep) : 0;

        // Lumber Upkeep
        report_upkeepCost_lumber += 
            (cottageCount * CottageData.lumberUpkeep) + 
            (farmCount * FarmData.lumberUpkeep) + 
            (millCount * MillData.lumberUpkeep) + 
            (marketCount * MarketData.lumberUpkeep);

        // Sterling Upkeep
        report_upkeepCost_sterling += 
            (cottageCount * CottageData.sterlingUpkeep) + 
            (farmCount * FarmData.sterlingUpkeep) + 
            (millCount * MillData.sterlingUpkeep) + 
            (marketCount * MarketData.sterlingUpkeep);
        
        // Apply Report to Resources
        grain -= (report_upkeepCost_grain < grain) ? report_upkeepCost_grain : grain;
        lumber -= (report_upkeepCost_lumber < lumber) ? report_upkeepCost_lumber : lumber;
        sterling -= (report_upkeepCost_sterling < sterling) ? report_upkeepCost_sterling : sterling;
    }

    void PopulationEat()
    {
        if (population <= grain)
        {
            report_grainSpentOnPop = population;
            report_starvationDeaths = 0;
        }
        else // the bad stuff
        {
            report_grainSpentOnPop = grain;
            for (int starvingPopulation = population - grain; starvingPopulation > 0; starvingPopulation--)
            {
                if (PercentChance(starveChance * currentSeason.StarveMod)) { report_starvationDeaths++; }
            }
        }
        grain -= (report_grainSpentOnPop < grain) ? report_grainSpentOnPop : grain;
        population -= (report_starvationDeaths < population) ? report_starvationDeaths : population;
    }

    bool PercentChance(float percentage)
    {
        float dieRoll = Random.value;
        return dieRoll < percentage;
    }
    #endregion


    // Game Build Functions
    #region 
    public bool BuildBuilding(TileType tile)
    {
        bool built = false;
        
        switch (tile)
        {
            case TileType.Cottage :
                built = (grain >= CottageData.grainCost) && (lumber >= CottageData.lumberCost) && (sterling >= CottageData.sterlingCost);
                if (built) { cottageCount++; grain -= CottageData.grainCost; lumber -= CottageData.lumberCost; sterling -= CottageData.sterlingCost; }
                break;
            case TileType.Farm :
                built = (grain >= FarmData.grainCost) && (lumber >= FarmData.lumberCost) && (sterling >= FarmData.sterlingCost);
                if (built) { farmCount++; grain -= FarmData.grainCost; lumber -= FarmData.lumberCost; sterling -= FarmData.sterlingCost; }
                break;
            case TileType.Mill :
                built = (grain >= MillData.grainCost) && (lumber >= MillData.lumberCost) && (sterling >= MillData.sterlingCost);
                if (built) { millCount++; grain -= MillData.grainCost; lumber -= MillData.lumberCost; sterling -= MillData.sterlingCost; }
                break;
            case TileType.Market :
                built = (grain >= MarketData.grainCost) && (lumber >= MarketData.lumberCost) && (sterling >= MarketData.sterlingCost);
                if (built) { marketCount++; grain -= MarketData.grainCost; lumber -= MarketData.lumberCost; sterling -= MarketData.sterlingCost; }
                break;
        }

        return built;
    }
    #endregion
}
