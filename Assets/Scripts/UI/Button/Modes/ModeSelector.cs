using System;
using System.Collections.Generic;
using System.Linq;
using Sifter.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Sifter.UI.Button.Modes
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
            ShouldClearButtonShow();
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
                mode.isOn = false;
            ShouldClearButtonShow();
        }

        void HandleModeChange()
        {
            switch (_localState)
            {
                case StateEnum.BROWSE:
                    ShouldClearButtonShow();
                    break;
                case StateEnum.EDIT:
                    ShouldClearButtonShow();
                    break;
                case StateEnum.RECORD:
                    ShouldClearButtonShow();
                    break;
                case StateEnum.POPUP:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void ShouldClearButtonShow()
        {
            var clearButton = _modeToggles[^1];
            var shouldShow = _modeToggles.Any(mode => mode.isOn);
            clearButton.gameObject.SetActive(shouldShow);
        }
    }
}