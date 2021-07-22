using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//Audio manager class
public class AudioManager : MonoBehaviour
{
	public Sound sounds;
	public static AudioManager instance;
	void Awake()
	{

		/*		if (instance == null)
				{
					instance = this;
				}
				else
				{
					Destroy(gameObject);
					return;
				}

				DontDestroyOnLoad(gameObject);
		*/
		sounds.source = gameObject.AddComponent<AudioSource>();
		sounds.source.clip = sounds.clip;
		sounds.source.volume = sounds.volumne;
		sounds.source.pitch = sounds.pitch;
		sounds.source.loop = sounds.loop;
	}

	public void SetVolume(float volume)
    {
		sounds.source.volume = volume;
    }

	void Start()
	{
		Play();
	}

	public void Play()
	{

		sounds.source.Play();
	}

	public void Stop(string name)
	{
		sounds.source.Stop();
	}

	public void playSoundOnEvent()
	{
		//play a sound when something happened
	}
}
