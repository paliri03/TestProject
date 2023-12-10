using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DailyBonusesController : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressText;

    [SerializeField] private Transform bonusesGrid;
    [SerializeField] private Bonus bonusPrefab;
    [SerializeField] private List<int> bonusesList;

    [SerializeField] private TextMeshProUGUI ticketText;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private AudioClip bonusSound;

    [SerializeField] private UnityEvent onBonusCollected;
    private int daysCollected { get {return PlayerPrefs.GetInt("DaysCollected", 0);} set { PlayerPrefs.SetInt("DaysCollected", value); } }
    private bool isCollectedToday 
    {
        get
        {
            if ((DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastDateCollected", "01.01.2023 12:00:00"))).TotalHours >= 24)
                return false;
            else
                return true;
        }
        set 
        {
            PlayerPrefs.SetString("LastDateCollected", DateTime.Now.ToString());
        } 
    }

    private void Start()
    {      
        if(daysCollected == bonusesList.Count) daysCollected = 0;

        SetBonusProgress();

        for (int i = 0; i < bonusesList.Count; i++)
        {
            var bonus = Instantiate(bonusPrefab, bonusesGrid);

            bonus.DayNum = i + 1;
            bonus.TicketsCount = bonusesList[i];

            if (i < daysCollected) 
                bonus.SetCollected();
            else if (i > daysCollected || (i == daysCollected && isCollectedToday)) 
                bonus.SetLocked();
            else 
                bonus.GetComponent<Button>().onClick.AddListener(() => CollectBonus(bonus));
        }
    }

    private void CollectBonus(Bonus bonus)
    {
        SoundsAudioSource.Instance.PlayOneShot(bonusSound, 1);

        isCollectedToday = true;
        TicketsController.ticketsCount += bonus.TicketsCount;
        bonus.SetCollected();

        daysCollected++;
        SetBonusProgress();
        ticketText.text = "X" + bonus.TicketsCount;
        dayText.text = "DAY" + bonus.DayNum;
        
        onBonusCollected.Invoke();
    }

    private void SetBonusProgress()
    {
        progressSlider.value = daysCollected;
        progressText.text = $"{daysCollected}/{bonusesList.Count}";
    }
}
