using System.Collections.Generic;
using System.Linq;
using Sifter.DataManagement;
using Sifter.Json;
using Sifter.Managers;
using Sifter.UI.Button;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sifter.Keyboard
{
    public class KeyboardPopulator : MonoBehaviour
    {
        [FormerlySerializedAs("_buttons")] [SerializeField]
        List<Button> _buttonsGUI;

        public List<KeyboardButton> KeyboardButtons = new();
        LayoutLoader _layoutLoader;

        void Awake()
        {
            EventManager.Singleton.OnKeymapLoadComplete += OnKeymapLoadComplete;
        }

        void Start()
        {
            _layoutLoader = GetComponent<LayoutLoader>();
            LoadLayout();
        }

        void OnEnable()
        {
            EventManager.Singleton.OnLayoutChanged += LoadLayout;
        }

        void OnDisable()
        {
            EventManager.Singleton.OnLayoutChanged -= LoadLayout;
        }

        void OnKeymapLoadComplete(List<KeyBinding> keybindings)
        {
            foreach (var b in from t in keybindings
                     select t._buttonsOrdered
                     into buttonsOrdered
                     from button in buttonsOrdered
                     select _buttonsGUI.FirstOrDefault(b =>
                         b.KeyboardButton.Position.Column == button.KeyboardButton.Position.Row &&
                         b.KeyboardButton.Position.Row ==
                         button.KeyboardButton.Position.Column)
                     into b
                     where b != null
                     select b)
                b.IncrementBindingCounter();
            // b.RedrawKey();
        }

        void LoadLayout()
        {
            print("This works!");
            KeyboardButtons = _layoutLoader.LoadLayout();
            print("Number of buttons: " + KeyboardButtons.Count);
            foreach (var button in KeyboardButtons)
            foreach (var b in _buttonsGUI.Where(b => b.KeyboardButton.Position.Column == button.Position.Row &&
                                                     b.KeyboardButton.Position.Row ==
                                                     button.Position.Column)) // TODO: Clean this up
            {
                if (!b.Changeable) continue;
                b.KeyboardButton.Key = button.Key;
                b.KeyboardButton.ShiftKey = button.ShiftKey;
                b.RedrawKey();
            }
        }
    }
}