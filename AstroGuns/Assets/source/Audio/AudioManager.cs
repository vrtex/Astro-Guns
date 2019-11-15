using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get => instance; }

    public List<Sound> sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        foreach (Button b in GameObject.FindObjectsOfType<Button>())
        {
            b.onClick.AddListener(() => Play("click"));
        }
    }

    void Start()
    {
        Play("background");
    }

    public void Play(string name)
    {
        Debug.Log("Play");
        Sound sound = sounds.Find(Sound => Sound.name == name);
        if(sound == null)
        {
            Debug.LogWarning("Don't find sound named \"" + name + "\"");
            return;
        }
        sound.source.Play();
    }
}
