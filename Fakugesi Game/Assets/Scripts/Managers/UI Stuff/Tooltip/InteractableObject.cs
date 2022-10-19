using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    [Header("Unity Handles")]
    [SerializeField] Vector3 startingPos;
	[SerializeField] Vector3 currentPos;
    [SerializeField] Quaternion startRot;

    [Header("Interacting Values")]
    [SerializeField] Color defaultMat;
    [SerializeField] Color highlightMat;

	[Header("Floats")]
	[SerializeField] float moveSpeed = 5f;

	[Header("Booleans")]
	[Tooltip("Only enable for objects that will change colour")]
	[SerializeField] bool isColourPicker = false;
	[Tooltip("Will make it possible to check which object is selected")]
	[SerializeField] bool isSelected = false;
	[Tooltip("For Objects that can be moved")]
	[SerializeField] bool canBeMoved = false;

	private void Awake()
	{
			defaultMat = gameObject.GetComponent<Image>().color;

		startingPos = transform.position;
		currentPos = startingPos;
		startRot = transform.rotation;
	}
	private void Update()
	{
		if(canBeMoved)
			transform.position = Vector3.Lerp(transform.position, currentPos, Time.deltaTime * moveSpeed);
	}
	public void Select(Vector3 focusPos, Vector3 tar)
	{
        currentPos = focusPos;
        transform.LookAt(tar);
	}

    public void DeSelect()
	{
        currentPos = startingPos;
		transform.rotation = startRot;
	}

    public void StartHighlight()
	{
		this.GetComponent<Image>().color = highlightMat;
		Debug.Log("Runs");
	}

    public void StopHighlight()
	{ 
		this.GetComponent<Image>().color = defaultMat;
	}

	public void ChangeColour(Color c)
	{
		if(isColourPicker && isSelected)
		{
			defaultMat = c;
		}
	}

	public bool IsSelected(bool v)
	{
		isSelected = v;
		return isSelected;
	}
}
