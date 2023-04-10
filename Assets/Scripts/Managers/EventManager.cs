using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sifter.Managers
{
    public class EventManager : MonoBehaviour
    {
        public UnityEvent OnModeChange;
        public UnityEvent OnModeClear;
        public UnityAction<string> OnKeymapCreateConfirmed;
        public Action OnLayoutChanged;
        public UnityAction OnPopupCancelled;
        public Action<StateEnum> OnStateChanged;
        public static EventManager Singleton { get; private set; }

        void Awake()
        {
            if (Singleton != null && Singleton != this)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            DontDestroyOnLoad(gameObject);
            OnKeymapCreateConfirmed += HandleOnKeymapCreateConfirmed;
            OnPopupCancelled += HandleOnPopupCancelled;
        }

        void OnDisable()
        {
            OnKeymapCreateConfirmed -= HandleOnKeymapCreateConfirmed;
        }

        void HandleOnPopupCancelled()
        {
            // TODO: For now..
            SaveManager.Singleton.LoadKeymap();
        }

        void HandleOnKeymapCreateConfirmed(string keymapName)
        {
            SaveManager.Singleton.SaveKeymap(keymapName);
        }

        public void OnKeymapChanged(string keymapName)
        {
            // TODO: Load keymap
            if (keymapName == "Add new keymap")
                UIManager.Singleton.CreatePopupWithTextInput("Create New Keymap",
                    "Please enter a name for your new keymap.",
                    OnKeymapCreateConfirmed, OnPopupCancelled);
        }
    }
}