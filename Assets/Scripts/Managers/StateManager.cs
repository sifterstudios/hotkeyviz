using System;
using Sifter.Util;
using UnityEngine;

namespace Sifter.Managers
{
    public class StateManager : MonoBehaviour
    {
        public static StateManager Singleton;
        Camera _cam;

        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            EventManager.Singleton.OnStateChanged.Invoke(StateEnum.BROWSE);
        }

        void Start()
        {
            _cam = Camera.main;
            if (_cam != null) _cam.clearFlags = CameraClearFlags.SolidColor;
        }

        void OnEnable()
        {
            EventManager.Singleton.OnStateChanged += HandleOnStateChanged;
        }

        void OnDisable()
        {
            EventManager.Singleton.OnStateChanged -= HandleOnStateChanged;
        }

        void HandleOnStateChanged(StateEnum newState)
        {
            switch (newState)
            {
                case StateEnum.BROWSE:
                    _cam.backgroundColor = Constants.BackgroundColor;
                    break;
                case StateEnum.EDIT:
                    _cam.backgroundColor = Constants.DarkPurple;
                    break;
                case StateEnum.RECORD:
                    _cam.backgroundColor = Constants.DarkBlue;
                    break;
                case StateEnum.POPUP:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, "No such state");
            }
        }
    }
}