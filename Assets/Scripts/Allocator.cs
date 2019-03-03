using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Allocator : MonoBehaviour
{
    public GameMaster gameMaster;
    public Text Population;
    public Text Grain;
    public Text Lumber;
    public Text Sterling;
    void Start()
    {

    }

    void Update()
    {
        Population.text = gameMaster.population.ToString();
        Grain.text = gameMaster.grain.ToString();
        Lumber.text = gameMaster.lumber.ToString();
        Sterling.text = gameMaster.sterling.ToString();
    }
}
