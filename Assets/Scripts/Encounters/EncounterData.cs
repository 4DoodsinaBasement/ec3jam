using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncounterType { Seasonal = 0, Tick = 1};
public enum Year { Year1 = 1, Year2 = 2, Year3 = 3 };
public enum EffectType { Population = 0, Grain = 1, Lumber = 2, Sterling = 3, FireChance = 4, FarmScaler = 5, MillScaler = 6, MarketScaler = 7, StarvationScaler = 8, ExposerScaler = 9 }
public enum EffectEquation { standard = 0, percentage = 1, cannibal =2 }

[CreateAssetMenu(menuName = "Encounter Data", fileName = "Encounters", order = 1)]

public class EncounterData : ScriptableObject
{
    //public string name;
    public EncounterType encounterType;
    public SeasonType[] AvailableSeasons;
    public Year[] AvailableYears;
    public int appearanceWeight;
    public string OutputText;

    public bool activateEffect1;
    public EffectType typeEffect1;
    public EffectEquation equationEffect1;
    public bool signAddEffect1;
    public int value1Effect1;
    public int value2Effect1;

    public bool activateEffect2;
    public EffectType typeEffect2;
    public EffectEquation equationEffect2;
    public bool signAddEffect2;
    public int value1Effect2;
    public int value2Effect2;

    public bool activateEffect3;
    public EffectType typeEffect3;
    public EffectEquation equationEffect3;
    public bool signAddEffect3;
    public int value1Effect3;
    public int value2Effect3;


}
