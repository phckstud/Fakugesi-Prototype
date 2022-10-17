using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using Ink.Runtime;

public class VariableManager : SingletonPersistent<VariableManager>
{
    #region Variables
    [Header("Story Specific Strings")]
    [SerializeField] string location = "locationChosen";
    [SerializeField] string allies = "numberOfAllies";
    [SerializeField] string treasure = "treasureWanted";
    [SerializeField] string difficulty = "difficultyScale";
    [SerializeField] string advance = "levelCanAdvance";

    [Header("Story Specific Integers")]
    [SerializeField] int locationChosen;
    [SerializeField] int numberOfAllies;
    [SerializeField] int treasureWanted;
    [SerializeField] int difficultyScale;

    [Header("Story Specific Boolean")]
    [SerializeField] bool levelCanAdvance;
	#endregion

    void Update()
    {
       // CheckAndUpdateVariables();
    }

    public void CheckAndUpdateVariables()
	{
        //Update location
        locationChosen = ((Ink.Runtime.IntValue)DialogueManager.Instance.GetVariableState(location)).value;
        Debug.Log("Location: " + locationChosen);

        //Update Allies
        numberOfAllies = ((Ink.Runtime.IntValue)DialogueManager.Instance.GetVariableState(allies)).value;
        Debug.Log("Allies: " + numberOfAllies);

        //Update treasure
        treasureWanted = ((Ink.Runtime.IntValue)DialogueManager.Instance.GetVariableState(treasure)).value;
        Debug.Log("Treasure: " + treasureWanted);

        //Update treasure
        difficultyScale = ((Ink.Runtime.IntValue)DialogueManager.Instance.GetVariableState(difficulty)).value;
        Debug.Log("Difficulty: " + difficultyScale);

        //Update story boolean
        levelCanAdvance = ((Ink.Runtime.BoolValue)DialogueManager.Instance.GetVariableState(advance)).value;
        Debug.Log("Story Progress: " + levelCanAdvance);
    }

	#region Integer/Boolean Callbacks
	public int GetLocation()
	{
        return locationChosen;
	}

    public int GetNumberOfAllies()
	{
        return numberOfAllies;
	}

    public int GetTreasure()
	{
        return treasureWanted;
	}

    public int GetDifficultyScale()
	{
        return difficultyScale;
	}

    public bool GetStoryProgress()
	{
        return levelCanAdvance;
	}
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/