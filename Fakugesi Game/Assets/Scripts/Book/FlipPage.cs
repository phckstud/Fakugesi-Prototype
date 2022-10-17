using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FlipPage : MonoBehaviour
{
	#region Button States
    public enum FlipBookButtonType { Next, Previous}
    #endregion
    [Header("Internal Variables")]
    [SerializeField] FlipBookButtonType buttonType;

    [Header("Unity Handles")]
    [SerializeField] Button nextBtn;
    [SerializeField] Button prevBtn;
    [SerializeField] Vector3 rotationVector, defaultRotationVector;
    [SerializeField] Vector3 startPos;
    [SerializeField] Quaternion startRot;

    [Header("Generic Values")]
    [SerializeField] DateTime startTime;
    [SerializeField] DateTime endTime;

    [Header("Booleans")]
    [SerializeField] bool isPressed;

    void Start()
    {
        if (nextBtn != null)
            nextBtn.onClick.AddListener(() => TurnPage(FlipBookButtonType.Next));

        if (prevBtn != null)
            prevBtn.onClick.AddListener(() => TurnPage(FlipBookButtonType.Previous));

        //Store Rotation Vector
        defaultRotationVector = rotationVector;

        //Store starting Position and rotation
        startRot = transform.rotation;
        startPos = transform.position;
    }

   
    void Update()
    {
        if(isPressed)
		{
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            //one is in seconds
            if((endTime - startTime).TotalSeconds >= 1)
			{
                isPressed = false;
                transform.rotation = startRot;
                transform.position = startPos;
			}
		}
    }

    void TurnPage(FlipBookButtonType btn)
	{
        isPressed = true;
        startTime = DateTime.Now;

        //Flip Left or Right
        if(btn == FlipBookButtonType.Previous)
		{
            Vector3 newRot = new Vector3(startRot.x, -defaultRotationVector.y, startRot.z);
        }
        else if(btn == FlipBookButtonType.Next)
		{
            Vector3 newRot = new Vector3(startRot.x, defaultRotationVector.y, startRot.z);
            transform.rotation = Quaternion.Euler(newRot);
            rotationVector = defaultRotationVector;
        }
	}

}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/