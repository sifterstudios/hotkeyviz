using System.Collections.Generic;
using System.Linq;
using Sifter.Json;
using UnityEngine;

namespace Sifter.Keyboard
{
    public class KeyboardPopulator : MonoBehaviour
    {
        [SerializeField] List<Button.Button> _buttons;

        void Start()
        {
            var keyboardButtons = LayoutLoader.LoadLayout();
            foreach (var button in keyboardButtons)
            {
                print(button.ToString());
                foreach (var goButton in _buttons.Where(goButton =>
                             goButton.KeyboardButton.Position == button.Position))
                {
                    goButton.KeyboardButton.Key = button.Key;
                    goButton.KeyboardButton.ShiftKey = button.ShiftKey;
                    goButton.redrawKey();
                }
            }
        }
    }
}