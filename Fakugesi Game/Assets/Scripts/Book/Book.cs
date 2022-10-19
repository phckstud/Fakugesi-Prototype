using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Book : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] GameObject middlePage;
    [SerializeField] GameObject frontCover, insideRightPage;
    [SerializeField] Button openBtn;
    [SerializeField] Vector3 rotationVector, defaultRotationVector;
    [SerializeField] Vector3 startPos;
    [SerializeField] Quaternion startRot;

    [Header("Generic Values")]
    [SerializeField] DateTime startTime;
	[SerializeField] DateTime endTime;

    [Header("Booleans")]
    [SerializeField] bool bookButtonIsPressed;

    void Start()
    {
        /*if (openBtn != null)
            openBtn.onClick.AddListener(() => OpenBook());*/

        //Store Rotation Vector
        defaultRotationVector = rotationVector;

        //Store starting Position and rotation
        startRot = transform.rotation;
        startPos = transform.position;
    }

   
    void Update()
    {
        if(bookButtonIsPressed)
		{
            transform.Rotate(-rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            if((endTime - startTime).TotalSeconds >= 1)
			{
                middlePage.SetActive(true);
                bookButtonIsPressed = false;
            }
		}
    }

    public void OpenBook()
	{
        bookButtonIsPressed = true;
        startTime = DateTime.Now;

        openBtn.gameObject.SetActive(false);
        frontCover.gameObject.SetActive(false);
        insideRightPage.gameObject.SetActive(false);
        //Playbook opening sound
    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/