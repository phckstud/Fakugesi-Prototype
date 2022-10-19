using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region Variables

	[Header("Unity Handles")]
    [SerializeField] Transform player;
    [SerializeField] GameObject Cam1, Cam2;
    public Animator anim;
    public Animator fadeAnim;

    [Header("Tutorial: Elements To Hide")]
    public GameObject Level;
    public GameObject Drivers;

    [Header("Level Set-Up Values")]
    [SerializeField] SpriteRenderer backgroundSpr;
    [SerializeField] SpriteRenderer treasureSpr;
    [SerializeField] Sprite[] backgrounds;
	[SerializeField] Sprite[] treasures;
    [SerializeField] Transform[] spawnPoints;
    public GameObject alliesPrefab, alliesParent, enemyPrefab, enemyParent;

    [Header("Floats")]
    public float AnimFinished;
    public float FadeFinished;
    public Vector3 offset;
    Vector3 endPos;

    [Header("Booleans")]
    public bool animFinished;

    [Header("Generic Elements")]
    public string animName;
    public string sceneSuccessName, sceneFailureName, fadeAnimName;
	#endregion
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //animFinished = false;

        if(anim != null)
            anim.Play(animName);
    }

   
    void Update()
    {
       //StartCoroutine(CamChanger());
       if(VariableManager.Instance != null)
		{
            if(VariableManager.Instance.GetStoryProgress())
			{
                SceneManager.LoadScene(sceneSuccessName);
			}
		}
    }

	

	#region Camera and Cut-scenes
	IEnumerator CamChanger()
    {
        if (animFinished == false)
        {
            Cam1.SetActive(false);
            Cam2.SetActive(true);
            yield return new WaitForSeconds(AnimFinished);
            Cam1.SetActive(true);
            Cam2.SetActive(false);
            animFinished = true;
        }
    }
	#endregion
    
	#region End Game Load Scene
    public void LoadNextScene(bool whichLevel)
	{
        StartCoroutine(FadeOut(whichLevel));
	}

    IEnumerator FadeOut(bool levelToGoTo)
	{
        fadeAnim.Play(fadeAnimName);
        yield return new WaitForSeconds(FadeFinished);

        //Successful mission
        if (levelToGoTo == true)
            SceneManager.LoadScene(sceneSuccessName);
        else if (levelToGoTo == false)
            SceneManager.LoadScene(sceneFailureName);
	}
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/