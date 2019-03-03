using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Season Data", fileName = "Season", order = 1)]
public class SeasonData : ScriptableObject
{
    public SeasonType seasonType;

    public float PopulationMod = 1.0f;
    public float LumberMod = 1.0f;
    public float SterlingMod = 1.0f;
    public float BuildMod = 1.0f;
    public float StarveMod = 1.0f;
    public float ExposeMod = 1.0f;
    public float FireMod = 1.0f;

    //public UpkeepEncounter[] upkeepEncounters;
    //public TickEncounter[] tickEncounters;
}