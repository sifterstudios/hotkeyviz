using System;
using DTT.UI.ProceduralUI;
using Sifter.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sifter.UI.Button.Confirm
{
    public class ButtonConfirm : MonoBehaviour, IPointerEnterHandler,
        IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] RoundedImage _border;
        StateEnum _localState;

        void OnEnable()
        {
            EventManager.Singleton.OnStateChanged += ctx => _localState = ctx;
        }

        void OnDisable()
        {
            EventManager.Singleton.OnStateChanged -= ctx => _localState = ctx;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (_localState)
            {
                case StateEnum.BROWSE:
                    break;
                case StateEnum.EDIT:
                    EventManager.Singleton.OnStateChanged.Invoke(StateEnum.BROWSE);
                    break;
                case StateEnum.RECORD:
                    EventManager.Singleton.OnKeybindConfirm.Invoke();
                    EventManager.Singleton.OnStateChanged.Invoke(StateEnum.BROWSE);
                    break;
                case StateEnum.POPUP:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(_localState.ToString(),
                        _localState, "No such state");
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _border.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _border.gameObject.SetActive(false);
        }
    }
}