using Sifter.Keyboard;
using TMPro;
using UnityEngine;

namespace Sifter.Button
{
    public class Button : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] bool _changeable;
        [SerializeField] int _col;
        [SerializeField] int _row;


        public KeyboardButton KeyboardButton;
        bool isShiftPressed;

        void Start()
        {
            if (!_changeable) _text.text = KeyboardButton.Key;
            KeyboardButton.Position.Column = _col;
            KeyboardButton.Position.Row = _row;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            _text.text = KeyboardButton.Key;
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
        public void RedrawKey()
        {
            print("Got to this point");
            _text.text = isShiftPressed ? KeyboardButton.ShiftKey : KeyboardButton.Key;
        }
    }
}