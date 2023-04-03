using TMPro;
using UnityEngine;

namespace Sifter.UI.Dropdown
{
    public class SifterDropdown : MonoBehaviour, IDropdownIndividual
    {
        [HideInInspector] public TMP_Dropdown _dropdown;

        void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            _dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(_dropdown); });
            IndividualStart();
        }

        public void IndividualStart()
        {
        }

        public void IndividualValueChanged()
        {
        }

        void DropdownValueChanged(TMP_Dropdown change)
        {
            print(change.value);
            IndividualValueChanged();
        }
    }
}