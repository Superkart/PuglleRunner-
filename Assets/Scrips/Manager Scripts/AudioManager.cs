using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BackgroundMusicSource;
    public List<AudioSource> AudioSources;

    private AudioClip sfxSound;

    // Start is called before the first frame update
    private void Start()
    {
        sfxSound = Resources.Load<AudioClip>("AudioFiles/game_sfx");
    }
    public void PlayBackgroundMusic()
    {
        if (BackgroundMusicSource != null)
        {
            BackgroundMusicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (BackgroundMusicSource != null)
        {
            BackgroundMusicSource.Stop();
        }
    }

    public void PlaySoundEffect(string audioName)
    {
       var audioClip = Resources.Load<AudioClip>("AudioFiles/"+audioName);

        if (AudioSources[0] != null)
        {
            AudioSources[0].clip = audioClip;
            AudioSources[0].Play();
        }
    }

    public void PlaySfx()
    {
        if (AudioSources[1] != null)
        {
            AudioSources[1].clip = sfxSound;
            AudioSources[1].Play();
        }
    }
}
