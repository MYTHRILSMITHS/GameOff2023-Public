using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] Sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.loop = s.Loop;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
        }
    }

    private Sound SetupSound(string sound, float velocity=0)
    {
        Sound s = Array.Find(Sounds, item => item.Name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return s;
        }
        if (s.EnableVariance)
        {
            s.Source.volume = s.Volume + UnityEngine.Random.Range(-s.VolumeVariance, s.VolumeVariance);
            s.Source.pitch = s.Pitch + UnityEngine.Random.Range(-s.PitchVariance, s.PitchVariance);
        }
        if (s.EnableVelocity)
        {
            s.Source.volume = s.Volume + velocity;
            //s.Source.pitch = s.Pitch + velocity;
        }
        return s;
    }

    /// <summary>
    /// Plays a sound but can be overwritten if called again. Good for music.
    /// </summary>
    public void Play(string sound)
    {
        Sound s = SetupSound(sound);
        if (s != null)
        {
            s.Source.Play();
        }
    }
    public void SafePlay(string sound)
    {
        Sound s = SetupSound(sound);
        if (s != null && !s.Source.isPlaying)
        {
            s.Source.Play();
        }
    }

    public void Stop(string sound)
    {
        Sound s = SetupSound(sound);
        if (s != null)
        {
            s.Source.Stop();
        }
    }

    /// <summary>
    /// Plays a sound but will stack sounds if called again. Good for effects.
    /// </summary>
    public void PlayOneShot(string sound, float velocity=0)
    {
        Sound s = SetupSound(sound, velocity);
            
        if (s != null)
        {
            s.Source.PlayOneShot(s.Clip);
        }

    }

    /// <summary>
    /// This one used for ui - quick hack
    /// </summary>
    public void PlayOneShot(string sound)
    {
        Sound s = SetupSound(sound, 0);

        if (s != null)
        {
            s.Source.PlayOneShot(s.Clip);
        }

    }
}
