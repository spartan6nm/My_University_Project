using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;

    public AudioClip Clip;

    [Range(0f,1f)]
    public float volume;

    [Range(0.1f , 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float SpatialBlend = 1f;

    public bool loop;

    public bool playOnAwake;

    

    [HideInInspector]
    public AudioSource source;
}
