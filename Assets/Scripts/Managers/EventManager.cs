using System;
using System.Collections.Generic;
using Sifter.DataManagement;
using UnityEngine;
using UnityEngine.Events;

namespace Sifter.Managers
{
    public class EventManager : MonoBehaviour
    {
        public UnityEvent OnModeChange;
        public UnityEvent OnModeClear;
        public Action OnLayoutChanged;
        public Action<StateEnum> OnStateChanged;
        public UnityAction<string> OnKeymapChangedInGUI;
        public UnityAction<string> OnKeymapCreateConfirmed;
        public UnityAction<List<KeyBinding>> OnKeymapLoadComplete;
        public UnityAction<string> OnKeymapLoadStart;
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
            PersistenceManager.Singleton.LoadKeymapName();
        }

        void HandleOnKeymapCreateConfirmed(string keymapName)
        {
            // TODO: Change this to be called from an event instead of a direct dependency
            PersistenceManager.Singleton.SaveKeymapName(keymapName);
        }
    }
}