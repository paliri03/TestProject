using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundsButton;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundsSource;

    private bool musicState { 
        get
        { 
            return PlayerPrefs.GetInt("MusicState", 1) == 1 ? true : false;
        }
        set
        {
            if (value)
                PlayerPrefs.SetInt("MusicState", 1);
            else
                PlayerPrefs.SetInt("MusicState", 0);
        }
    }
    private bool soundsState
    {
        get
        {
            return PlayerPrefs.GetInt("SoundsState", 1) == 1 ? true : false;
        }
        set
        {
            if (value)
                PlayerPrefs.SetInt("SoundsState", 1);
            else
                PlayerPrefs.SetInt("SoundsState", 0);
        }
    }

    private void Awake()
    {
        ChangeMusicState(musicState);
        ChangeSoundsState(soundsState);

        musicButton.onClick.AddListener(()=> ChangeMusicState(!musicState));
        soundsButton.onClick.AddListener(() => ChangeSoundsState(!soundsState));
    }

    private void ChangeMusicState(bool state)
    {
        if(state)
            musicSource.Play();
        else
            musicSource.Pause();
        
        musicState = state;
        ChangeButtonState(musicButton, state);
    }

    private void ChangeSoundsState(bool state)
    {
        soundsSource.mute = !state;

        soundsState = state;
        ChangeButtonState(soundsButton, state);
    }

    private void ChangeButtonState(Button button, bool state)
    {
        button.transform.GetChild(0).gameObject.SetActive(!state);
    }
}
