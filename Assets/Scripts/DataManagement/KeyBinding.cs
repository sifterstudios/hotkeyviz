using System.Collections.Generic;
using Sifter.UI.Button;

namespace Sifter.DataManagement
{
    public class KeyBinding
    {
        public List<Button> _buttonsOrdered = new();

        public string
            Description; // TODO: Add this to the GUI, either a textbox to the right or a popup when you click a checkmark!

        public bool IsCommandMode;
        public bool IsInsertMode;
        public bool IsNormalMode;
        public bool IsVisualMode;
    }
}