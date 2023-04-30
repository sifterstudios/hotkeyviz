using System;
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
        [SerializeField] GameObject _descriptionAndConfirmAndDelete;


        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            EventManager.Singleton.OnKeymapChangedInGUI += OnKeymapChanged;
            EventManager.Singleton.OnStateChanged += OnStateChanged;
            _descriptionAndConfirmAndDelete.gameObject.SetActive(false);
        }

        void OnDisable()
        {
            EventManager.Singleton.OnKeymapChangedInGUI -= OnKeymapChanged;
            EventManager.Singleton.OnStateChanged -= OnStateChanged;
        }

        void OnStateChanged(StateEnum state)
        {
            switch (state)
            {
                case StateEnum.BROWSE:
                    _descriptionAndConfirmAndDelete.gameObject.SetActive(false);
                    break;
                case StateEnum.EDIT:
                    _descriptionAndConfirmAndDelete.gameObject.SetActive(true);
                    break;
                case StateEnum.RECORD:
                    _descriptionAndConfirmAndDelete.gameObject.SetActive(true);
                    break;
                case StateEnum.POPUP:
                    _descriptionAndConfirmAndDelete.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, "No such state");
            }
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

            EventManager.Singleton.OnKeymapLoadStart(keymapName);
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