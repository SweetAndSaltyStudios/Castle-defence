using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DropZone : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        // Debug.Log("OnPointerEnter");     
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
            d.placeholderParent = transform;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " +gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null)
            d.parentToReturn = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        if (d != null && d.placeholderParent == this.transform)
            d.placeholderParent = d.parentToReturn; 
    }
}
