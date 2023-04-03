using System;
using UnityEngine;

namespace Sifter.Managers
{
    public class EventManager : MonoBehaviour
    {
// Declare event for when the keyboard layout changes
        public Action OnLayoutChanged;
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
        }

        public void InvokeEvent(Action action)
        {
            action?.Invoke();
        }
    }
}