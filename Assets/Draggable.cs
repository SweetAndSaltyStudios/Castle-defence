using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
	private CanvasGroup canvasGroup;
	private LayoutElement layoutElement;
	public Transform parentToReturn = null;
	public Transform placeholderParent = null;

	private GameObject placeholder;

	private void Start ()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		layoutElement = GetComponent<LayoutElement>();
		canvasGroup.blocksRaycasts = true;       
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		// Debug.Log("OnBeginDrag");
		placeholder = new GameObject();
		placeholder.transform.SetParent(transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.layoutElement.preferredWidth;
		le.preferredHeight = this.layoutElement.preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

		parentToReturn = transform.parent;
		placeholderParent = parentToReturn;
		transform.SetParent(transform.parent.parent);

		canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		// Debug.Log("OnDrag");
		transform.position = eventData.position;

		if (placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);     

		int newSiblingIndex = placeholderParent.childCount;

		for (int i = 0; i < placeholderParent.childCount; i++)
		{          
			if (transform.position.x < placeholderParent.GetChild(i).position.x)
			{
				newSiblingIndex = i;

				if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;

				break;
			}
		}

		placeholder.transform.SetSiblingIndex(newSiblingIndex);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// Debug.Log("OnEndDrag");
		transform.SetParent(parentToReturn);
		transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		canvasGroup.blocksRaycasts = true;

		Destroy(placeholder);
	}  
}
