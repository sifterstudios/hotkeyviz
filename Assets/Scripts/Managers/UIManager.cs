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
            EventManager.Singleton.OnKeymapChangedInGUI += OnKeymapChanged;
        }

        void OnKeymapChanged(string keymapName)
        {
            // TODO: Load keymap
            if (keymapName == "Add new keymap")
            {
                CreatePopupWithTextInput("Create New Keymap",
                    "Please enter a name for your new keymap.",
                    EventManager.Singleton.OnKeymapCreateConfirmed, EventManager.Singleton.OnPopupCancelled);
                return;
            }
            EventManager.Singleton.OnKeymapLoadingStarted(keymapName);
        }

        public void CreatePopupWithTextInput(string title, string message, UnityAction<string> onConfirm,
            UnityAction onCancel,
            bool withTextInput = true)
        {
            var gO = Instantiate(_popup, _canvas.transform);
            gO.GetComponent<SifterPopup>().Initialize(title, message, onConfirm, onCancel, withTextInput);
        }
    }
}