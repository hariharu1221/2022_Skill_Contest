using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioClip MainBgm;
    [SerializeField] AudioClip InGameBgm;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource fpxSource;

    [SerializeField] List<AudioClip> audioClips;
    Dictionary<string, AudioClip> pairs = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        SetInstance();


        foreach(AudioClip audioClip in audioClips)
        {
            pairs.Add(audioClip.name, audioClip);
        }
    }

    public void PlayFPX(string key ,float volume)
    {
        if (!pairs.ContainsKey(key)) return;

        fpxSource.PlayOneShot(pairs[key], volume);
    }

    public void PlayBGM(float volume)
    {
        bgmSource.volume = volume;

        if (SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            bgmSource.clip = InGameBgm;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            bgmSource.clip = MainBgm;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
}
