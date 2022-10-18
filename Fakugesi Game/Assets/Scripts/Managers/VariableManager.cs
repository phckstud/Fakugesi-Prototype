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
    [SerializeField] string advance = "levelCanAdvance";

    [Header("Story Specific Boolean")]
    [SerializeField] bool levelCanAdvance;
	#endregion

    void Update()
    {
       // CheckAndUpdateVariables();
    }

    public void CheckAndUpdateVariables()
	{
        //Update story boolean
        levelCanAdvance = ((Ink.Runtime.BoolValue)DialogueManager.Instance.GetVariableState(advance)).value;
        Debug.Log("Story Progress: " + levelCanAdvance);
    }

	#region Integer/Boolean Callbacks
    public bool GetStoryProgress()
	{
        return levelCanAdvance;
	}
	#endregion
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/