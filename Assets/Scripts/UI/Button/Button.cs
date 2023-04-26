using System;
using DTT.UI.ProceduralUI;
using Sifter.Keyboard;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static Sifter.InputManager;

namespace Sifter.UI.Button
{
    public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] int _col;
        [SerializeField] int _row;
        [SerializeField] RoundedImage _border;


        public KeyboardButton KeyboardButton;
        public bool Changeable;
        int _bindingCounter;
        int _currentDrawnBindingCounter;
        bool _isShiftPressed;

        void Awake()
        {
            Singleton.Inputactions.Browse.ShiftLayerVisual.performed += _ => ShiftPressed();
            Singleton.Inputactions.Browse.ShiftLayerVisual.canceled += _ => ShiftReleased();
        }

        void Start()
        {
            if (!Changeable) _text.text = KeyboardButton.Key;
            KeyboardButton.Position.Column = _col;
            KeyboardButton.Position.Row = _row;
        }

        void OnDisable()
        {
            Singleton.Inputactions.Browse.ShiftLayerVisual.performed -= _ => ShiftPressed();
            Singleton.Inputactions.Browse.ShiftLayerVisual.canceled -= _ => ShiftReleased();
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (Changeable) _text.text = KeyboardButton.Key;
            var transformLocalScale = _text.transform.localScale;
            var localScale = transformLocalScale;
            var x = localScale.x;
            var y = localScale.y;

            if (x > y) transformLocalScale.x = y;
            else transformLocalScale.y = x;
        }
#endif

        public void OnPointerEnter(PointerEventData eventData)
        {
            _border.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _border.gameObject.SetActive(false);
        }

        void ShiftPressed()
        {
            if (!Changeable) return;
            _isShiftPressed = true;
            RedrawKey();
        }

        void ShiftReleased()
        {
            if (!Changeable) return;
            _isShiftPressed = false;
            RedrawKey();
        }

        public void RedrawKey()
        {
            if (!Changeable) return;
            _text.text = _isShiftPressed ? KeyboardButton.ShiftKey : KeyboardButton.Key;
            if (_bindingCounter == _currentDrawnBindingCounter) return;
            DecideOnBackground();
        }

        void DecideOnBackground()
        {
            throw new NotImplementedException();
        }

        public void IncrementBindingCounter()
        {
            _bindingCounter++;
        }

        public void DecrementBindingCounter()
        {
            _bindingCounter = Math.Clamp(_bindingCounter - 1, 0, int.MaxValue);
        }
    }
}