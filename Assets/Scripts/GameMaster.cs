using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonType {Spring = 0, Summer = 1, Autumn = 2, Winter = 3};
public enum TileType {Rocks1 = -2, Rocks2 = -1, Field = 0, Cottage = 1, Farm = 2, Mill = 3, Market = 4, Fort = 5};


public class GameMaster : MonoBehaviour
{
    // Global Variables
    #region

    // Data
    public TransitionHandler transitionHandler;
    public NotificationManager notifyTray;
    public GameObject activeGridObject;
    public TileObject[] activeGrid;
    public GameObject passiveGridObject;
    public TileObject[] passiveGrid;
    public SeasonData[] seasonData;

    // Music Settings
    public MusicManager music;
    public List<float> turnMusicDown = new List<float>();
    int musicFadeSteps = 100;
    int musicCounter = 0;
    public bool fadeOutMusic = false;
    
    // Game Settings
    bool gameOver = false;
    const int ESTABLISH_YEAR = 1587;
    public SeasonData currentSeason;
    int currentSeasonIndex = 0;

    // Time Settings
    int maxTurns = 12; // number of maxiumn turns
    public int currentTurns = 0;
    public float totalTurnTime = 60;
    public int totalTickCount = 10;
    public float totalTickTime = 6;
    public float currentTickCount = 0;
    public float currentTickTime = 0;
    int counter = 0;

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
    public float exposeChance = 0.25f;
    public float fireChance = 0.005f;
    float fireChanceOriginal;

    // Encounter Bonuses
    public float SeasonalEncounterChance = 0.4f;
    public float tickEncounterChance = 0.2f;
    int populationEncounterBonus = 0;
    public int grainEncounterBonus = 0;
    int lumberEncounterBonus = 0;
    int sterlingEncounterBonus = 0;
    string encounterText;
    public EncounterData[] encounterData;

    // Season Report
    public int report_populationIncrease = 0;
    public int report_grainIncrease = 0;
    public int report_sterlingIncrease = 0;
    public int report_upkeepCost_grain = 0;
    public int report_upkeepCost_lumber = 0;
    public int report_upkeepCost_sterling = 0;
    public int report_grainSpentOnPop = 0;
    public int report_starvationDeaths = 0;
    public ReportManager reportMan;

    // Tick Notification
    public int notify_exposureDeaths = 0;
    public int notify_lumberIncrease = 0;
    #endregion


    // Mono Functions
    void Start()
    {
        currentSeason = seasonData[0];
        fireChanceOriginal = fireChance;

        LoadTiles();

        for (int i = 0; i < musicFadeSteps; i++)
        { turnMusicDown.Add(1.0f - ((1.0f / musicFadeSteps) * i)); }
        turnMusicDown.Add(0.0f);
        music.SetTrack(SeasonType.Spring);
    }
    void FixedUpdate()
    {
        foreach (TileObject tile in activeGrid)
        {
            if (tile.underConstruction)
            {
                tile.buildProgress += 0.05f;

                if (tile.buildProgress >= tile.buildTarget) // Building finishes building
                {
                    tile.underConstruction = false;
                    tile.buildProgress = 0;
                    tile.finishedType = tile.startType;

                    switch (tile.finishedType)
                    {
                        case TileType.Cottage: cottageCount++; break;
                        case TileType.Farm: farmCount++; break;
                        case TileType.Mill: millCount++; break;
                        case TileType.Market: marketCount++; break;
                    }
                    UpdatePopCap();
                }
            }
        }

        currentTickTime += 0.05f;
        if (currentTickTime >= totalTickTime)
        {
            currentTickTime = 0; currentTickCount++;
            if (currentTickCount < totalTickCount)
            {
                GameTick();
                Debug.Log("Game Tick");
            }
            else
            {
                currentTickCount = 0;
                UpdateTiles();
                notifyTray.AddNotification(currentSeason.seasonString + " of year " + (ESTABLISH_YEAR + (currentTurns / 4)));
                Debug.Log("Season Upkeep");
            }


            if (currentTickCount == 9) {
                SeasonUpkeep();
                transitionHandler.fadeOut = false;
                transitionHandler.fadeIn = true;
                fadeOutMusic = true;
            }
            if (currentTickCount == 0) {
                transitionHandler.fadeIn = false;
                transitionHandler.fadeOut = true;
                music.SetTrack(currentSeason.seasonType);
            }
        }

        if (fadeOutMusic)
        {
            music.SetVolume(turnMusicDown[musicCounter++]);
            if (musicCounter == turnMusicDown.Count - 1)
            {
                music.SetVolume(0.0f);
                musicCounter = 0;
                fadeOutMusic = false;
            }
        }
    }


