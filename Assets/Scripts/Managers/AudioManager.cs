using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static AudioManager Instance {
        get {
            if (instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            }
            return instance;
        }
    }

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        musicSource.clip = s.clip[0];
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        //int index = (s.clip.Length > 1) ? UnityEngine.Random.Range(0,s.clip.Length - 1) : 0;
        int index = UnityEngine.Random.Range(0, s.clip.Length);
        sfxSource.PlayOneShot(s.clip[index]);
    }  
    private void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }
}
[System.Serializable]
public struct Sound
{
    public string name; 
    public AudioClip[] clip;
}
