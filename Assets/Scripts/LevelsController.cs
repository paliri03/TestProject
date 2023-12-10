using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private List<Transform> levelButtonPositions;

    [SerializeField] private AudioClip sound;

    public static int lastCompletedLevel
    {
        get { return PlayerPrefs.GetInt("LastCompletedLevel", 0); }
        set { PlayerPrefs.SetInt("LastCompletedLevel", value); }
    }
    

    private void Start()
    {
        for(int i = 0; i < levelButtonPositions.Count; i++)
        {
            var button = Instantiate(levelButtonPrefab, levelButtonPositions[i]);
            button.levelText.text = (i + 1).ToString();

            if(i > lastCompletedLevel)
            {
                button.levelText.enabled = false;
                button.GetComponent<Button>().interactable = false;
            }
            else if(i == lastCompletedLevel)
            {
                button.lockImage.SetActive(false);
                button.GetComponent<Button>().onClick.AddListener(() => CompleteLevel(button));
            }
            else
            {
                button.lockImage.SetActive(false);
            }
        }
    }

    private void CompleteLevel(LevelButton button)
    {
        lastCompletedLevel++;
        SoundsAudioSource.Instance.PlayOneShot(sound);
        button.GetComponent<Button>().onClick.RemoveAllListeners();

        var lvlNum = int.Parse(button.levelText.text);
        if(lvlNum < levelButtonPositions.Count)
        {
            var nextLevelButton = levelButtonPositions[lvlNum].GetComponentInChildren<LevelButton>();
            nextLevelButton.levelText.enabled = true;
            nextLevelButton.lockImage.SetActive(false);
            nextLevelButton.GetComponent<Button>().interactable = true;
            nextLevelButton.GetComponent<Button>().onClick.AddListener(() => CompleteLevel(nextLevelButton));
        }
    }
}
