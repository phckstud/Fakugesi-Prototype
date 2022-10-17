using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Booleans")]
    [SerializeField] bool playerInRange;
    [SerializeField] bool requireInteraction, forBook;

    private void Awake()
    {
       // playerInRange = false;
        //visualCue.SetActive(false);
    }

    private void Update()
    {
        if (requireInteraction && !forBook)
        {
            if (InputManager.Instance.onInteract && !DialogueManager.Instance.dialogueIsPlaying)
            {
                DialogueManager.Instance.EnterDialogueMode(inkJSON);
                InputManager.Instance.onInteract = false;
            }
        }

       if(!requireInteraction && !forBook)
        {
           if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
            {
                visualCue.SetActive(true);
                if (InputManager.Instance.onConversationEnter)
                {
                    DialogueManager.Instance.EnterDialogueMode(inkJSON);
                }
            }
            else
            {
                visualCue.SetActive(false);
            }
        }
    }

    public void OpenBook()
	{
        if(forBook)
		{
            DialogueManager.Instance.EnterDialogueMode(inkJSON);
        }
	}

   private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/