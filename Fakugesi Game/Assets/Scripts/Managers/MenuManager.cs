using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] AudioSource BGMusic;
    [SerializeField] AudioClip[] bgMusic;

    [Header("Main Menu Effects")]
    [SerializeField] TextMeshProUGUI mainTitleText;
    [SerializeField] float typingSpeed;

	[Header("Generic Elements")]
	[SerializeField] string sceneToLoad;

	private void Start()
	{
        StartCoroutine(DisplayLine(mainTitleText.text));
	}
	#region Menu
	public void PlayGame()
	{
		SceneManager.LoadScene(sceneToLoad);
	}

	public void ExitGame()
	{
		Application.Quit();
		Debug.Log("GGs");
	}
	#endregion

	private void Update()
	{
        PlayBGSound();
	}
	#region Music & Effects
	public void PlayBGSound()
    {
        int index = UnityEngine.Random.Range(0, bgMusic.Length);
        if (!BGMusic.isPlaying)
        {
            Debug.Log("Not Playing");
            BGMusic.clip = bgMusic[index];
            BGMusic.Play();
            Debug.Log("Is Playing");
        }

    }
    //Typewrite Effect
    private IEnumerator DisplayLine(string line)
    {
        // set the text to the full line, but set the visible characters to 0
        mainTitleText.text = line;
        mainTitleText.maxVisibleCharacters = 0;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                mainTitleText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/