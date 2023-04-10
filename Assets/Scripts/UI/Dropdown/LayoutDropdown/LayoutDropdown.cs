using System.Linq;
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
            var loadHandle =
                Addressables.LoadAssetsAsync<TextAsset>("Layouts", null);
            loadHandle.Completed += handle =>
            {
                var layouts = handle.Result.Select(layout => layout.name).ToList();
                _dropdown.ClearOptions();
                _dropdown.AddOptions(layouts);
            };
        }
    }
}