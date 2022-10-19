using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HubManager : MonoBehaviour
{
	[Header("Generic Elements")]
	[SerializeField] string sceneName;
   public void LoadScene()
	{
		SceneManager.LoadScene(sceneName);
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/