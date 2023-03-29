using TMPro;
using UnityEngine;

namespace Sifter
{
    public class Button : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        [SerializeField] bool _changeable;

        public int _row;
        public int _column;
        public string normalText = "K";
        public string shiftText = "";

        void Start()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _text.text = normalText;
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _text.text = normalText;
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
    }
}