using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnPointerUnityEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] UnityEvent _onDown = null;
    [SerializeField] UnityEvent _onUp = null;

    public void OnPointerDown(PointerEventData eventData)
    {
        _onDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _onUp?.Invoke();
    }
}
