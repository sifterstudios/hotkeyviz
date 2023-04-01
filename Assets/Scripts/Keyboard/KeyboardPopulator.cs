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
                foreach (var b in _buttons.Where(b => b.KeyboardButton.Position.Column == button.Position.Row &&
                                                      b.KeyboardButton.Position.Row == button.Position.Column))
                {
                    b.KeyboardButton.Key = button.Key;
                    b.KeyboardButton.ShiftKey = button.ShiftKey;
                    b.RedrawKey();
                }
            }
        }
    }
}