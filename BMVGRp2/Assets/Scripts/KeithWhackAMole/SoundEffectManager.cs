using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }

    public AudioClip playerHitMoleClip; // Assign this in the inspector
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure that there is only one instance of the SoundEffectManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerHitMoleSound()
    {
        if (playerHitMoleClip != null)
        {
            audioSource.PlayOneShot(playerHitMoleClip);
        }
    }
}
