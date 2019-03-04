using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationManager : MonoBehaviour
{
    public TextMeshProUGUI tray;
    public AudioSource source;
    Queue<string> notifications = new Queue<string>();
    public int maxDisplayedNotifications = 20;

    void Awake()
    {
        source = GetComponent<AudioSource>();

        notifications.Enqueue("Welcome to Roanoke!");
        notifications.Enqueue("Spring of 1587");
        tray.text = "";
        foreach (string notify in notifications)
        {
            tray.text += "\n" + notify;
        }
    }

    public void AddSoundNotification(string message)
    {
        source.Play();
        AddNotification(message);
    }

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
