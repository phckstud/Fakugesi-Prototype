using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI[] dialogueText;
    [SerializeField] private TextMeshProUGUI[] displayNameText;
    [SerializeField] private Animator portraitAnimator;
   // [SerializeField] private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    [SerializeField] AudioSource sfx;
    private TextMeshProUGUI[] choicesText;

    [Header("Integers")]
    [SerializeField] int dialogueIndex;
    [SerializeField] int displaynameIndex;

    [Header("Booleans")]
    [SerializeField] bool hasRunOnce;

    //To advance to the next level per finishing the story
    [Header("Generic Elements")]
    [SerializeField] string sceneName;
    [SerializeField] string storeSpeaker, storeDialogueLines;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";

    private DialogueVariables dialogueVariables;

    private void OnEnable()
    {
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();
        hasRunOnce = false;

      
        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine
            && currentStory.currentChoices.Count == 0
            && InputManager.Instance.onInteract)
        {
            if (sfx != null)
                sfx.Play();

            ContinueStory();
            InputManager.Instance.onInteract = false;
        }
    }

    public void ContinueStoryButton()
	{
        if (canContinueToNextLine
          && currentStory.currentChoices.Count == 0)
        {
            if (sfx != null)
                sfx.Play();

            ContinueStory();
        
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);

		// reset portrait, layout, and speaker
		displayNameText[displaynameIndex].text = "???";
        portraitAnimator.Play("default");
       // layoutAnimator.Play("default");

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        //Check and Update Variables
        VariableManager.Instance.CheckAndUpdateVariables();

        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
		dialogueText[dialogueIndex].text = "";

        //Check if player can disengage in conversation
        InputManager.Instance.onConversationEnter = false;

        //Check if the story should move on
        if (VariableManager.Instance.GetStoryProgress())
		{
            SceneManager.LoadScene(sceneName);
		}
    }

    private void ContinueStory()
    {
        InputManager.Instance.onInteract = false;
        hasRunOnce = true;

        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

          /*  foreach (var item in dialogueText)
            {
                if (item.text == storeDialogueLines && displaynameIndex == dialogueIndex)
                {
                    if (dialogueIndex <= dialogueText.Length)
                        dialogueIndex++;
                    else
                        dialogueIndex = 0;

                    Debug.Log("Change Dialogue");
                }
            }*/

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
           
            // handle tags
            HandleTags(currentStory.currentTags);
            //Check and Update Variables
           
            VariableManager.Instance.CheckAndUpdateVariables();
            

        }
        else
        {
            hasRunOnce = true;
            StartCoroutine(ExitDialogueMode());
        }

        
    }

    private IEnumerator DisplayLine(string line)
    {
		// set the text to the full line, but set the visible characters to 0
		dialogueText[dialogueIndex].text = line;
        dialogueText[dialogueIndex].maxVisibleCharacters = 0;

        //Store Dialogue
        storeDialogueLines = dialogueText[dialogueIndex].text;

        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (InputManager.Instance.onInteract)
            {
                if(sfx != null)
                    sfx.Play();

				dialogueText[dialogueIndex].maxVisibleCharacters = line.Length;
                InputManager.Instance.onInteract = false;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
				dialogueText[dialogueIndex].maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        if (hasRunOnce)
        {
            foreach (var item in displayNameText)
            {
                if (item.text == storeSpeaker && displaynameIndex == dialogueIndex)
                {
                    if (displaynameIndex < displayNameText.Length - 1)
                    {
                        displaynameIndex++;
                    }
                    else
                        displaynameIndex = 0;

                    dialogueIndex = displaynameIndex;

                    Debug.Log("Change Speaker");
                }
            }
        }

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    protected virtual void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
					displayNameText[displaynameIndex].text = tagValue;
                    storeSpeaker = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
               /* case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;*/
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        if (currentChoices.Count <= 0)
            return;

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            ContinueStory();
        }
    }


    public void SetVariableState(string variableName, Ink.Runtime.Object variableValue)
    {
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables.Remove(variableName);
            dialogueVariables.variables.Add(variableName, variableValue);
        }
        else
        {
            Debug.LogWarning("Tried to update variable that wasn't initialized by globals.ink: " + variableName);
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit()
    {
        if(dialogueVariables != null)
             dialogueVariables.SaveVariables();
    }

}


/*

Copyright (C) Nhlanhla 'Stud' Langa

*/