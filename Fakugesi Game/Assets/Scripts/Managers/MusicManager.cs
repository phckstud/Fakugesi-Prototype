using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// All sounds and music in this game come from ZapSplat.com
/// </summary>
public class MusicManager : SingletonPersistent<MusicManager>
{
	[Header("External Scripts")]
    public Sound[] sounds;

    [Header("Audio Sources")]
    public AudioSource BGMusic;
    public AudioSource SoundEffects;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] jump;
    [SerializeField] AudioClip walkOnDryGrass, walkOnSpace, walkOnNormal;

    [Header("Floats")]
    public float audioVolume = 1;

    [Header("Booleans")]
    public bool isPaused;

    [Header("Strings")]
    public string sceneName;
    public string introSceneName = "IntroScene";

   /* void Awake()
    {

        foreach (Sound soundCurrentlyLookingAt in sounds)
        {
            //Set Audio Source
            if (FX.clip != null)
                soundCurrentlyLookingAt.AudioSource = FX.gameObject.AddComponent<AudioSource>();
            else
                soundCurrentlyLookingAt.AudioSource = FX;

            //Mixer
            soundCurrentlyLookingAt.AudioSource.outputAudioMixerGroup = soundCurrentlyLookingAt.mix;

            //clip
            soundCurrentlyLookingAt.AudioSource.clip = soundCurrentlyLookingAt.clip;

            //volume
            soundCurrentlyLookingAt.AudioSource.volume = soundCurrentlyLookingAt.volume;

            //pitch
            soundCurrentlyLookingAt.AudioSource.pitch = soundCurrentlyLookingAt.pitch;

            //loop
            soundCurrentlyLookingAt.AudioSource.loop = soundCurrentlyLookingAt.Loop;

            soundCurrentlyLookingAt.AudioSource.playOnAwake = soundCurrentlyLookingAt.PlayOnAwake;

        }
    }*/

    /// <summary>
    /// looks through the array sounds to find the name of the called sound
    /// </summary>
  

    private void Start()
    {
        //GetPlayerPrefs(audioVolume);
        //SetVolume();

    }
    private void Update()
    {
        //PlayBGSound();
      //  SetVolume();
       // PauseGame();

       /* if (pauseCanvas == null)
        {
            pauseCanvas = GetComponentInChildren<Canvas>();
        }*/

    }
    public void PlayBGSound()
    {
        if (BGMusic == null)
            return;

        if (SceneManager.GetActiveScene().name == sceneName || SceneManager.GetActiveScene().name == introSceneName)
        {
            BGMusic.Stop(); 
            return;
        }
       // int index = UnityEngine.Random.Range(0, bgMusic.Length);
        if (!BGMusic.isPlaying)
        {
            Debug.Log("Not Playing");
            //BGMusic.clip = bgMusic[index];
            BGMusic.Play();
            Debug.Log("Is Playing");
        }

    }

    public void PlayJumpClip()
	{
        int index = UnityEngine.Random.Range(0, jump.Length);
        SoundEffects.clip = jump[index];
        SoundEffects.Play();
    }

    public void PlayDryGrassClip()
    {
        /*int index = UnityEngine.Random.Range(0, walkOnDryGrass.Length);*/
        SoundEffects.clip = walkOnDryGrass;
        SoundEffects.Play();
    }
    public void PlayNormalClip()
    {
        /*int index = UnityEngine.Random.Range(0, walkOnDryGrass.Length);*/
        SoundEffects.clip = walkOnNormal;
        SoundEffects.Play();
    }

    public void PlaySpaceClip()
    {
        /*int index = UnityEngine.Random.Range(0, walkOnDryGrass.Length);*/
        SoundEffects.clip = walkOnSpace;
        SoundEffects.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SoundEffects.clip = clip;
        SoundEffects.Play();
        Debug.Log(clip.name + "is Playing");
    }

    public void PlaySFX(string name)
    {
        Sound SoundThatWeFind = Array.Find(sounds, sound => sound.Name == name);

        SoundEffects.clip = SoundThatWeFind.clip;
        SoundEffects.Play();

        if (SoundThatWeFind == null)
        {
            Debug.LogWarning("Music reference " + name + " is not found");
            return;
        }
    }

    public void SetVolume()
    {
        BGMusic.volume = audioVolume;
        SoundEffects.volume = audioVolume;
        PlayerPrefs.SetFloat("SoundEffectsVolume", audioVolume);
        PlayerPrefs.SetFloat("MainVolume", BGMusic.volume);
    }
    public void VolumeUpdateEffects(float v)
    {
        PlayerPrefs.SetFloat("SoundEffectsVolume", v);
    }

    public void VolumeUpdate(float v)
    {
        BGMusic.volume = v;
        PlayerPrefs.SetFloat("MainVolume", v);
    }


}