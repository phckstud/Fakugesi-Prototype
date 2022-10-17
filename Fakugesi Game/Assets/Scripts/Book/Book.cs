using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] Button openBtn;
    [SerializeField] Vector3 rotationVector;

    [Header("Generic Values")]
    [SerializeField] DateTime startTime;
	[SerializeField] DateTime endTime;

    [Header("Booleans")]
    [SerializeField] bool bookButtonIsPressed;

    void Start()
    {
        if (openBtn != null)
            openBtn.onClick.AddListener(() => OpenBook());
    }

   
    void Update()
    {
        if(bookButtonIsPressed)
		{
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            if((endTime - startTime).TotalSeconds >= 1)
			{
                bookButtonIsPressed = false;
                
            }
		}
    }

    void OpenBook()
	{
        bookButtonIsPressed = true;
        startTime = DateTime.Now;

        openBtn.gameObject.SetActive(false);
        //Playbook opening sound
    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/