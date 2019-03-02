using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeasonType {Spring, Summer, Autumn, Winter};

[CreateAssetMenu(menuName = "Season", fileName = "Season", order = 1)]
public class SeasonObject : ScriptableObject
{
    public SeasonType seasonType;
}