using Sifter.Managers;
using TMPro;
using UnityEngine;

namespace Sifter.UI.Dropdown.LayoutDropdown
{
    public class KeymapDropdown : MonoBehaviour
    {
        TMP_Dropdown _dropdown;

        void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            var singletonKeymapNames = SaveManager.Singleton.KeymapNames;
            _dropdown.ClearOptions();
            _dropdown.AddOptions(singletonKeymapNames);
        }

        public void IndividualValueChanged()
        {
            EventManager.Singleton.InvokeEvent(EventManager.Singleton.OnLayoutChanged);
        }
    }
}