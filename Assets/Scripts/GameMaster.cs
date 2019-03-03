using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonType {Spring = 0, Summer = 1, Autumn = 2, Winter = 3};
public enum TileType {Borderless = -1, Field = 0, Cottage = 1, Farm = 2, Mill = 3, Market = 4, Fort = 5};


public class GameMaster : MonoBehaviour
{
    // Global Variables
    #region
    // Data
    public GameObject activeGridObject;
    public TileObject[] activeGrid;
    public GameObject passiveGridObject;
    public TileObject[] passiveGrid;
    public SeasonData[] seasonData;
    public NotificationManager notifyTray;
    
    // Game Settings
    bool gameOver = false;
    const int ESTABLISH_YEAR = 1587;
    int maxTurns = 12; // number of maxiumn turns
    public int currentTurns = 0;
    float totalTurnTime = 60;
    public float currentTurnTime = 0;
    float totalTickTime = 6;
    public float currentTickTime = 0;
    public SeasonData currentSeason;
    int currentSeasonIndex = 0;

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
    float buildRate = 1.0f;
    float starveChance = 0.5f;
    float exposeChance = 0.25f;
    float fireChance = 0.1f;

    // Encounter Bonuses
    int populationEncounterBonus = 0;
    int grainEncounterBonus = 0;
    int lumberEncounterBonus = 0;
    int sterlingEncounterBonus = 0;

    // Season Report
    public int report_populationIncrease = 0;
    public int report_grainIncrease = 0;
    public int report_sterlingIncrease = 0;
    public int report_upkeepCost_grain = 0;
    public int report_upkeepCost_lumber = 0;
    public int report_upkeepCost_sterling = 0;
    public int report_grainSpentOnPop = 0;
    public int report_starvationDeaths = 0;

    // Tick Notification
    public int notify_exposureDeaths = 0;
    public int notify_lumberIncrease = 0;
    #endregion


    void Start()
    {
        currentSeason = seasonData[0];
        notifyTray.AddNotification("Welcome to Roanoke!");
        notifyTray.AddNotification(currentSeason.seasonString + " of year " + (ESTABLISH_YEAR + (currentTurns / 4)));
        
        LoadTiles();
    }
    void Update() { }


    // Tile Functions
    #region
    void LoadTiles()
    {
        activeGrid = activeGridObject.GetComponentsInChildren<TileObject>();
        passiveGrid = passiveGridObject.GetComponentsInChildren<TileObject>();

        foreach (TileObject tile in activeGrid) 
        { 
            tile.tileType = TileType.Field; 
            tile.seasonType = currentSeason.seasonType;
            // tile.NewTint();
        }
        foreach (TileObject tile in passiveGrid)
        {
            tile.tileType = TileType.Borderless;
            tile.seasonType = currentSeason.seasonType;
            // tile.NewTint();
        }
    }
    void UpdateTiles()
    {
        foreach (TileObject tile in activeGrid) { tile.seasonType = currentSeason.seasonType; }
        foreach (TileObject tile in passiveGrid) { tile.seasonType = currentSeason.seasonType; }
    }
    #endregion