    // Tile Functions
    #region
    void LoadTiles()
    {
        activeGrid = activeGridObject.GetComponentsInChildren<TileObject>();
        passiveGrid = passiveGridObject.GetComponentsInChildren<TileObject>();

        foreach (TileObject tile in activeGrid)
        {
            tile.startType = TileType.Field;
            tile.finishedType = TileType.Field;
            tile.seasonType = currentSeason.seasonType;
        }
        foreach (TileObject tile in passiveGrid)
        {
            if (PercentChance(0.15f))
            {
                if (PercentChance(0.5f))
                {
                    tile.startType = TileType.Rocks1;
                    tile.finishedType = TileType.Rocks1;
                }
                else
                {
                    tile.startType = TileType.Rocks2;
                    tile.finishedType = TileType.Rocks2;
                }
            }
            else
            {
                tile.startType = TileType.Field;
                tile.finishedType = TileType.Field;
            }
            tile.seasonType = currentSeason.seasonType;
            tile.SetColor(new Color(0.9f, 0.9f, 0.9f));
        }
    }
    void UpdateTiles()
    {
        foreach (TileObject tile in activeGrid) { tile.seasonType = currentSeason.seasonType; }
        foreach (TileObject tile in passiveGrid)
        {
            tile.seasonType = currentSeason.seasonType;

            switch(tile.seasonType)
            {
                case SeasonType.Spring : tile.SetColor(new Color(0.9f, 0.9f, 0.9f, 1.0f)); break;
                case SeasonType.Summer : tile.SetColor(new Color(0.85f, 0.85f, 0.85f, 1.0f)); break;
                case SeasonType.Autumn : tile.SetColor(new Color(0.92f, 0.92f, 0.92f, 1.0f)); break;
                case SeasonType.Winter : tile.SetColor(new Color(0.82f, 0.82f, 0.82f, 1.0f)); break;
            }
        }
    }
    #endregion


    // Game Season Functions
    public void SeasonUpkeep()
    {
        NextSeason();
        ZeroReports();
        CheckSeasonEncounter();
        PopulationIncrease();
        CollectGrain();
        CollectSterling();
        UpkeepCost();
        PopulationEat();
        CheckGameOver();
        UpdateSeasonalUi();


    }
    #region
    void NextSeason()
    {
        if (currentSeasonIndex == 3) { currentSeasonIndex = 0; } else { currentSeasonIndex++; }
        currentSeason = seasonData[currentSeasonIndex];
        currentTurns++;
        
       
    }
    void ZeroReports()
    {
        fireChance = fireChanceOriginal;
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
        if (PercentChance(SeasonalEncounterChance))
        {
            EncounterData currentEncounter = EncounterFinder(EncounterType.Seasonal);
            if(!(currentEncounter is null)){ ExecuteEncounter(currentEncounter);}
            
        }
    }

