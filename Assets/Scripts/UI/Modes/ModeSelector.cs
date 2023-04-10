using System;
using System.Collections.Generic;
using System.Linq;
using Sifter.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Sifter.UI.Modes
{
    public class ModeSelector : MonoBehaviour
    {
        [SerializeField] List<Toggle> _modeToggles;
        StateEnum _localState;

        void Awake()
        {
            _localState = StateManager.Singleton._currentState;
            EventManager.Singleton.OnStateChanged += ctx => _localState = ctx;
            EventManager.Singleton.OnModeChange.AddListener(HandleModeChange);
            EventManager.Singleton.OnModeClear.AddListener(HandleModeClear);
            print("registered listeners");
        }

        void OnDisable()
        {
            EventManager.Singleton.OnStateChanged -= ctx => _localState = ctx;
            EventManager.Singleton.OnModeChange.RemoveListener(HandleModeChange);
            EventManager.Singleton.OnModeClear.RemoveListener(HandleModeClear);
        }

        void HandleModeClear()
        {
            foreach (var mode in _modeToggles.Where(mode => mode.isOn))
                // mode.GetComponentInChildren<ColorSwapper>().SwapColor();
                mode.isOn = false;
        }

        void HandleModeChange()
        {
            switch (_localState)
            {
                case StateEnum.BROWSE:
                    // TODO: Handle browse mode
                    break;
                case StateEnum.EDIT:
                    // TODO: Handle edit mode
                    break;
                case StateEnum.RECORD:
                    // TODO: Handle record mode
                    break;
                case StateEnum.POPUP:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}