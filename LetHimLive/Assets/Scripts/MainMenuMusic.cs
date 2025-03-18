using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource[] audioSources;

    private int lastPlayedIndex = -1;

    void Start()
    {
        if (audioSources.Length == 0)
        {
            Debug.LogError("No AudioSources assigned to the RandomSongPlayer script!");
            return;
        }

        PlayRandomSong();
    }

    void Update()
    {
        bool isPlaying = false;
        foreach (var source in audioSources)
        {
            if (source.isPlaying)
            {
                isPlaying = true;
                break;
            }
        }

        if (!isPlaying && audioSources.Length > 0)
        {
            PlayRandomSong();
        }
    }

    void PlayRandomSong()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, audioSources.Length);
        } while (randomIndex == lastPlayedIndex && audioSources.Length > 1);

        lastPlayedIndex = randomIndex;

        audioSources[randomIndex].Play();
    }
}
