using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Sifter.UI.Popup
{
    public class SifterPopup : MonoBehaviour
    {
        [SerializeField] float _width;
        [SerializeField] float _height;
        [SerializeField] TMP_Text _title;
        [SerializeField] TMP_Text _message;
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] UnityEngine.UI.Button _confirmButton;
        [SerializeField] UnityEngine.UI.Button _cancelButton;
        bool _withTextInput;

        public string Title { get; set; }
        public string Message { get; set; }

        public void Initialize(string title, string message, UnityAction<string> onConfirm, UnityAction cancel,
            bool withTextInput = true)
        {
            _withTextInput = withTextInput;
            _inputField.gameObject.SetActive(withTextInput);
            Title = title;
            Message = message;
            _title.text = title;
            _message.text = message;
            _inputField.gameObject.SetActive(withTextInput);

            _confirmButton.onClick.AddListener(() => onConfirm(_inputField.text));
            _cancelButton.onClick.AddListener(cancel);
            _confirmButton.onClick.AddListener(DestroyMe);
            _cancelButton.onClick.AddListener(DestroyMe);
        }

        void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}