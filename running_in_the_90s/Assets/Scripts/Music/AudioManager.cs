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
            if (s.name != "Tape-change")
                s.source.loop = true;
        }
    }

    void Start()
    {
        Play("Tape-1");
    }


    public void Play(string name)
    {
        Sound intro = Array.Find(sounds, sound => sound.name == "Tape-change");
        if (previous_sound != null)
        {
            previous_sound.source.Stop();
            intro.source.Play();
        }
        AudioSource audio = GetComponent<AudioSource>();
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound" + name +  "not found!");
            return;
        }

        if (previous_sound == null)
            s.source.Play();
        else
            s.source.PlayDelayed(intro.clip.length);

        previous_sound = s;
        
    }
}
