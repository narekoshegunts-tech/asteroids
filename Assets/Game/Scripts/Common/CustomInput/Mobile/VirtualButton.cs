using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.Common.CustomInput.Mobile
{
    public class VirtualButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnPressed;
        public event Action OnPressedDown;
        public event Action OnPressedUp;

        public bool IsPressed { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");
            IsPressed = true;
            OnPressedDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
            OnPressedUp?.Invoke();
        }

        private void Update()
        {
            if (IsPressed)
                OnPressed?.Invoke();
        }
    }
}