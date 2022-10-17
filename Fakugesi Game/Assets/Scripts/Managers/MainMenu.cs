using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Unity Handles")]
    public GameObject finishedText;

    [Header("Integers")]
    public int sceneBuild;

    private void Start()
    {
        sceneBuild = SceneManager.sceneCountInBuildSettings;
        finishedText.SetActive(false);
    }
    private void Update()
    {

    }
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

    public void Extra()
    {
        SceneManager.LoadScene(4);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
