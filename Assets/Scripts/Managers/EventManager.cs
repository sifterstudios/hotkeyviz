using System;
using UnityEngine;
using UnityEngine.Events;

namespace Sifter.Managers
{
    public class EventManager : MonoBehaviour
    {
        public UnityEvent OnModeChange;
        public UnityEvent OnModeClear;
        public UnityAction<string> OnKeymapChangedInGUI;
        public UnityAction<string> OnKeymapLoadingStarted;
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
            PersistenceManager.Singleton.LoadKeymap();
        }

        void HandleOnKeymapCreateConfirmed(string keymapName)
        {
            // TODO: Change this to be called from an event instead of a direct dependency
            PersistenceManager.Singleton.SaveKeymap(keymapName);
        }

    }
}