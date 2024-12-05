using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    // AudioSource to play the background music
    public AudioSource musicAudioSource;

    // Audio clip for the background music
    public AudioClip musicClip;

    void Start()
    {
        // Check if the AudioSource is assigned, otherwise get it from the game object
        if (musicAudioSource == null)
        {
            musicAudioSource = GetComponent<AudioSource>();
        }

        // Assign the music clip and start playing the music
        if (musicClip != null)
        {
            musicAudioSource.clip = musicClip;
            musicAudioSource.loop = true; // Ensure the music loops
            musicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clip assigned in BackgroundMusicManager.");
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicAudioSource.isPlaying)
        {
            musicAudioSource.Stop();
        }
    }
}
