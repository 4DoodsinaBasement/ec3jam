using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ReportManager : MonoBehaviour
{

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;


    
    Queue<string> reports = new Queue<string>();
    public int maxDisplayedReports = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNotification(string message)
    {
        if (reports.Count < maxDisplayedReports)
        {
            reports.Enqueue(message);
        }
        else
        {
            reports.Enqueue(message);
            reports.Dequeue();
        }

        text3.text = "";

        foreach (string report in reports)
        {
            text3.text += "\n" + report;
        }
    }

    public void clearQue()
    {
        reports.Clear();
    }


    public void SeasonUpdate(string seasonText, string encounterText)
    {
        text1.text = seasonText;
        text2.text = encounterText;
        
    }
}
