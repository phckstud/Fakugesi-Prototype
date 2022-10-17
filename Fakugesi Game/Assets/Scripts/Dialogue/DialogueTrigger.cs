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

    //private bool playerInRange;

    private void Awake()
    {
       // playerInRange = false;
        //visualCue.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.Instance.onInteract && !DialogueManager.Instance.dialogueIsPlaying)
        {
            DialogueManager.Instance.EnterDialogueMode(inkJSON);
            InputManager.Instance.onInteract = false;
        }

        /* if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
         {
             visualCue.SetActive(true);
            /* if (InputManager.GetInstance().GetInteractPressed())
             {
                 DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
             }
         }
         else
         {
             visualCue.SetActive(false);
         }*/
    }

   /* private void OnTriggerEnter2D(Collider2D collider)
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
    }*/
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/