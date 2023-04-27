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

        void OnKeymapLoadComplete(List<KeyBinding> keybindings)
        {
            foreach (var b in from t in keybindings
                     select t._buttonsOrdered
                     into buttonsOrdered
                     from button in buttonsOrdered
                     select _buttons.FirstOrDefault(b => b.KeyboardButton.Position.Column == button.Position.Row &&
                                                         b.KeyboardButton.Position.Row ==
                                                         button.Position.Column)
                     into b
                     where b != null
                     select b)
            {
                b.IncrementBindingCounter();
                b.RedrawKey();
            }
        }

        static void LoadLayout()
        {
            print("This works!");
        }
    }
}