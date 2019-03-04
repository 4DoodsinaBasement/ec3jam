using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Allocator : MonoBehaviour
{
    public GameMaster gameMaster;
    public TextMeshProUGUI Population;
    public TextMeshProUGUI Grain;
    public TextMeshProUGUI Lumber;
    public TextMeshProUGUI Sterling;
    public TextMeshProUGUI seasonLabel;
    public GameObject progressBarObject;
    float progressBarStartPosition;
    float progressBarStartWidth;
    public float percent;

    void Start()
    {
        progressBarStartPosition = progressBarObject.transform.position.x;
        progressBarStartWidth = progressBarObject.transform.localScale.x;
    }

    void Update()
    {
        Population.text = gameMaster.population.ToString() + "/" + gameMaster.populationCap.ToString();
        Grain.text = gameMaster.grain.ToString();
        Lumber.text = gameMaster.lumber.ToString();
        Sterling.text = gameMaster.sterling.ToString();
        seasonLabel.text = gameMaster.currentSeason.seasonString;

        UpdateProgressBar();
    }

    void UpdateProgressBar()
    {
        percent = ((gameMaster.currentTickCount * 6.0f) + gameMaster.currentTickTime) / (gameMaster.totalTurnTime - gameMaster.totalTickTime);
        if (percent > 1.0f) { percent = 1.0f; }

        Vector3 tempScale = progressBarObject.transform.localScale;
        tempScale.x = percent;
        progressBarObject.transform.localScale = tempScale;

        Vector3 tempPos = progressBarObject.transform.position;
        tempPos.x = progressBarStartPosition + (1.4f * (percent * Mathf.Abs(progressBarStartPosition)));
        progressBarObject.transform.position = tempPos;

        // progressBarStartPosition + (1.2f * (percent * Mathf.Abs(progressBarStartPosition)));
    }
}
