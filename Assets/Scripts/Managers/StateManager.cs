using UnityEngine;

namespace Sifter.Managers
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Singleton;
        public StateEnum _currentState;

        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            _currentState = StateEnum.BROWSE;
            EventManager.Singleton.OnStateChanged += HandleOnStateChanged;
        }

        void HandleOnStateChanged(StateEnum newState)
        {
            _currentState = newState;
        }
    }
}