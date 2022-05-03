using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

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
        }
    }

    void Start()
    {
        Play("Tape1");
    }


    public void Play(string name)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound" + name +  "not found!");
            return;
        }
        s.source.Play();
    }
}
