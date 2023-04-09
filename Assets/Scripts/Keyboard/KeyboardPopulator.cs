using System.Collections.Generic;
using System.Linq;
using Sifter.Json;
using Sifter.Managers;
using Sifter.UI.Button;
using UnityEngine;

namespace Sifter.Keyboard
{
    public class KeyboardPopulator : MonoBehaviour
    {
        [SerializeField] List<Button> _buttons;

        void Start()
        {
            EventManager.Singleton.OnLayoutChanged += LoadLayout;
            var keyboardButtons = LayoutLoader.LoadLayout();
            foreach (var button in keyboardButtons)
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