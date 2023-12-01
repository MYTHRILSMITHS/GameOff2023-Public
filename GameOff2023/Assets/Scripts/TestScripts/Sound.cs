using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sound
{

    [Header("Sound Settings")]
    public string Name;
    public AudioClip Clip;
    public bool Loop = false;
    [Range(0f, 1f)]
    public float Volume = .75f;
    [Range(.1f, 3f)]
    public float Pitch = 1f;

    [Header("Random Variance +/- The Value")]
    public bool EnableVariance = false;
    [Range(0f, 1f)]
    public float VolumeVariance = 0f;
    [Range(0f, 1f)]
    public float PitchVariance = 0f;

    public bool EnableVelocity = false;

    [HideInInspector]
    public AudioSource Source;

}
