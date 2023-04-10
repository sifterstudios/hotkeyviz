using Sifter.UI.Popup;
using UnityEngine;
using UnityEngine.Events;

namespace Sifter.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Singleton;
        [SerializeField] GameObject _popup;
        [SerializeField] GameObject _canvas;


        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
        }

        public void CreatePopupWithTextInput(string title, string message, UnityAction<string> onConfirm,
            UnityAction onCancel,
            bool withTextInput = true)
        {
            // new transform in the middle of the screen
            var gO = Instantiate(_popup, _canvas.transform);
            gO.GetComponent<SifterPopup>().Initialize(title, message, onConfirm, onCancel, withTextInput);
        }
    }
}