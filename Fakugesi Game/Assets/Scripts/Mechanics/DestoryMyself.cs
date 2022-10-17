using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DestoryMyself : MonoBehaviour
{
    [Header("Destroy Float")]
    [SerializeField] float timeBeforeDestroy;

    [Header("Input Needed Boolean")]
    [SerializeField] bool inputNeeded = false;
    void Start()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }

	private void Update()
	{
        //If there's no input manager simply stop running
        if(InputManager.Instance == null)
            return;

        //Check for player inout and make sure this object needs input to destroy
		if(InputManager.Instance.onInteract && inputNeeded)
            Destroy(gameObject);
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/