using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	[Header("Generic Elements")]
	[SerializeField] string sceneName;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		SceneManager.LoadScene(sceneName);
	}
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/