    // Game Season Functions
    #region
    public void SeasonUpkeep()
    {
        NextSeason();
        ZeroReports();
        PopulationIncrease();
        CollectGrain();
        CollectSterling();
        UpkeepCost();
        PopulationEat();
        CheckSeasonEncounter();
        CheckGameOver();
    } 
    void NextSeason()
    {
        if (currentSeasonIndex == 3) { currentSeasonIndex = 0; } else { currentSeasonIndex++; }
        currentSeason = seasonData[currentSeasonIndex];
        currentTurns++;
        notifyTray.AddNotification(currentSeason.seasonString + " of year " + (ESTABLISH_YEAR + (currentTurns / 4)));
        UpdateTiles();
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
    void PopulationIncrease()
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
        sterlingEncounterBonus = 0;
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
    void CheckSeasonEncounter()
    {
        // TODO: Build Encounters
    }
    #endregion


    // Game Tick Functions
    #region 
    public void GameTick()
    {
        ZeroNotifications();
        PopulationExposure();
        CollectLumber();
        CheckFire();
        CheckTickEncounter();
        CheckGameOver();
    }
    void ZeroNotifications()
    {
        notify_exposureDeaths = 0;
        notify_lumberIncrease = 0;
    }
    void PopulationExposure()
    {
        if (population > populationCap) // the bad stuff
        {
            for (int exposedPopulation = population - populationCap; exposedPopulation > 0; exposedPopulation--)
            {
                if (PercentChance(exposeChance * currentSeason.ExposeMod)) { notify_exposureDeaths++; }
            }
            if (notify_exposureDeaths > 0) { notifyTray.AddNotification(((notify_exposureDeaths < population) ? notify_exposureDeaths : population) + " people died of exposure."); }
        }
        population -= (notify_exposureDeaths < population) ? notify_exposureDeaths : population;
    }
    void CollectLumber()
    {
        notify_lumberIncrease += (int)(millCount * MillData.lumberIncrease * currentSeason.LumberMod) + lumberEncounterBonus;
        lumberEncounterBonus = 0;
        if (notify_lumberIncrease > 0) { notifyTray.AddNotification("You gained " + notify_lumberIncrease + " lumber!"); }
        lumber += notify_lumberIncrease;
    }
    void CheckFire()
    {
        List<TileType> buildingsBuilt = new List<TileType>();
        for (int i = 0; i < cottageCount; i++) { buildingsBuilt.Add(TileType.Cottage); }
        for (int i = 0; i < farmCount; i++) { buildingsBuilt.Add(TileType.Farm); }
        for (int i = 0; i < millCount; i++) { buildingsBuilt.Add(TileType.Mill); }
        for (int i = 0; i < marketCount; i++) { buildingsBuilt.Add(TileType.Market); }

        foreach(TileType building in buildingsBuilt)
        {
            if (PercentChance(fireChance * currentSeason.FireMod))
            {
                switch (building)
                {
                    case TileType.Cottage :
                        cottageCount--;
                        break;
                    case TileType.Farm :
                        farmCount--;
                        break;
                    case TileType.Mill :
                        millCount--;
                        break;
                    case TileType.Market :
                        marketCount--;
                        break;
                }
                foreach (TileObject tile in activeGrid) { if(tile.tileType == building) { tile.tileType = TileType.Field; break; } }

                notifyTray.AddNotification("A " + building.ToString() + " burned down!");
            }
        }

        UpdatePopCap();
    }
    void CheckTickEncounter()
    {
        // TODO: Build Encounters
    }
    #endregion


    // Game Mechanical Functions
    #region 
    public bool BuildBuilding(TileType tile)
    {
        bool built = false;
        bool enoughSpace = false;
        foreach (TileObject t in activeGrid) { if (t.tileType == TileType.Field) { enoughSpace = true; } }

        if (enoughSpace == false) { notifyTray.AddNotification("Not enough building space."); }
        
        switch (tile)
        {
            case TileType.Cottage :
                built = enoughSpace && ((grain >= CottageData.grainCost) && (lumber >= CottageData.lumberCost) && (sterling >= CottageData.sterlingCost));
                if (built && enoughSpace)
                { 
                    cottageCount++;
                    grain -= CottageData.grainCost;
                    lumber -= CottageData.lumberCost;
                    sterling -= CottageData.sterlingCost;
                }
                break;
            case TileType.Farm :
                built = enoughSpace && ((grain >= FarmData.grainCost) && (lumber >= FarmData.lumberCost) && (sterling >= FarmData.sterlingCost));
                if (built && enoughSpace)
                { 
                    farmCount++;
                    grain -= FarmData.grainCost; 
                    lumber -= FarmData.lumberCost; 
                    sterling -= FarmData.sterlingCost;
                }
                break;
            case TileType.Mill :
                built = enoughSpace && ((grain >= MillData.grainCost) && (lumber >= MillData.lumberCost) && (sterling >= MillData.sterlingCost));
                if (built && enoughSpace)
                {
                    millCount++; 
                    grain -= MillData.grainCost; 
                    lumber -= MillData.lumberCost; 
                    sterling -= MillData.sterlingCost;
                }
                break;
            case TileType.Market :
                built = enoughSpace && ((grain >= MarketData.grainCost) && (lumber >= MarketData.lumberCost) && (sterling >= MarketData.sterlingCost));
                if (built && enoughSpace)
                {
                    marketCount++;
                    grain -= MarketData.grainCost;
                    lumber -= MarketData.lumberCost;
                    sterling -= MarketData.sterlingCost;
                }
                break;
        }

        if (built)
        {
            int randomTile;
            do { randomTile = Random.Range(0,activeGrid.Length); }
            while (activeGrid[randomTile].tileType != TileType.Field);
            activeGrid[randomTile].tileType = tile;
            UpdateTiles();
        }

        UpdatePopCap();
        return built;
    }
    void UpdatePopCap()
    {
        populationCap = 0;
        populationCap += cottageCount * CottageData.populationCapIncrease;
        populationCap += farmCount * FarmData.populationCapIncrease;
        populationCap += millCount * MillData.populationCapIncrease;
        populationCap += marketCount * MarketData.populationCapIncrease;
    }
    bool PercentChance(float percentage)
    {
        float dieRoll = Random.value;
        return dieRoll < percentage;
    }
    void CheckGameOver()
    {
        gameOver = population <= 0;
        if (gameOver)
        {
            notifyTray.AddNotification("GAME OVER");
        }
    }
    #endregion
}
