using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CottageData
{
    public static string buildingName = "Cottage";
    public static string buildingDescription = "Prevents Exposure";
    // Build Costs
    public static float buildTime = 8.0f;
    public static int grainCost = 0;
    public static int lumberCost = 50;
    public static int sterlingCost = 10;
    // Upkeep Costs
    public static int grainUpkeep = 0;
    public static int lumberUpkeep = 3;
    public static int sterlingUpkeep = 0;
    // Benefits
    public static int populationCapIncrease = 20;
    public static int grainIncrease;
    public static int lumberIncrease;
    public static int sterlingIncrease;
}

public static class FarmData
{
    public static string buildingName = "Farm";
    public static string buildingDescription = "Produces Grain in Autumn";
    // Build Costs
    public static float buildTime = 12.0f;
    public static int grainCost = 10;
    public static int lumberCost = 30;
    public static int sterlingCost = 0;
    // Upkeep Costs
    public static int grainUpkeep = 10; //only applied in spring, check the UpkeepCost function
    public static int lumberUpkeep = 0;
    public static int sterlingUpkeep = 0;
    // Benefits
    public static int populationCapIncrease = 5;
    public static int grainIncrease = 80;
    public static int lumberIncrease;
    public static int sterlingIncrease;
}

public static class MillData
{
    public static string buildingName = "Mill";
    public static string buildingDescription = "Produces Wood";
    // Build Costs
    public static float buildTime = 7.0f;
    public static int grainCost = 10;
    public static int lumberCost = 5;
    public static int sterlingCost = 20;
    // Upkeep Costs
    public static int grainUpkeep = 3;
    public static int lumberUpkeep = 0;
    public static int sterlingUpkeep = 0;
    // Benefits
    public static int populationCapIncrease;
    public static int grainIncrease;
    public static int lumberIncrease = 4;
    public static int sterlingIncrease;
}

public static class MarketData
{
    public static string buildingName = "Market";
    public static string buildingDescription = "Produces Sterling";
    // Build Costs
    public static float buildTime = 9.0f;
    public static int grainCost = 15;
    public static int lumberCost = 50;
    public static int sterlingCost = 5;
    // Upkeep Costs
    public static int grainUpkeep = 0;
    public static int lumberUpkeep = 1;
    public static int sterlingUpkeep = 2;
    // Benefits
    public static int populationCapIncrease;
    public static int grainIncrease;
    public static int lumberIncrease;
    public static int sterlingIncrease = 30;
}

public static class FortData
{
    public static string buildingName = "Fort";
    public static string buildingDescription = "";
    // Build Costs
    public static float buildTime;
    public static int grainCost;
    public static int lumberCost;
    public static int sterlingCost;
    // Upkeep Costs
    public static int grainUpkeep;
    public static int lumberUpkeep;
    public static int sterlingUpkeep;
    // Benefits
    public static int populationCapIncrease;
    public static int grainIncrease;
    public static int lumberIncrease;
    public static int sterlingIncrease;
}