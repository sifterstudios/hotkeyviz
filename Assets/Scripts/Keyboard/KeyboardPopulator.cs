using System.Collections.Generic;
using System.Linq;
using Sifter.DataManagement;
using Sifter.Json;
using Sifter.Managers;
using Sifter.UI.Button;
using UnityEngine;

namespace Sifter.Keyboard
{
    public class KeyboardPopulator : MonoBehaviour
    {
        [SerializeField] List<Button> _buttons;
        public List<KeyboardButton> KeyboardButtons;

        void Awake()
        {
            EventManager.Singleton.OnKeymapLoadComplete += OnKeymapLoadComplete;
        }

        void OnKeymapLoadComplete(List<KeyBinding> keybindings)
        {
            for (int i = 0; i < keybindings.Count; i++)
            {
               var buttonsOrdered = keybindings[i]._buttonsOrdered; 
                foreach (var button in buttonsOrdered)
                {
                    var b = _buttons.FirstOrDefault(b => b.KeyboardButton.Position.Column == button.Position.Row &&
                                                         b.KeyboardButton.Position.Row ==
                                                         button.Position.Column); // TODO: Clean this up
                    if (b == null) continue;
                    b.IncrementBindingCounter();
                    b.RedrawKey();
                }
            }
        }

        void Start()
        {
            EventManager.Singleton.OnLayoutChanged += LoadLayout;
            KeyboardButtons = LayoutLoader.LoadLayout();
            foreach (var button in KeyboardButtons)
            foreach (var b in _buttons.Where(b => b.KeyboardButton.Position.Column == button.Position.Row &&
                                                  b.KeyboardButton.Position.Row ==
                                                  button.Position.Column)) // TODO: Clean this up
            {
                if (!b.Changeable) continue;
                b.KeyboardButton.Key = button.Key;
                b.KeyboardButton.ShiftKey = button.ShiftKey;
                b.RedrawKey();
            }
        }

        void OnDisable()
        {
            EventManager.Singleton.OnLayoutChanged -= LoadLayout;
        }

        void LoadLayout()
        {
            print("This works!");
        }
    }
}