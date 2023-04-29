using System.Collections.Generic;
using Sifter.Keyboard;

namespace Sifter.DataManagement
{
    public class KeyBinding
    {
        public List<KeyboardButton> _buttonsOrdered;
        public bool IsCommandMode;
        public bool IsInsertMode;
        public bool IsNormalMode;
        public bool IsVisualMode;
    }
}