using System;
using DTT.UI.ProceduralUI;
using Sifter.Keyboard;
using Sifter.Managers;
using Sifter.Util;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using static Sifter.InputManager;

namespace Sifter.UI.Button
{
    public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] int _col;
        [SerializeField] int _row;
        [SerializeField] RoundedImage _border;

        [SerializeField] Color _noKeybindsColor;
        [SerializeField] Color _oneKeybindColor;
        [SerializeField] Color _twoKeybindsColor;
        [SerializeField] Color _threeKeybindsColor;
        [SerializeField] Color _fourKeybindsColor;
        [SerializeField] Color _fiveKeybindsColor;
        [SerializeField] Color _sixKeybindsColor;

        [FormerlySerializedAs("_moreThanSevenKeybindsColor")] [SerializeField]
        Color _moreThanSixKeybindsColor;


        public KeyboardButton KeyboardButton;
        public bool Changeable;
        RoundedImage _background;
        int _bindingCounter; // TODO: Add functionality for when binding is added or removed.
        int _currentDrawnBindingCounter;
        bool _isShiftPressed;
        StateEnum _localState;

        void Awake()
        {
            Singleton.Inputactions.Browse.ShiftLayerVisual.performed += _ => ShiftPressed();
            Singleton.Inputactions.Browse.ShiftLayerVisual.canceled += _ => ShiftReleased();
            EventManager.Singleton.OnStateChanged += ctx => _localState = ctx;
        }

        void Start()
        {
            if (!Changeable)
                _text.text = KeyboardButton.Key; // NOTE: Possible race condition, keyboardbutton needs to be set first!
            KeyboardButton.Position.Column = _col;
            KeyboardButton.Position.Row = _row;
            _background = GetComponent<RoundedImage>();
            RedrawKey();
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

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (_localState)
            {
                case StateEnum.BROWSE:
                    // TODO: Show hover with current keybinds or update visual if button is a prefix. To be honest,
                    // Hover should probably be handled by actual hovering and prefixing by clicking.
                    break;
                case StateEnum.EDIT:
                    // TODO: Editing might only be deletion, in chat case show a popup with confirmation message.
                    break;
                case StateEnum.RECORD:
                    EventManager.Singleton.OnButtonClickedInRecordMode.Invoke(this);
                    break;
                case StateEnum.POPUP:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

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
            if (_isShiftPressed) return;
            DecideOnBackground();
        }

        void DecideOnBackground()
        {
            if (_currentDrawnBindingCounter == _bindingCounter && _currentDrawnBindingCounter != 0) return;
            _background.color = _localState == StateEnum.RECORD
                ? Constants.Red
                : GetColorBasedOnBindingCounter();
            _currentDrawnBindingCounter = _bindingCounter;
        }

        Color GetColorBasedOnBindingCounter()
        {
            return _bindingCounter switch
            {
                0 => _noKeybindsColor,
                1 => _oneKeybindColor,
                2 => _twoKeybindsColor,
                3 => _threeKeybindsColor,
                4 => _fourKeybindsColor,
                5 => _fiveKeybindsColor,
                6 => _sixKeybindsColor,
                _ => _moreThanSixKeybindsColor
            };
        }

        public void IncrementBindingCounter(int count = 1)
        {
            _bindingCounter += count;
        }

        public void DecrementBindingCounter()
        {
            _bindingCounter = Math.Clamp(_bindingCounter - 1, 0, int.MaxValue);
        }
    }
}