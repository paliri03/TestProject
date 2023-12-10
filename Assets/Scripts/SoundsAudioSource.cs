using UnityEngine;

public class SoundsAudioSource : MonoBehaviour
{
    static public AudioSource Instance;

    private void Start()
    {
        Instance = GetComponent<AudioSource>();
    }
}
