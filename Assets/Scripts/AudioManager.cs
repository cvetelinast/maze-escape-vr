using UnityEngine;

public class AudioManager : MonoBehaviour {
    [SerializeField] private AudioSource collectCoinAudioSource;
    [SerializeField] private AudioSource finishLineAudioSource;
    [SerializeField] private AudioSource backgroundMusicAudioSource;

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
