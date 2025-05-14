using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GraphDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
        => BeginDrag(eventData);

    protected abstract void BeginDrag(PointerEventData eventData);

    public void OnDrag(PointerEventData eventData)
        => Drag(eventData);

    protected abstract void Drag(PointerEventData eventData);

    public void OnEndDrag(PointerEventData eventData)
        => EndDrag(eventData);

    protected abstract void EndDrag(PointerEventData eventData);
}
