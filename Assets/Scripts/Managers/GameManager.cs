using System.Collections.Generic;
using System.Globalization;
using Sifter.DataManagement;
using Sifter.Keyboard;
using UnityEngine;
using UnityEngine.UI;

namespace Sifter.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;
        [SerializeField] KeyboardPopulator _keyboardPopulator;
        public bool isNormalModeActivated;
        public bool isInsertModeActivated;
        public bool isVisualModeActivated;
        public bool isCommandModeActivated;


        void Awake()
        {
            if (Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            var refreshRate = Screen.currentResolution.refreshRateRatio;
            var refreshRateValue = refreshRate.value;
            var targetFrameRate = int.Parse(refreshRateValue.ToString(CultureInfo.InvariantCulture));
            Application.targetFrameRate = targetFrameRate;
            EventManager.Singleton.OnLayoutChanged += HandleOnLayoutChanged;
        }

        void OnEnable()
        {
            EventManager.Singleton.OnModeChanged += HandleModeChange;
            EventManager.Singleton.OnModeClear.AddListener(HandleModeClear);
        }

        void HandleModeChange(List<Toggle> toggles)
        {
            isNormalModeActivated = toggles[Modes.Normal].isOn;
            isInsertModeActivated = toggles[Modes.Insert].isOn;
            isVisualModeActivated = toggles[Modes.Visual].isOn;
            isCommandModeActivated = toggles[Modes.Command].isOn;
        }

        void HandleModeClear()
        {
            isNormalModeActivated = false;
            isInsertModeActivated = false;
            isVisualModeActivated = false;
            isCommandModeActivated = false;
        }

        void HandleOnLayoutChanged()
        {
            // TODO: If the layout changes, all keybindings that are loaded will have wrong locations/letters, depending on how you look at it. This should be handled with a popup!
        }
    }
}