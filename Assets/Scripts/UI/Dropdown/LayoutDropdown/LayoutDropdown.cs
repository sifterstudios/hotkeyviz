using System.Linq;
using Sifter.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Sifter.UI.Dropdown.LayoutDropdown
{
    public class LayoutDropdown : MonoBehaviour
    {
        TMP_Dropdown _dropdown;

        void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
            print("Start loading");
            var loadHandle =
                Addressables.LoadAssetsAsync<TextAsset>("Layouts", null);
            loadHandle.Completed += handle =>
            {
                print("Inside completed");
                var layouts = handle.Result.Select(layout => layout.name).ToList();
                // _dropdown.ClearOptions();
                _dropdown.AddOptions(layouts);
            };
        }

        public void IndividualValueChanged()
        {
            // call event that layout changed
            EventManager.Singleton.InvokeEvent(EventManager.Singleton.OnLayoutChanged);
        }
    }
}