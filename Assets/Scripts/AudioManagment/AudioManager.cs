using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();


    void Awake()
    {

        foreach (Sound sound in sounds)
        {
            soundDictionary.Add(sound.name, sound);

            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.playOnAwake = sound.playOnAwake;

            sound.source.loop = sound.loop;

            sound.source.clip = sound.Clip;

            sound.source.volume = sound.volume;

            sound.source.pitch = sound.pitch;

            
        }
    }


    public void Play(string name)
    {
        soundDictionary[name].source.Play();
    }
}
