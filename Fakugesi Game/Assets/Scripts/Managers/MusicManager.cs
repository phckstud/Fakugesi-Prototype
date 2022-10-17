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

    [Header("Unity Handles")]
    public Canvas pauseCanvas;
    public Slider mainMusic;
    public Slider effectsmusic;

    [Header("Audio Sources")]
    public AudioSource BGMusic;
    public AudioSource SoundEffects;

    [Header("Audio Clips")]
    public AudioClip[] bgMusic;
    public AudioClip Click, Jump, CompleteLevel, spike, rotatingPlatform;

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
        PlayBGSound();
      //  SetVolume();
       // PauseGame();

       /* if (pauseCanvas == null)
        {
            pauseCanvas = GetComponentInChildren<Canvas>();
        }*/

    }
    public void PlayBGSound()
    {
        if (SceneManager.GetActiveScene().name == sceneName || SceneManager.GetActiveScene().name == introSceneName)
        {
            BGMusic.Stop(); 
            return;
        }
        int index = UnityEngine.Random.Range(0, bgMusic.Length);
        if (!BGMusic.isPlaying)
        {
            Debug.Log("Not Playing");
            BGMusic.clip = bgMusic[index];
            BGMusic.Play();
            Debug.Log("Is Playing");
        }

    }

    public void PlaySFX(AudioClip clip)
    {
        SoundEffects.clip = clip;
        SoundEffects.Play();
        Debug.Log(clip.name + "is Playing");
    }

    public void Play(string name)
    {
        Sound SoundThatWeFind = Array.Find(sounds, sound => sound.Name == name);

        if (!SoundThatWeFind.AudioSource.isPlaying)
            SoundThatWeFind.AudioSource.Play();

        if (SoundThatWeFind == null)
        {
            Debug.LogWarning("Music reference " + name + " is not found");
            return;
        }
    }
    public void GetPlayerPrefs(float v)
    {
        v = PlayerPrefs.GetFloat("SoundEffectsVolume");
        effectsmusic.value = v;
        mainMusic.value = PlayerPrefs.GetFloat("MainVolume");
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

    #region Pausing
    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseCanvas.gameObject.SetActive(isPaused);
        }
    }

    public void ResumeGame()
    {
        PlaySFX(Click);
        isPaused = !isPaused;
        pauseCanvas.gameObject.SetActive(isPaused);
    }
    public void MainMenu()
    {
        PlaySFX(Click);
        SceneManager.LoadScene(sceneName);

        isPaused = false;
        pauseCanvas.gameObject.SetActive(isPaused);
    }

    public void ExitGame()
    {
        PlaySFX(Click);
        Application.Quit();
        Debug.Log("GG");
    }
    #endregion

    IEnumerator CanvasDisplay()
    {
        pauseCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        pauseCanvas.gameObject.SetActive(false);
    }
}