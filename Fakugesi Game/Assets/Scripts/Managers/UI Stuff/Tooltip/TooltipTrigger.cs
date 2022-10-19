using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Generic Elements")]
	[Tooltip("We use these string to state what elements will do")]
	[SerializeField] string content;
	[SerializeField] string header;

	[Header("Offset")]
	[SerializeField] float xOffset = 0.9f, yOffset = 0.5f;
	void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
	{
		TooltipSystem.Show(content, header);
		TooltipSystem.Position(this.transform.position, xOffset, yOffset);
		this.GetComponent<InteractableObject>().StartHighlight();
	}

	void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
	{
		TooltipSystem.Hide();
		this.GetComponent<InteractableObject>().StopHighlight();
	}

	public void ShowToolTip()
	{
		TooltipSystem.Show(content, header);
	}

	public void HideToolTip()
	{
		TooltipSystem.Hide();
	}

	private void OnMouseEnter()
	{
		ShowToolTip();
		this.GetComponent<InteractableObject>().StartHighlight();
	}

	private void OnMouseExit()
	{
		HideToolTip();
		this.GetComponent<InteractableObject>().StopHighlight();
	}
}
