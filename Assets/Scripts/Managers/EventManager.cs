using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sifter.Managers
{
    public class EventManager : MonoBehaviour
    {
        public UnityAction<string> OnKeymapCreateConfirmed;

// Declare event for when the keyboard layout changes
        public Action OnLayoutChanged;
        public UnityAction OnPopupCancelled;

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

        public void InvokeEvent(Action action)
        {
            action?.Invoke();
        }
    }
}