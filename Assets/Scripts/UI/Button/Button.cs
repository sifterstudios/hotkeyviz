using Sifter.Keyboard;
using TMPro;
using UnityEngine;

namespace Sifter.UI.Button
{
    public class Button : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] int _col;
        [SerializeField] int _row;

        public KeyboardButton KeyboardButton;
        public bool Changeable;
        bool _isShiftPressed;

        void Awake()
        {
            InputManager.Instance.Inputactions.Browse.ShiftLayerVisual.performed += ctx => ShiftPressed();
            InputManager.Instance.Inputactions.Browse.ShiftLayerVisual.canceled += ctx => ShiftReleased();
        }

        void Start()
        {
            if (!Changeable) _text.text = KeyboardButton.Key;
            KeyboardButton.Position.Column = _col;
            KeyboardButton.Position.Row = _row;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (Changeable) _text.text = KeyboardButton.Key;
            var transformLocalScale = _text.transform.localScale;
            var localScale = transformLocalScale;
            var x = localScale.x;
            var y = localScale.y;
            if (x > y)
                transformLocalScale.x = y;
            else
                transformLocalScale.y = x;
        }
#endif

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
        }
    }
}