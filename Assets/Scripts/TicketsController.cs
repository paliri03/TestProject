using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TicketsController : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> ticketsTexts;

    public static int ticketsCount 
    {
        get
        {
            return PlayerPrefs.GetInt("TicketsCount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("TicketsCount", value);
            onTicketsCountChanged.Invoke(value);
        }
    }
    public static Action<int> onTicketsCountChanged;

    private void Start()
    {
        UpdateTickectsText(ticketsCount);
        onTicketsCountChanged += UpdateTickectsText;
    }

    private void UpdateTickectsText(int count)
    {
        foreach (var text in ticketsTexts)
            text.text = count.ToString();
    }
}
