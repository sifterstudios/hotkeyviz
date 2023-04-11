using Sifter.Managers;
using TMPro;
using UnityEngine;

namespace Sifter.UI.Dropdown.KeymapDropdown
{
    public class KeymapDropdown : MonoBehaviour
    {
        TMP_Dropdown _dropdown;

        void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.ClearOptions();
            var singletonKeymapNames = PersistenceManager.Singleton.KeymapNames;
            if (!singletonKeymapNames.Contains("Add new keymap")) singletonKeymapNames.Add("Add new keymap");

            _dropdown.AddOptions(singletonKeymapNames);
        }

        public void OnKeymapChanged()
        {
            var keymapName = _dropdown.options[_dropdown.value].text;
            EventManager.Singleton.OnKeymapChangedInGUI(keymapName);
        }
    }
}