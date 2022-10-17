using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region Variables
    public enum Difficulty { easy, medium, hard}

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
    public Difficulty difficultyScale;
    public int difficultyEasy = 1, difficultyMedium = 3, difficultyHard = 5;

    [Header("Floats")]
    public float AnimFinished;
    public float FadeFinished;
    public Vector3 offset;
    Vector3 endPos;

    [Header("Booleans")]
    public static bool animFinished;

    [Header("Generic Elements")]
    public string animName;
    public string sceneSuccessName, sceneFailureName, fadeAnimName;
	#endregion
	void Start()
    {
        ConfirmDifficulty();

        SetUpEntireLevel();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animFinished = false;
        anim.Play(animName);

      
    }

   
    void Update()
    {
       StartCoroutine(CamChanger());
    }

	#region Level Set-Up
    //For Start/Load
    void SetUpEntireLevel()
	{
        if (VariableManager.Instance != null)
        {
            SetUpBackground();
            SetUpTreasure();
           // SetUpAllies();
            SetUpDifficulty();
        }
	}

    void SetUpBackground()
	{
        backgroundSpr.sprite = backgrounds[VariableManager.Instance.GetLocation()];

	}

    void SetUpTreasure()
	{
        treasureSpr.sprite = treasures[VariableManager.Instance.GetTreasure()];
	}

    void SetUpAllies()
	{
        Vector3 startPos = player.gameObject.transform.position;
        //Vector3 posOffset = (endPos - startPos) / (VariableManager.Instance.GetNumberOfAllies() - 1);

		for (int i = 0; i < VariableManager.Instance.GetNumberOfAllies(); i++)
		{
           GameObject allyprefab = Instantiate(alliesPrefab, startPos + offset, Quaternion.identity);
           startPos += offset;
           allyprefab.transform.SetParent(alliesParent.transform);
        }
	}
    
    void SetUpDifficulty()
	{
        switch(VariableManager.Instance.GetDifficultyScale())
		{
            case 0:
                difficultyScale = Difficulty.easy;
                break;
            case 1:
                difficultyScale = Difficulty.medium;
                break;
            case 2:
                difficultyScale = Difficulty.hard;
                break;
            default:
                Debug.LogWarning("Difficulty cannot be found: " + VariableManager.Instance.GetDifficultyScale());
                break;
		}

        ConfirmDifficulty();
	}

    void ConfirmDifficulty()
	{
        switch(difficultyScale)
		{
            case Difficulty.easy:
				for (int i = 0; i < difficultyEasy; i++)
				{
                    int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
				
				}
                break;
            case Difficulty.medium:
                for (int i = 0; i < difficultyMedium; i++)
                {
                    int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                  
                }
                break;
            case Difficulty.hard:
                for (int i = 0; i < difficultyHard; i++)
                {
                    int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
                   
                }
                break;
            default:
                Debug.LogWarning("Difficulty ChoseN: " + difficultyScale);
                break;
        }
	}
	#endregion

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