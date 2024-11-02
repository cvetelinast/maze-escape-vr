using UnityEngine;
using UnityEngine.Audio;
using static ColorsGenerator;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioSource collectCoinAudioSource;
    [SerializeField] private AudioSource finishLineAudioSource;
    [SerializeField] private AudioSource backgroundMusicAudioSource;

    [SerializeField] private AudioResource garfieldAudioResource;
    [SerializeField] private AudioResource blueLagoonAudioResource;
    [SerializeField] private AudioResource jungleAudioResource;
    [SerializeField] private AudioResource barbieAudioResource;
    [SerializeField] private AudioResource darthVaderAudioResource;

    public void SetupBackgroundAudioResource(MazeColorScheme mazeColorScheme)
    {
        backgroundMusicAudioSource.resource =
            mazeColorScheme switch
            {
                MazeColorScheme.GARFIELD => garfieldAudioResource,
                MazeColorScheme.BLUE_LAGOON => blueLagoonAudioResource,
                MazeColorScheme.JUNGLE => jungleAudioResource,
                MazeColorScheme.BARBIE => barbieAudioResource,
                MazeColorScheme.DARTH_VADER => darthVaderAudioResource,
                _ => null
            };
    }

    public void PlayAudioCollectCoin()
    {
        if (collectCoinAudioSource != null)
        {
            collectCoinAudioSource.Play();
        }
    }

    public void PlayAudioFinishLine()
    {
        if (finishLineAudioSource != null)
        {
            finishLineAudioSource.Play();
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Play();
        }
    }
}
