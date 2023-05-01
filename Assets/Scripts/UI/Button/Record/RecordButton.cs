using System;
using Sifter.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Sifter.UI.Button.Record
{
    public class RecordButton : MonoBehaviour
    {
        [SerializeField] GameObject _visualButton;
        bool _isRecording;

        void OnEnable()
        {
            EventManager.Singleton.OnStateChanged += HandleStateChanged;
        }

        void OnDisable()
        {
            EventManager.Singleton.OnStateChanged -= HandleStateChanged;
        }

        void HandleStateChanged(StateEnum obj)
        {
            switch (obj)
            {
                case StateEnum.BROWSE:
                    _visualButton.SetActive(true);
                    _visualButton.GetComponentInChildren<Toggle>().isOn = false;
                    break;
                case StateEnum.EDIT:
                    _visualButton.SetActive(false);
                    break;
                case StateEnum.RECORD:
                    break;
                case StateEnum.POPUP:
                    _visualButton.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, "No such state");
            }
        }

        public void HandleClick()
        {
            EventManager.Singleton.OnStateChanged.Invoke(_isRecording ? StateEnum.BROWSE : StateEnum.RECORD);
            _isRecording = !_isRecording;
        }
    }
}