    void ExecuteEncounter(EncounterData encoData)
    {
        int value1 = 0;
        int value2 = 0; 
        int value3 = 0;
        int sign;

        if (encoData.activateEffect1)
        {
            sign = (encoData.signAddEffect1) ? 1 : -1;

            switch (encoData.equationEffect1)
            {
                case EffectEquation.percentage:
                    switch (encoData.typeEffect1)
                    { 

                        case EffectType.FarmScaler:
                            grainEncounterBonus *= sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) / 100);
                            break;
                        case EffectType.FireChance:
                            fireChance *= sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) / 100);
                            break;
                        case EffectType.Sterling:
                            value1 = (sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) / 100)) * sterling;
                            sterling += value1;
                            break;
                        case EffectType.Lumber:
                            value1 = (sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) / 100)) * lumber;
                            lumber += value1;
                            break;
                        case EffectType.Grain:
                            value1 = (sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) / 100)) * grain;
                            grain += value1;
                            break;
                        case EffectType.Population:
                            value1 = (sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) / 100)) * population;
                            population += value1; 
                            break;
                    }
                    break;
                case EffectEquation.standard:

                    Debug.Log(encoData.value1Effect1 + " " + encoData.value2Effect1);


                    switch (encoData.typeEffect1)
                    {
                           case EffectType.FarmScaler:
                               
                               value1 = (Random.Range(encoData.value1Effect1, encoData.value2Effect1)) ;
                               grainEncounterBonus += (sign * value1);
                               break;
                           case EffectType.FireChance:
                               fireChance += sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) );
                               break;
                           case EffectType.Sterling:
                               value1 = sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1));
                               sterlingEncounterBonus += value1;
                               break;
                           case EffectType.Lumber:
                                value1 = sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1) );
                                lumber += value1;
                               break;
                           case EffectType.Grain:
                                value1 = sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1));
                                grain += value1;
                               break;
                           case EffectType.Population:
                                value1 = sign * (Random.Range(encoData.value1Effect1, encoData.value2Effect1));
                                population += value1;
                            break;
                    }                   
                    break;

            }




        }



        encounterText = string.Format(encoData.OutputText, Mathf.Abs(value1), Mathf.Abs(value2), Mathf.Abs(value3));



    }


    EncounterData EncounterFinder(EncounterType enType)
    {
        List<EncounterData> encounterIndex = new List<EncounterData>();

        foreach (EncounterData encount in encounterData)
        {
            if (encount.encounterType == enType)
            {
                foreach (Year years in encount.AvailableYears)
                {
                    if ((int)years == (1+ (currentTurns / 4)))
                    {
                        foreach (SeasonType seasons in encount.AvailableSeasons)
                        {
                            if (seasons == currentSeason.seasonType)
                            {
                                encounterIndex.Add(encount);
                            }
                        }
                    }
                }
            }
        }

        if (encounterIndex.Count > 0)
        {
            return encounterIndex[Random.Range(0,encounterIndex.Count)];
        }
        return null;
    }


    void UpdateSeasonalUi()
    {
        reportMan.clearQue();

        int shelteredPop = (population > populationCap) ? populationCap : population;


        //reportMan.AddNotification("Current Sheltered Poputation: " + shelteredPop);
        reportMan.AddNotification("Births: " + report_populationIncrease);
        if (currentSeason.seasonType == SeasonType.Autumn) { reportMan.AddNotification("Harvested: " + report_grainIncrease + " Grain"); }
        reportMan.AddNotification("Sterling Increase: " + report_sterlingIncrease);
        reportMan.AddNotification("Seasonal Maintenance:    Grain: " + report_upkeepCost_grain+ "   Lumber: " + report_upkeepCost_lumber +"   Sterling: " + report_upkeepCost_sterling);
        reportMan.AddNotification("Grain Eaten: " + report_grainSpentOnPop);
        reportMan.AddNotification(report_starvationDeaths+ " died of starvation");
        reportMan.SeasonUpdate(currentSeason.name, encounterText);

        encounterText = "";
    }
    #endregion


    // Game Tick Functions
    public void GameTick()
    {
        ZeroNotifications();
        PopulationExposure();
        CollectLumber();
        CheckFire();
        CheckTickEncounter();
        CheckGameOver();
    }
    #region 
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
        if (notify_lumberIncrease > 0)
        {
            //notifyTray.AddNotification("You gained " + notify_lumberIncrease + " lumber!");
        }
        lumber += notify_lumberIncrease;
    }
    void CheckFire()
    {
        foreach (TileObject tile in activeGrid)
        {
            if (tile.finishedType == TileType.Cottage || 
                tile.finishedType == TileType.Farm || 
                tile.finishedType == TileType.Mill || 
                tile.finishedType == TileType.Market)
            {
                if (PercentChance(fireChance * currentSeason.FireMod))
                {
                    notifyTray.AddSoundNotification("A " + tile.finishedType.ToString() + " burned down!");

                    tile.finishedType = TileType.Field;
                    tile.startType = TileType.Field;
                }
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
        bool buildingApproved = false;
        bool enoughSpace = false;
        foreach (TileObject t in activeGrid) { if (t.startType == TileType.Field) { enoughSpace = true; } }

        if (enoughSpace == false) { notifyTray.AddNotification("Not enough building space."); }
        
        switch (tile)
        {
            case TileType.Cottage :
                buildingApproved = enoughSpace && ((grain >= CottageData.grainCost) && (lumber >= CottageData.lumberCost) && (sterling >= CottageData.sterlingCost));
                if (buildingApproved && enoughSpace)
                { 
                    buildingsConstructing++; // cottageCount++;
                    grain -= CottageData.grainCost;
                    lumber -= CottageData.lumberCost;
                    sterling -= CottageData.sterlingCost;
                }
                break;
            case TileType.Farm :
                buildingApproved = enoughSpace && ((grain >= FarmData.grainCost) && (lumber >= FarmData.lumberCost) && (sterling >= FarmData.sterlingCost));
                if (buildingApproved && enoughSpace)
                { 
                    buildingsConstructing++; // farmCount++;
                    grain -= FarmData.grainCost; 
                    lumber -= FarmData.lumberCost; 
                    sterling -= FarmData.sterlingCost;
                }
                break;
            case TileType.Mill :
                buildingApproved = enoughSpace && ((grain >= MillData.grainCost) && (lumber >= MillData.lumberCost) && (sterling >= MillData.sterlingCost));
                if (buildingApproved && enoughSpace)
                {
                    buildingsConstructing++; // millCount++; 
                    grain -= MillData.grainCost; 
                    lumber -= MillData.lumberCost; 
                    sterling -= MillData.sterlingCost;
                }
                break;
            case TileType.Market :
                buildingApproved = enoughSpace && ((grain >= MarketData.grainCost) && (lumber >= MarketData.lumberCost) && (sterling >= MarketData.sterlingCost));
                if (buildingApproved && enoughSpace)
                {
                    buildingsConstructing++; // marketCount++;
                    grain -= MarketData.grainCost;
                    lumber -= MarketData.lumberCost;
                    sterling -= MarketData.sterlingCost;
                }
                break;
        }

        if (buildingApproved)
        {
            int randomTile;
            do { randomTile = Random.Range(0,activeGrid.Length); }
            while (activeGrid[randomTile].startType != TileType.Field);

            activeGrid[randomTile].startType = tile;
            switch (tile)
            {
                case TileType.Cottage : 
                    activeGrid[randomTile].buildTarget = CottageData.buildTime;
                    break;
                case TileType.Farm : 
                    activeGrid[randomTile].buildTarget = FarmData.buildTime;
                    break;
                case TileType.Mill : 
                    activeGrid[randomTile].buildTarget = MillData.buildTime;
                    break;
                case TileType.Market : 
                    activeGrid[randomTile].buildTarget = MarketData.buildTime;
                    break;
            }
            activeGrid[randomTile].underConstruction = true;

            UpdateTiles();
        }

        UpdatePopCap();
        return buildingApproved;
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
