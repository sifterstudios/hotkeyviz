using Sifter.Keyboard;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Sifter.Button
{
    public class Button : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] bool _changeable;

        [ShowInInspector] public KeyboardButton KeyboardButton = new();
        bool isShiftPressed;

        void Start()
        {
            _text.text = KeyboardButton.Key;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            _text = GetComponentInChildren<TMP_Text>();
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
        public void redrawKey()
        {
            _text.text = isShiftPressed ? KeyboardButton.Key : KeyboardButton.ShiftKey;
        }
    }
}