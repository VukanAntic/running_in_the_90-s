using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private Sound previous_sound = null;


    void Awake()
    {
        if(instance == null)
            instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = true;
        }
    }

    void Start()
    {
        // hteli bi default muziku ovde, ne tape-1
        Play("Tape-1");
    }


    public void Play(string name)
    {
        if (previous_sound != null)
            previous_sound.source.Stop();

        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound" + name +  "not found!");
            return;
        }
        s.source.Play();
        previous_sound = s;
        
    }
}
