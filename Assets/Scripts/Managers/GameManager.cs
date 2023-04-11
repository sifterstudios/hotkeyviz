using Sifter.Keyboard;
using UnityEngine;

namespace Sifter.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton = new();
        [SerializeField] KeyboardPopulator _keyboardPopulator;

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
            var test = int.Parse(refreshRateValue.ToString());
            Application.targetFrameRate = test;
            EventManager.Singleton.OnLayoutChanged += HandleOnLayoutChanged;
        }

        void HandleOnLayoutChanged()
        {
            // TODO: If the layout changes, all keybindings that are loaded will have wrong locations/letters, depending on how you look at it. This should be handled with a popup!
        }
    }
}