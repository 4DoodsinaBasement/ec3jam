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
    void Start()
    {

    }

    void Update()
    {
        Population.text = gameMaster.population.ToString() + "/" + gameMaster.populationCap.ToString();
        Grain.text = gameMaster.grain.ToString();
        Lumber.text = gameMaster.lumber.ToString();
        Sterling.text = gameMaster.sterling.ToString();
    }
}
