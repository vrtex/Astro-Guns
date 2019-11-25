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

	private AudioSource audioSource	= null;

	[Header("OnOff")]
	public bool         soundsOn	= true;
	public bool         musicOn     = true;

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

		audioSource = GetComponent<AudioSource>();

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
		if(musicOn) PlayMusic();
	}

	public void PlayMusic()
	{
		audioSource.Play();
	}

	public void StopMusic()
	{
		audioSource.Stop();
	}

	public void SwitchMusic()
	{
		musicOn = !musicOn;
		if(musicOn) audioSource.Play();
		else audioSource.Stop();
	}

	public void SwitchSound()
	{
		soundsOn = !soundsOn;
	}

	public void Play(string name)
    {
		if(!soundsOn) return;

        Sound sound = sounds.Find(Sound => Sound.name == name);
        if(sound == null)
        {
            Debug.LogWarning("Don't find sound named \"" + name + "\"");
            return;
        }
        sound.source.Play();
    }
}
