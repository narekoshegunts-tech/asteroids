using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Common.CustomInput.Mobile
{
public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform _handle;

    private float _radius;

    public Vector2 Direction { get; private set; }
    public bool IsPressed { get; private set; }
    public bool WasPressedThisFrame { get; private set; }
    public bool WasReleasedThisFrame { get; private set; }

    private Vector2 _center;
    
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        
        RectTransform background = (RectTransform)transform;
        _radius = background.sizeDelta.x * 0.5f * 0.8f;

        _center = background.anchoredPosition;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        WasPressedThisFrame = true;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position / _canvas.scaleFactor;
        Vector2 direction = pos - _center;

        if (direction.magnitude > _radius)
            direction = direction.normalized * _radius;

        _handle.anchoredPosition = direction;
        Direction = direction / _radius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        WasReleasedThisFrame = true;
        _handle.anchoredPosition = Vector2.zero;
    }
    
}
}
