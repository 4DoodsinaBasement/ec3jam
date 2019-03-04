using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public TextMeshProUGUI tray;
    Queue<string> notifications = new Queue<string>();
    public int maxDisplayedNotifications = 17;

    public void AddNotification(string message)
    {
        if (notifications.Count < maxDisplayedNotifications)
        {
            notifications.Enqueue(message);
        }
        else
        {
            notifications.Enqueue(message);
            notifications.Dequeue();
        }

        tray.text = "";

        foreach (string notify in notifications)
        {
            tray.text += "\n" + notify;
        }
    }
}
